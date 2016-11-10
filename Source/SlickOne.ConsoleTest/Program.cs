using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlickOne.WebUtility;

namespace SlickOne.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("begin to test...");
            var baseUrl = "http://localhost/soweb/api/helloworld";
            for (var i = 0; i < 10; i++)
            {
                var clientHelper = HttpClientHelper.CreateHelper(string.Format("{0}/get/{1}", baseUrl, i.ToString()));
                var message = clientHelper.Get();

                Console.WriteLine(string.Format("the result is: {0} ", message));
            }


            System.Console.ReadLine();
        }
    }
}
