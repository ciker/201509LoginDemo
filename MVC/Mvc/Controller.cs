using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc
{
    public class Controller : ControllerBase
    {
        public IActionInvoker ActionInvoker { get; set; }
        public Type Type { get; set; }

        public Controller()
        {
            ActionInvoker = new ControllerActionInvoker();
            Type = this.GetType();
        }
        protected override void ExecuteCore(string action)
        {
            ActionInvoker.InvokeAction(Type, action);
        }

        public ContentResult Content(string msg)
        {
            return new ContentResult() { Data = msg };
        }
    }

}
