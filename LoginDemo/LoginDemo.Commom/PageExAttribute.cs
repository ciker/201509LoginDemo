using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginDemo.Commom
{
    public class PageExAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IgnoreFieldAttribute : Attribute
    {
    }
}
