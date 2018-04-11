using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Classes
{
    public class RouterResponse<T> : IResponsable<T>, IExceptionable
    {
        /// <summary>
        /// Path which the response originated from
        /// </summary>
        public string Path { get; private set; }


        /// <summary>
        /// Body passed to the request after the body has been parsed
        /// </summary>
        public T Body { get; private set; }

        public Exception Error { get; private set; }

        /// <summary>
        /// Passes the response's contents to the instance of the RouterResponse
        /// </summary>
        /// <param name="req"></param>
        public RouterResponse(IRequestable<T> req)
        {
            Path = req.Path;
            Body = req.Body;
        }

        
    }
}
