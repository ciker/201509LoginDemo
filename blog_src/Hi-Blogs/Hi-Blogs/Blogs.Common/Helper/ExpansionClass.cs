using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace System
{
    /// <summary>
    /// 扩展类
    /// </summary>
    public static class ExpansionClass
    {
        #region 关闭进程
        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="t"></param>
        public static void ThreadClose(this Thread t)
        {
            if (t != null)
            {
                try
                {
                    t.Abort();
                    t.DisableComObjectEagerCleanup();
                    t = null;
                }
                catch (Exception)
                { }

            }
        }
        #endregion

        #region 把 字符串集合 合并到一个字符串中 用逗号分割
        /// <summary>
        /// 把 字符串集合 合并到一个字符串中 用逗号分割
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string MyToString(this List<string> t)
        {
            string str = string.Empty;
            for (int i = 0; i < t.Count; i++)
            {
                if (t.Count > 1 && i < t.Count - 1)
                {
                    str += t[i] + ",";
                }
                else
                    str += t[i];
            }
            return str;
        }
        #endregion

        #region  用于 EF 查询语句 字符串条件筛选
        /// <summary>
        /// 用于 EF 查询语句 字符串条件筛选 
        /// 因为.Net字符串是Unicode格式 在数据库 执行时 会进行数据转换，
        /// 也就是说如果你在表中建立了索引此时会失效代替的是造成全表扫描
        /// </summary>
        /// <param name="str"></param>
        /// <param name="EF"></param>
        /// <returns></returns>
        public static string AsNonUnicode(this string str)
        {
            int EF = 6;
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            if (EF < 6)
                return EntityFunctions.AsNonUnicode(str);
            else
                return DbFunctions.AsNonUnicode(str);
        }
        #endregion

        #region MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            #region MD5实现方式一
            //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //byte[] bytValue, bytHash;
            //bytValue = System.Text.Encoding.UTF8.GetBytes(str);
            //bytHash = md5.ComputeHash(bytValue);
            //md5.Clear();
            //string sTemp = "";
            //for (int i = 0; i < bytHash.Length; i++)
            //{
            //    sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            //} 
            #endregion
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }

        /// <summary>
        /// 16位 MD5加密(小写)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5_16(this string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string a = BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str)), 4, 8);
            a = a.Replace("-", "").ToLower();
            return a;
        }

        /// <summary>
        /// 32位 MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5_32(this string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string a = BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str)));
            a = a.Replace("-", "");
            return a;
        }

        #endregion

        #region 序列号对象
        /// <summary>
        /// 序列号对象
        /// </summary>
        /// <param name="myclass"></param>
        /// <returns></returns>
        public static string ToJson(this object myclass)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(myclass);
        }
        #endregion

        #region 字符串的扩展方法
        /// <summary>
        /// 转DateTime 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? MyToDateTime(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;
            else
                return DateTime.Parse(str);
        }

        /// <summary>
        /// 转double
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double MyToDouble(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return -1;
            else
                return double.Parse(str);
        }

        /// <summary>
        /// 转int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int MyToInt(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return -1;
            else
                return int.Parse(str);
        }
        #endregion

        #region 深度复制 通过序列化 需要 在序列对象 添加 [Serializable()]

        // 利用反射实现深拷贝
        #region 在 有循环引用 对象   不可以用
        public static T DeepCopyWithReflection<T>(T obj)
        {
            Type type = obj.GetType();

            // 如果是字符串或值类型则直接返回
            if (obj is string || type.IsValueType) return obj;

            if (type.IsArray)
            {
                Type elementType = Type.GetType(type.FullName.Replace("[]", string.Empty));
                var array = obj as Array;
                Array copied = Array.CreateInstance(elementType, array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    copied.SetValue(DeepCopyWithReflection(array.GetValue(i)), i);
                }

                return (T)Convert.ChangeType(copied, obj.GetType());
            }

            object retval = Activator.CreateInstance(obj.GetType());

            PropertyInfo[] properties = obj.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Instance | BindingFlags.Static);
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(obj, null);
                if (propertyValue == null)
                    continue;
                property.SetValue(retval, DeepCopyWithReflection(propertyValue), null);
            }

            return (T)retval;
        }
        #endregion

        #region 利用反射实现深拷贝 DeepCopyWith (有循环引用 对象   可以用)
        // 用一个字典来存放每个对象的反射次数来避免反射代码的循环递归
        static Dictionary<Type, int> typereflectionCountDic = new Dictionary<Type, int>();
        static object DeepCopyDemoClasstypeRef = null;

        private static T DeepCopyWithReflection_Second<T>(T obj)
        {
            Type type = obj.GetType();

            // 如果是字符串或值类型则直接返回
            if (obj is string || type.IsValueType) return obj;

            if (type.IsArray)
            {
                Type elementType = Type.GetType(type.FullName.Replace("[]", string.Empty));
                var array = obj as Array;
                Array copied = Array.CreateInstance(elementType, array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    copied.SetValue(DeepCopyWithReflection_Second(array.GetValue(i)), i);
                }

                return (T)Convert.ChangeType(copied, obj.GetType());
            }

            // 对于类类型开始记录对象反射的次数
            int reflectionCount = Add(typereflectionCountDic, obj.GetType());
            if (reflectionCount > 1)
                return obj;

            object retval = Activator.CreateInstance(obj.GetType());

            PropertyInfo[] properties = obj.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Instance | BindingFlags.Static);
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(obj, null);
                if (propertyValue == null)
                    continue;
                property.SetValue(retval, DeepCopyWithReflection_Second(propertyValue), null);
            }

            return (T)retval;
        }

        /// <summary>
        /// 利用反射实现深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CopyReflectionData<T>(this T obj)
        {
            Type type = obj.GetType();

            // 如果是字符串或值类型则直接返回
            if (obj is string || type.IsValueType) return obj;

            if (type.IsArray)
            {
                Type elementType = Type.GetType(type.FullName.Replace("[]", string.Empty));
                var array = obj as Array;
                Array copied = Array.CreateInstance(elementType, array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    copied.SetValue(DeepCopyWithReflection_Second(array.GetValue(i)), i);
                }

                return (T)Convert.ChangeType(copied, obj.GetType());
            }

            int reflectionCount = Add(typereflectionCountDic, obj.GetType());
            if (reflectionCount > 1 && obj.GetType() == typeof(T))
                return (T)DeepCopyDemoClasstypeRef; // 返回deepCopyClassB对象

            object retval = Activator.CreateInstance(obj.GetType());

            if (retval.GetType() == typeof(T))
                DeepCopyDemoClasstypeRef = retval; // 保存一开始创建的DeepCopyDemoClass对象

            PropertyInfo[] properties = obj.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Instance | BindingFlags.Static);
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(obj, null);
                if (propertyValue == null)
                    continue;
                property.SetValue(retval, CopyReflectionData(propertyValue), null);
            }

            return (T)retval;
        }

        private static T SetArrayObject<T>(T arrayObj)
        {
            Type elementType = Type.GetType(arrayObj.GetType().FullName.Replace("[]", string.Empty));
            var array = arrayObj as Array;
            Array copied = Array.CreateInstance(elementType, array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                copied.SetValue(CopyReflectionData(array.GetValue(i)), i);
            }

            return (T)Convert.ChangeType(copied, arrayObj.GetType());
        }

        private static int Add(Dictionary<Type, int> dict, Type key)
        {
            if (key.Equals(typeof(String)) || key.IsValueType) return 0;
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, 1);
                return dict[key];
            }

            dict[key] += 1;
            return dict[key];
        }

        #endregion

        #region 利用XML序列化和反序列化实现
        /// <summary>
        /// 利用XML序列化和反序列化实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CopyXmlSerializerData<T>(this T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = xml.Deserialize(ms);
                ms.Close();
            }

            return (T)retval;
        }
        #endregion

        #region 利用二进制序列化和反序列实现
        /// <summary>
        /// 利用二进制序列化和反序列实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CopyBinarySerializeData<T>(this T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                // 序列化成流
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                // 反序列化成对象
                retval = bf.Deserialize(ms);
                ms.Close();
            }

            return (T)retval;
        }
        #endregion

        #region 利用DataContractSerializer序列化和反序列化实现
        /// <summary>
        /// 利用DataContractSerializer序列化和反序列化实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CopyContractSerializeData<T>(this T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(T));
                ser.WriteObject(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = ser.ReadObject(ms);
                ms.Close();
            }
            return (T)retval;
        }
        #endregion
        #endregion
    }
}
