using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressRouter.Interfaces
{
    public interface IRoute
    {
        string Path { get; }
        string Description { get; }
        

    }
}
