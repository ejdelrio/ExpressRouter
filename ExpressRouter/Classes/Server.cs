using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Classes
{
    class Server<T> : IServable<T>
    {
        public Func<IRequestable<T>, IResponsable<T>> Process { get; private set; }
        public Server(Func<IRequestable<T>, IResponsable<T>> process)
        {
            Process = process;
        }

        
    }
}
