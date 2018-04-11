using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Classes
{
    public class RouterRequest<T> : IRequestable<T>, IExceptionable
    {
        public Exception Error { get; private set; }
        public string Path { get; private set; }
        public T Body { get; set; }
        public RouterRequest(string path, T body)
        {
            Path = path;
            Body = body;
        }


    }
}
