using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Delegates;

namespace ExpressRouter.Interfaces
{
    public interface IRouterable<T>
    {
        IResponsable<T> GetResponseFromServer(IRequestable<T> req, params MiddleWareOperation<T>[] middleWareItems);
        void AddServer(IRoute route, IServable<T> server);
    }
}
