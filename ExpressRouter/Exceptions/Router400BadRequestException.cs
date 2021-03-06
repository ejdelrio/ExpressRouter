﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Exceptions
{
    public class Router400BadRequestException<T> : RouterAbstractException<T>
    {


        public Router400BadRequestException(IRequestable<T> req) : base(404, req)
        {
        }

    }
}
