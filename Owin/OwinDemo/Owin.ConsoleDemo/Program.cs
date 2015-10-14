using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace Owin.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:8080/";
            var startOpts = new StartOptions(url)
            {
            };
            //using (WebApp.Start<Startup>(startOpts))
            //{
            //    Console.WriteLine("Server run at " + url + " , press Enter to exit.");
            //    Console.ReadLine();
            //}
            using (WebApp.Start<Startup>(url: url))
            {
                var client = new HttpClient();
                var response = client.GetAsync(url + "api/HomeApi/1").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();

            }
        }
    }
}
