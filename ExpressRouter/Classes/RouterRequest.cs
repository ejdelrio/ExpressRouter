using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Classes
{
    class RouterRequest<T> : IResponsable<T>
    {
        public IRoute Path { get; private set; }
        public T Body { get; private set; }
        public RouterRequest(IRequestable<T> req)
        {

        }


    }
}
