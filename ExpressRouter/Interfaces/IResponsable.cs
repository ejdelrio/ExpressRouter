using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressRouter.Interfaces
{
    public interface IResponsable<T>
    {
        IRoute Path { get; }
        T Body { get; }
    }
}
