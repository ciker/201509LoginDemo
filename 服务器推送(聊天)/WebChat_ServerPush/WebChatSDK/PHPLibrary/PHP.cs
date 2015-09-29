using System;
using System.Collections.Generic;
using System.Text;

namespace WebChatSDK.PHPLibrary
{
    public static class PHP
    {
        public static PHPArray array(params object[] objs)
        {
            PHPArray ary = new PHPArray();
            foreach (object obj in objs)
            {
                ary.Add(obj);
            }
            return ary;
        }

        public static PHPArray array(string[] keys, object[] objs)
        {
            if (keys.Length != objs.Length)
            {
                throw new ArgumentException("数组数量不一样");
            }
            PHPArray ary = new PHPArray();
            for (int i = 0; i < keys.Length; i++)
            {
                ary.Add(keys[i], objs[i]);
            }
            return ary;
        }

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="strPath">文件路径</param>
        /// <param name="msg">内容</param>
        public static void DeBug(string strPath, object msg)
        {
            System.IO.StreamWriter sw = null;
            System.IO.FileStream fs = new System.IO.FileStream(strPath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.Read);
            fs.Seek(0, System.IO.SeekOrigin.End);
            sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
            string LineText = DateTime.Now.ToString() + ", " + msg.ToString();
            sw.WriteLine(LineText);
            sw.Close();
            sw = null;
            fs.Close();
            fs = null;
        }

        /// <summary>
        /// 重复字符串
        /// </summary>
        /// <param name="peat">重复的字符串</param>
        /// <param name="times">重复次数</param>
        /// <returns></returns>
        public static string str_repeat(string peat, int times)
        {
            if (times == 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < times; i++)
            {
                sb.Append(peat);
            }
            return sb.ToString();
        }

        #region print_r
        public static void print_r(PHPArray dic)
        {
            print_r(dic, false);
        }
        public static string print_r(PHPArray dic, bool @return)
        {
            string str = print_r(dic, 0);
            if (!@return)
            {
                Console.WriteLine(str);
            }
            return str;
        }

        private static string print_r(PHPArray dic, int depth)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Array");
            sb.AppendLine(print_r_tag(depth) + "(");
            if (dic != null)
            {
                foreach (KeyValuePair<object, object> entry in dic)
                {
                    object key = entry.Key;
                    object val = entry.Value;
                    if (val is PHPArray)
                    {
                        sb.Append(print_r_tag(depth + 1));
                        sb.Append("[");
                        sb.Append(key);
                        sb.Append("] => ");
                        sb.AppendLine(print_r(val as PHPArray, depth + 2));
                    }
                    else
                    {
                        sb.Append(print_r_tag(depth + 1));
                        sb.Append("[");
                        sb.Append(key);
                        sb.Append("] => ");
                        sb.AppendLine(val.ToString());
                    }
                }
            }
            sb.Append(print_r_tag(depth) + ")");
            return sb.ToString();
        }

        private static string print_r_tag(int spaces)
        {
            StringBuilder sb = new StringBuilder();
            int i = 1;
            while (i <= spaces)
            {
                sb.Append("    ");
                i++;
            }
            return sb.ToString();
        }
        #endregion
    }
}
