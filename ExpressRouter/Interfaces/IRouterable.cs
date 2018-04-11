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
        IResponsable<T> GetResponseFromServer(IRequestable<T> req);
        void AddServer(string path, params MiddleWareOperation<T>[] middleWareItems);
    }
}
