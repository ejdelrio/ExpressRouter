﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Classes
{
    public class RouterResponse<T> : IResponsable<T>
    {
        public string Path { get; private set; }

        public T Body { get; private set; }

        public RouterResponse(IRequestable<T> req)
        {
            Path = req.Path;
            Body = req.Body;
        }

        
    }
}
