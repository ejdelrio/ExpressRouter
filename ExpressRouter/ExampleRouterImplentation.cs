using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressRouter.Classes;
using ExpressRouter.Delegates;
using ExpressRouter.Interfaces;

namespace ExpressRouter
{
    class ExampleRouterImplentation
    {

        Router<string> EmbeddedRouter;

        public ExampleRouterImplentation(Router<string> router)
        {
            EmbeddedRouter = router;
            router.AddServer("Reverse String", ReverseString, ConstructResponse);
            router.AddServer("Capitalize Vowels", CapitalizeVowelsString, ConstructResponse);
            router.AddServer(
                "All",
                "Capitalize String, Reverse it, LowerCase it, ReverseIt, Capitlaize Vowels",
                CapitalizeString, 
                ReverseString,
                LowerCaseString,
                ReverseString,
                CapitalizeVowelsString,
                ConstructResponse);
        }

        MiddleWareOperation<string> ReverseString = (ref IRequestable<string> req, ref IResponsable<string> res) =>
        {
            string reqBody = req.Body;
            var reveresedString = new StringBuilder("");

            for (int i = reqBody.Length - 1; i >= 0; i--)
            {
                char letter = reqBody[i];
                reveresedString.Append(letter);
            }

            req.Body = reveresedString.ToString();
            Console.WriteLine(req.Body);
        };

        MiddleWareOperation<string> CapitalizeString = (ref IRequestable<string> req, ref IResponsable<string> res) =>
        {
            req.Body = req.Body.ToUpper();
            Console.WriteLine(req.Body);
        };

        MiddleWareOperation<string> LowerCaseString = (ref IRequestable<string> req, ref IResponsable<string> res) =>
        {
            req.Body = req.Body.ToLower();
            Console.WriteLine(req.Body);
        };

        MiddleWareOperation<string> CapitalizeVowelsString = (ref IRequestable<string> req, ref IResponsable<string> res) =>
        {
            var vowelDict = new Dictionary<char, bool>();
            vowelDict.Add('a', true);
            vowelDict.Add('e', true);
            vowelDict.Add('i', true);
            vowelDict.Add('o', true);
            vowelDict.Add('u', true);

            var reqBody = req.Body;
            var output = new StringBuilder("");

            for(int i = 0; i < reqBody.Length; i++)
            {
                var letter = req.Body.ToLower()[i];
                var letterToAdd = vowelDict.ContainsKey(letter) ? req.Body.ToUpper()[i] : req.Body[i];
                output.Append(letterToAdd);
            }
            req.Body = output.ToString();
            Console.WriteLine(req.Body);
        };


        MiddleWareOperation<string> ConstructResponse = (ref IRequestable<string> req, ref IResponsable<string> res) =>
        {
            res = new RouterResponse<string>(req);
        };

        public void CallRouter(IRequestable<string> req)
        {
            if (req == null)
                throw new ArgumentNullException();

            EmbeddedRouter.GetResponseFromServer(req);
        }

    }
}
