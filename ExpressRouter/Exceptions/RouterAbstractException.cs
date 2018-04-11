using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Exceptions
{
    public abstract class RouterAbstractException<T> : Exception
    {
        public int ErrorCode { get; private set; }
        public T Body { get; private set; }
        public string Path { get; private set; }

        private RouterAbstractException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public RouterAbstractException(int errorCode, string path) : this(errorCode)
        {
            Path = path;
        }

        public RouterAbstractException(int errorCode, IRequestable<T> req) : this(errorCode)
        {
            Body = req.Body;
            Path = req.Path;
        }

        public RouterAbstractException(int errorCode, IResponsable<T> res) : this(errorCode)
        {
            Body = res.Body;
            Path = res.Path;
        }
        
    }
}
