using System;
using System.IO;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace Owin.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region file hashcode

            var fpath = @"D:\sysbacke450.GHO";
            var file = new FileInfo(fpath);
            var hash = file.GetHashCode();
            var MD5Hash = HashHelper.ComputeMD5(fpath);

            var copyPath = @"D:\sysbacke4501123123.GHO";
            file.CopyTo(copyPath);
            var fileC = new FileInfo(copyPath);
            var hashCopy = fileC.GetHashCode();
            var MD5HashC = HashHelper.ComputeMD5(copyPath);

            Console.WriteLine(hash);

            Console.WriteLine(MD5Hash);
            Console.WriteLine(hashCopy);
            Console.WriteLine(MD5HashC);
            Console.ReadLine();

            #endregion


            #region webapi owin self host

            //const string url = "http://localhost:8080/";
            //var startOpts = new StartOptions(url)
            //{
            //};
            ////using (WebApp.Start<Startup>(startOpts))
            ////{
            ////    Console.WriteLine("Server run at " + url + " , press Enter to exit.");
            ////    Console.ReadLine();
            ////}
            //using (WebApp.Start<Startup>(url: url))
            //{
            //    var client = new HttpClient();
            //    var response = client.GetAsync(url + "api/HomeApi/1").Result;

            //    Console.WriteLine(response);
            //    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            //    Console.ReadLine();

            //}

            #endregion
        }
    }
}
