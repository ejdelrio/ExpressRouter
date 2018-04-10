using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Delegates;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Classes
{
    public class Router<T> : IRouterable<T>
    {
        public Dictionary<IRoute, IServable<T>> DefinedRoutes { get; private set; }
        /// <summary>
        /// Takes a route and a series of functions to construct a server for the given route.
        /// The server will reference the series of functions and pass the request through each function
        /// </summary>
        /// <param name="route">Route with given path and description</param>
        /// <param name="middleWareItems">Series of middle ware functions to parse a request</param>
        public void AddServer(IRoute route, params MiddleWareOperation<T>[] middleWareItems)
        {
            if (route == null)
                throw new ArgumentNullException();

            IServable<T> server = DefineServer(middleWareItems);
            DefinedRoutes.Add(route, server);
        }


        /// <summary>
        /// Takes a variable amount of functions, tkate take type T and return type T. Defines an
        /// arrow function that will always reference the same series of functions but will take a new request each time
        /// </summary>
        /// <param name="middleWareItems">series of Generic functions that take type t and return type t</param>
        /// <returns>an instance of a server with it's new process defined</returns>
        IServable<T> DefineServer(params MiddleWareOperation<T>[] middleWareItems)
        {
            IServable<T> server = new Server<T>(req => GetResponseFromServer(req, middleWareItems));

            return server;
        }
        /// <summary>
        /// Passes requet through variable number of functions that are saved to the server instance
        /// </summary>
        /// <param name="req">Instance of request passed to the router</param>
        /// <param name="middleWareItems">Series of functions that take a request and return a processed resquest</param>
        /// <returns>Parsed Request</returns>
       IRequestable<T> ParseRequest(IRequestable<T> req, params MiddleWareOperation<T>[] middleWareItems)
        {
            foreach (var operation in middleWareItems)
                req = operation(req);

            return req;
        }

        /// <summary>
        /// constructs a response from the parsed user request
        /// </summary>
        /// <param name="req">Request passed on the instance of the Router query. Will constantly change for the route</param>
        /// <param name="middleWareItems">Middle ware saved to the instance of a server. The middle ware will be constant</param>
        /// <returns>A response constructed from a parsed request body</returns>
        public IResponsable<T> GetResponseFromServer(IRequestable<T> req, params MiddleWareOperation<T>[] middleWareItems)
        {
            IRequestable<T> parsedReq = ParseRequest(req, middleWareItems);
            IResponsable<T> res = new RouterResponse(parsedReq);

            return res;
        }


        IServable<T> GetServer(IRoute route)
        {
            bool RouteIsNotDefined = DefinedRoutes.ContainsKey(route);
            if (RouteIsNotDefined)
                throw new KeyNotFoundException();

            var StoredServerInteface = DefinedRoutes[route];
            return StoredServerInteface;
        }


        public void AddServer(IRoute route, IServable<T> server)
        {
            throw new NotImplementedException();
        }
    }
}
