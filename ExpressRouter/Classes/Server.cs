using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Interfaces;

namespace ExpressRouter.Classes
{
    public class Server<T> : IServable<T>
    {
        /// <summary>
        /// Optional descirption of the paths functionality for user presentation
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// function that is defined on instantiation. Takes and returns the same data type after it's been parsed
        /// </summary>
        public Func<IRequestable<T>, IRequestable<T>> Process { get; private set; }
        public Server(string description, Func<IRequestable<T>, IRequestable<T>> process)
        {
            Description = description;
            Process = process;
        }

        public Server(Func<IRequestable<T>, IRequestable<T>> process) : this("No descrption provided.", process)
        { }

    }
}
