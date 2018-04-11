using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Delegates
{
    /// <summary>
    /// Pointet for middleware function definitions
    /// </summary>
    /// <typeparam name="T">defines body of the input and output</typeparam>
    /// <param name="req">Request to be parsed be a function definition</param>
    /// <returns>Same request after it's been processed</returns>
    public delegate IRequestable<T> MiddleWareOperation<T>(IRequestable<T> req);
}
