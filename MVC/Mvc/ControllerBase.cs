using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;

namespace Mvc
{
    public abstract class ControllerBase : IController
    {
        public static ICollection<Type> Types { get; set; }
        protected abstract void ExecuteCore(string action);
        public virtual void Execute(HttpContext context)
        {
            var strs = context.Request.AppRelativeCurrentExecutionFilePath.Substring(2).Split('/');
            var control = string.Empty;
            var action = string.Empty;
            if (strs.Length == 2)
            {
                control = strs[0];
                action = strs[1];
            }
            else
            {
                control = "Home";
                action = "Index";
            }
            if (Types == null)
            {
                Types = new Collection<Type>();
                var assms = BuildManager.GetReferencedAssemblies();
                foreach (Assembly assembly in assms)
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        Types.Add(type);
                    }
                }
            }
            var controlType = Types.SingleOrDefault(o => o.Name == control + "Controller");
            if (controlType != null)
            {
                var baseControl = Activator.CreateInstance(controlType) as Controller;
                if (baseControl != null) baseControl.ExecuteCore(action);
            }
            



        }
    }
}
