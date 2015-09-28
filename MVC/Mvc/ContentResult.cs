using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mvc
{
    public class ContentResult : ActionResult
    {
        public string Data { get; set; }
        public override void ExecuteResult()
        {
            HttpContext.Current.Response.Write(Data);
        }
    }
}
