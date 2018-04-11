using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Exceptions
{
    public class Router404PathNotFoundException<T> : RouterAbstractException<T>
    {

        public Router404PathNotFoundException(string path) : base(404, path)
        {

        }
    }
}
