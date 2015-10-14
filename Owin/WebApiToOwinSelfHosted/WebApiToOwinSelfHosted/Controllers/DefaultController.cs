using System.Web.Http;

namespace WebApiToOwinSelfHosted.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "This is the Get method in the Default Controller response";
        }
    }
}
