using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
    public static class StringExtensions
    {
        public static string ToProperCase(this string value)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        public static string ToFullPath(this string path)
        {
            return Path.GetFullPath(path);
        }

        public static bool Contains(this string sourceString, string stringToCheck, StringComparison comparison)
        {
            return sourceString.IndexOf(stringToCheck, comparison) >= 0;
        }

        public static bool IsEmpty(this string stringValue)
        {
            return string.IsNullOrEmpty(stringValue);
        }

        public static bool IsNotEmpty(this string stringValue)
        {
            return !string.IsNullOrEmpty(stringValue);
        }

        public static bool ToBool(this string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue)) return false;

            return bool.Parse(stringValue);
        }

        public static string FormatWith(this string stringFormat, params object[] args)
        {
            return String.Format(stringFormat, args);
        }

        public static string ExtractDomainName(this string value)
        {
            if (value.IsEmpty()) return "";
            string[] strings = null;

            if (value.Contains("\\"))
            {
                strings = value.Split('\\');
                var name = strings.Last();
                return name;
            }

            if (value.Contains("/"))
            {
                strings = value.Split('/');
                var name = strings.Last();
                return name;
            }

            if (value.Contains("@"))
            {
                strings = value.Split('@');
                var name = strings.Last();
                return name;
            }
            return value;
        }

        [DebuggerStepThrough]
        public static T ToEnum<T>(this string target, T defaultValue) where T : IComparable, IFormattable
        {
            T convertedValue = defaultValue;

            if (!string.IsNullOrEmpty(target))
            {
                try
                {
                    convertedValue = (T)Enum.Parse(typeof(T), target.Trim(), true);
                }
                catch (ArgumentException)
                {
                }
            }

            return convertedValue;
        }
    }
}
