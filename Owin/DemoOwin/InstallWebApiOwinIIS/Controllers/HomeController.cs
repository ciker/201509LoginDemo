using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;

namespace InstallWebApiOwinIIS.Controllers
{
    public class HomeController : ApiController
    {
        public HttpResponseMessage Get(){
            var filePath = HostingEnvironment.MapPath("~/index.html");

            var response = new HttpResponseMessage(HttpStatusCode.OK);

            // met.1 (MSDN): http://msdn.microsoft.com/en-us/library/ezwyzy7b.aspx, 
            //       (Darrel M.): http://stackoverflow.com/a/8122393
            response.Content = new StringContent(File.ReadAllText(filePath));

            // met.2 http://stackoverflow.com/a/20888749
            //response.Content = new StreamContent(File.OpenRead(filePath));

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
