﻿using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
//using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
//using System.Threading.Tasks;
using Newtonsoft.Json;
//using ServiceStack;

namespace LoginDemo.Commom
{
    public static class StringExt
    {

        /// <summary>
        /// 获取config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppConfigByKey(this string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("Key is not null allowed");
            }
            var configValue = "";
            configValue = ConfigurationManager.AppSettings[key];
            return configValue;
        }

        /// <summary>
        /// to base64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToBase64String(this string str)
        {
            var encbuff = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FromBase64String(this string str)
        {
            var decbuff = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(decbuff);
        }

        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5Compute32(this string str)
        {
            var md5 = MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(str));
            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获得16位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5Compute16(this string input)
        {
            return Md5Compute32(input).Substring(8, 16);
        }

        /// <summary>
        /// string split
        /// </summary>
        /// <param name="input"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string[] Split(this string input, string split)
        {
            return input.Split(split.ToArray());
        }

        /// <summary>
        /// sql key words filter
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SafeSqlLiteral(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            input = input.VbStringReplace("AND", "");
            input = input.VbStringReplace("OR", "");
            input = input.VbStringReplace("SELECT", "");
            input = input.VbStringReplace("UPDATE", "");
            input = input.VbStringReplace("INSERT", "");
            input = input.VbStringReplace("ALTER", "");
            input = input.VbStringReplace("DELETE", "");
            input = input.VbStringReplace("DROP", "");
            input = input.VbStringReplace("TRUNCATE", "");
            input = input.VbStringReplace("LIKE", "");
            input = input.VbStringReplace("CREATE", "");
            input = input.VbStringReplace("EXEC", "");
            input = input.VbStringReplace("DECLARE", "");
            input = input.VbStringReplace("FROM", "");
            input = input.VbStringReplace("%", "[%]");
            input = input.VbStringReplace("@", "[@]");
            input = input.VbStringReplace("[", "[[]");
            input = input.VbStringReplace("_", "[_]");
            return input;
        }

        /// <summary>
        /// use VB.String.Replace ,ignore the str case
        /// </summary>
        /// <param name="str"></param>
        /// <param name="find"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string VbStringReplace(this string str, string find, string replacement)
        {
            return Microsoft.VisualBasic.Strings.Replace(str, find, replacement);
        }

        /// <summary>
        /// deserialize string to obj
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T Deserialize2Obj<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// reflect to generate condition 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="t"></param>
        /// <param name="ignorePageInfo">whether ignore the page properties</param>
        /// <returns></returns>
        public static string GenerateCondition(this string str, object t)
        {
            var conditions = new StringBuilder();
            var properties = t.GetType().GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);


            (from property in properties
             where property.GetValue(t, null) != null
             //let des = property.GetCustomAttribute<IgnoreFieldAttribute>()
             let des = property.IsDefined(typeof(IgnoreFieldAttribute)) //optimizate GetCustomAttribute by IsDefined
             //where des == null
             where des == false
             select property).Each<PropertyInfo>(property =>
            {
                conditions.Append(" AND ").Append(property.Name).Append("=@").Append(property.Name);
            });
            #region foreach
            //foreach (var property in from property in properties
            //                         where property.GetValue(t, null) != null
            //                         let des = property.GetCustomAttribute<IgnoreFieldAttribute>()
            //                         where des == null
            //                         select property)
            //{
            //    conditions.Append(" AND ").Append(property.Name).Append("=@").Append(property.Name);
            //}
            #endregion
            #region Parallel.ForEach
            //var propertys = from property in properties
            //                where property.GetValue(t, null) != null
            //                let des = property.GetCustomAttribute<IgnoreFieldAttribute>()
            //                where des == null
            //                select property;
            //Parallel.ForEach(propertys, (property) =>
            //{
            //    conditions.Append(" AND " + property.Name + "=@" + property.Name + " ");
            //});
            #endregion
            #region foreach

            //foreach (var property in properties)
            //{
            //    if (property.GetValue(t, null) == null) continue;
            //    var des = property.GetCustomAttribute<IgnoreFieldAttribute>();
            //    if (des == null)
            //    {
            //        conditions.Append(" AND " + property.Name + "=@" + property.Name + " ");
            //    }
            //}
            #endregion
            return conditions.ToString();
        }



        /// <summary>
        /// regular mobile number
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMobile(this string str)
        {
            var regex = new Regex(@"^[1][3-8]\d{9}$");
            return regex.IsMatch(str);
        }

        /// <summary>
        /// regular email 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(this string str)
        {
            var regex = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            return regex.IsMatch(str);
        }

        public static bool IsQQ(this string str)
        {
            var regex = new Regex(@"^\d{5,10}$");
            return regex.IsMatch(str);
        }

        public static int GetAccountType(this string str)
        {
            return str.IsMobile() ? 1 : (str.IsEmail() ? 2 : (str.IsQQ() ? 3 : 0));
        }
    }
}
