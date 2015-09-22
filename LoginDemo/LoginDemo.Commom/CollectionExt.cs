using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoginDemo.Commom
{
    public static class CollectionExtx
    {
        /// <summary>
        /// obj serialize to string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string SerializeObj2String<T>(this T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        public static void Each<T>(this IEnumerable<T> t, Action<T> action)
        {
            //Parallel.ForEach(t, action);
            foreach (var obj in t)
            {
                action(obj);
            }
        }

        public static string GenerateByOperate<T>(this T t, GenerateOperate generateOperate)
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
                 if (generateOperate == GenerateOperate.Condition)
                 {
                     conditions.Append(" AND ").Append(property.Name).Append("=@").Append(property.Name);
                 }
                 if (generateOperate == GenerateOperate.UpdateField)
                 {
                     conditions.Append(" SET ").Append(property.Name).Append("=@").Append(property.Name).Append(",");
                 }
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

        public static string GenerateSingleTableSqlByOperate<T>(this T t, SqlOperate sqlOperate)
        {
            var type = t.GetType();
            var tableName = type.Name;
            var properties =
                type.GetProperties(BindingFlags.GetField | BindingFlags.Public | BindingFlags.GetProperty |
                                   BindingFlags.Instance);
            var useableProperties = from p in properties
                                    where p.GetValue(t, null) != null
                                    let des = p.IsDefined(typeof(IgnoreFieldAttribute))
                                    where !des
                                    select p;

            var sqlText = new StringBuilder();
            if (sqlOperate == SqlOperate.Select)
            {

            }
            if (sqlOperate == SqlOperate.Insert)
            {

            }
            if (sqlOperate == SqlOperate.Update)
            {
                sqlText.Append("UPDATE ").Append(tableName).Append(" SET ");

                useableProperties.Each<PropertyInfo>((property) =>
                {
                    sqlText.Append(property.Name).Append("=@").Append(property.Name).Append(",");
                });
                sqlText.Replace(",", "", sqlText.Length - 1, 1);//remove the last character 
                sqlText.Append(" WHERE [ID]=@ID ");
            }
            if (sqlOperate == SqlOperate.Delete)
            {

            }

            return sqlText.ToString();
        }
    }
}
