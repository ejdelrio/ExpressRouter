using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Delegates
{
    public delegate IRequestable<T> MiddleWareOperation<T>(IRequestable<T> req);
}
