using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Exceptions
{
    class Router400ErrorNoResponseDefined<T> : RouterAbstractException<T>
    {
        public Router400ErrorNoResponseDefined(IRequestable<T> req) : base(404, req)
        { }


    }
}
