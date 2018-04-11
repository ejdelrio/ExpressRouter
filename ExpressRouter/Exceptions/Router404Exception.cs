using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Exceptions
{
    public class Router404Exception : Exception
    {
        public string Path { get; private set; }
        public Router404Exception()
        {
        }
        public Router404Exception(string path)
        {

            Path = path;
        }
    }
}
