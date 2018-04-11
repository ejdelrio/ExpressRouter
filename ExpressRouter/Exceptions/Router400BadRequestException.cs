using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Exceptions
{
    public class Router400BadRequestException<T> : Exception
    {
        public string Path { get; private set; }
        public T Body { get; private set; }

        public Router400BadRequestException(IRequestable<T> req)
        {
            Path = req.Path;
            Body = req.Body;
        }

    }
}
