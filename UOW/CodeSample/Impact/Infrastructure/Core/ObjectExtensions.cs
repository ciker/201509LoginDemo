using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core
{

    public static class ObjectExtensions
    {
        public static bool IsTypeOf<T>(this object o)
        {
            return o.GetType() == typeof(T);
        }

        public static bool IsNot<T>(this object obj)
        {
            return !(obj is T);
        }

        public static bool Is<T>(this object obj)
        {
            return obj is T;
        }

        public static bool IsNull(this object obj)
        {
            return ReferenceEquals(obj, null);
        }

        public static bool IsNotNull(this object obj)
        {
            return !ReferenceEquals(obj, null);
        }

        public static bool IsDBNull(this object obj)
        {
            return Convert.IsDBNull(obj);
        }

        public static bool IsNotDBNull(this object obj)
        {
            return !Convert.IsDBNull(obj);
        }

        public static T As<T>(this object o)
        {
            T value = default(T);

            // given an empty string, return the default value
            if (o is String && o.ToString().IsEmpty()) return value;

            try
            {
                if (typeof(T) == typeof(String))
                {
                    return (T)(o.ToString() as object);
                }
                if (value is ValueType)
                {
                    // TODO: HAS 07/10/2011 Complete tests for all default value types.
                    if (value is Int32) return (T)(Convert.ToInt32(o) as object);
                    if (value is Double) return (T)(Convert.ToDouble(o) as object);
                    if (value is Decimal) return (T)(Convert.ToDecimal(o) as object);
                    if (value is Boolean) return (T)(Convert.ToBoolean(o) as object);
                    if (value is Guid) return (T)(new Guid(o.ToString()) as object);
                    if (typeof(T) == typeof(DateTime)) return (T)(DateTime.Parse(o.ToString()) as object);
                }
                value = (T)o;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("Conversion Error: {0}", e));
            }
            return value;
        }
    }
}
