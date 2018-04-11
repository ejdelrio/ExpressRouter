using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Delegates;
using ExpressRouter.Interfaces;
using ExpressRouter.Exceptions;

namespace ExpressRouter.Classes
{
    public class Router<T> : IRouterable<T>
    {
        public Dictionary<string, IServable<T>> DefinedRoutes { get; private set; }
        public Router (Dictionary<string, IServable<T>> dict)
        {
            DefinedRoutes = dict;
        }


        /// <summary>
        /// Takes a route and a series of functions to construct a server for the given route.
        /// The server will reference the series of functions and pass the request through each function
        /// </summary>
        /// <param name="route">Route with given path and description</param>
        /// <param name="middleWareItems">Series of middle ware functions to parse a request</param>
        public void AddServer(string path, string description, params MiddleWareOperation<T>[] middleWareItems)
        {
            if (path == null)
                throw new ArgumentNullException();

            IServable<T> server = DefineServer(description, middleWareItems);
            DefinedRoutes.Add(path, server);
        }

        public void AddServer(string path, params MiddleWareOperation<T>[] middleWareItems)
        {
            AddServer(path, path, middleWareItems);
        }


        /// <summary>
        /// Takes a variable amount of functions, tkate take type T and return type T. Defines an
        /// arrow function that will always reference the same series of functions but will take a new request each time
        /// </summary>
        /// <param name="middleWareItems">series of Generic functions that take type t and return type t</param>
        /// <returns>an instance of a server with it's new process defined</returns>
        IServable<T> DefineServer(string description, params MiddleWareOperation<T>[] middleWareItems)
        {
            IServable<T> server = new Server<T>(description, req => ParseRequest(req, middleWareItems));

            return server;
        }



        /// <summary>
        /// Passes requet through variable number of functions that are saved to the server instance
        /// </summary>
        /// <param name="req">Instance of request passed to the router</param>
        /// <param name="middleWareItems">Series of functions that take a request and return a processed resquest</param>
        /// <returns>Parsed Request</returns>
       IResponsable<T> ParseRequest(IRequestable<T> req, params MiddleWareOperation<T>[] middleWareItems)
        {
            IResponsable<T> res = null;
            foreach (var operation in middleWareItems)
                operation(ref req, ref res);

            return res;
        }


        /// <summary>
        /// constructs a response from the parsed user request
        /// </summary>
        /// <param name="req">Request passed on the instance of the Router query. Will constantly change for the route</param>
        /// <param name="middleWareItems">Middle ware saved to the instance of a server. The middle ware will be constant</param>
        /// <returns>A response constructed from a parsed request body</returns>
        public IResponsable<T> GetResponseFromServer(IRequestable<T> req)
        {
            if (req == null)
                throw new ArgumentNullException();

            IServable<T> server = GetServer(req.Path);
            var parsedReq = ValidateRouteProcesses(req, server);

            IResponsable<T> res = new RouterResponse<T>(req);
            if (res == null)
                throw new Router400ErrorNoResponseDefined<T>(req);

            return res;
        }


        IServable<T> GetServer(string path)
        {
            bool RouteIsNotDefined = DefinedRoutes.ContainsKey(path);
            if (!RouteIsNotDefined)
                throw new Router404Exception(path);

            var StoredServerInteface = DefinedRoutes[path];
            return StoredServerInteface;
        }
        /// <summary>
        /// Attempts to parse passed request with the routes middleware.
        /// Throw a custom exception if the function fails
        /// </summary>
        /// <param name="req">Request to be parsed</param>
        /// <param name="server">Server containing functionality to be used</param>
        /// <returns>A parsed request or an exception</returns>
        IResponsable<T> ValidateRouteProcesses(IRequestable<T> req, IServable<T> server)
        {
            try
            {
                IResponsable<T> res = server.Process(req);
                return res;
            }
            catch(Exception)
            {
                throw new Router400BadRequestException<T>(req);
            }
        }

    }
}
