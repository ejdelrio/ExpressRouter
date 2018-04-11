using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Classes;

namespace ExpressRouter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var routerDict = new Dictionary<string, Interfaces.IServable<string>>();
            var exampleRouter = new Classes.Router<string>(routerDict);
            var example = new ExampleRouterImplentation(exampleRouter);

            var firstRequest = new RouterRequest<string>("Reverse String", "Winner winner chicken dinner");
            var secondRequest = new RouterRequest<string>("Capitalize Vowels", "all of the vowels in this string will be big");
            var thirdRequest = new RouterRequest<string>("All", "This will do a bunch of stuff :D");

            example.CallRouter(firstRequest);
            example.CallRouter(secondRequest);
            example.CallRouter(thirdRequest);
            Console.ReadLine();

        }
    }
}
