using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc
{
    public class ControllerActionInvoker : IActionInvoker
    {
        public void InvokeAction(Type controller, string action)
        {
            var control = Activator.CreateInstance(controller) as Controller; 
            var controlType = control.GetType();
            var rst = controlType.GetMethod(action).Invoke(control, null) as ActionResult;
            rst.ExecuteResult();
        }
    }
}
