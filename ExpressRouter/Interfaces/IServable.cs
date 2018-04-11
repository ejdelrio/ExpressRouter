using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressRouter.Interfaces
{
    public interface IServable<T>
    {
        string Description { get; }
        Func<IRequestable<T>, IResponsable<T>> Process { get; }
    }
}
