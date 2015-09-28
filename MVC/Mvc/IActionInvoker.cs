using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc
{
    public interface IActionInvoker
    {
        void InvokeAction(Type controller, string action);
    }
}
