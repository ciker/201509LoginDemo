using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace Blogs.Helper
{
    /// <summary>
    /// 文件操作辅助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 默认路径（网站根目录） 最后已经带了"\"
        /// </summary>
        public static string defaultpath;// = HttpContext.Current.Server.MapPath("~/");

        #region 00 编码格式
        /// <summary>
        /// 编码格式
        /// </summary>
        public static Encoding encoding = Encoding.Default;
        #endregion

        #region 01 文件是否被占用 +static bool FileIsTake(string FileFullName)
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);

        /// <summary>
        /// 文件是否被占用
        /// </summary>
        /// <param name="FileFullName"></param>
        /// <returns></returns>
        public static bool FileIsTake(string FileFullName)
        {
            if (!File.Exists(FileFullName))
                return false;  //文件不存在
            IntPtr vHandle = _lopen(FileFullName, OF_READWRITE | OF_SHARE_DENY_NONE);
            if (vHandle == HFILE_ERROR)
                return true;//文件被占用！            
            CloseHandle(vHandle);
            return false;
        }
        #endregion

        #region 02 判断是否是文件
        /// <summary>
        /// 判断是否是文件(文件是否存在)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool isFile(string path)
        {

            if (File.Exists(path))
            {
                return true;//是文件
            }
            return false;
            //else if (Directory.Exists(path))
            //{
            //    // 是文件夹
            //}
            //else
            //{
            //    // 都不是
            //}
        }
        #endregion

        #region 02 创建路径
        /// <summary>
        /// 创建路径 如果存在 return false
        /// </summary>
        /// <param name="paht"></param>
        public static bool CreatePath(string paht)
        {
            if (!Directory.Exists(paht))
            {
                Directory.CreateDirectory(paht);
                return true;
            }
            return false;
        }
        #endregion

        #region 03 记录数据到文件  覆盖或追加 （如果不存在 则创建）
        /// <summary>
        /// 记录数据  覆盖或追加
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <param name="FileName">文件名称</param>
        /// <param name="str"></param>
        /// <param name="Cover">如果文件存 true:覆盖 false：追加</param>
        public static void SaveFile(string FilePath, string FileName, string str, bool Cover = true)
        {
            try
            {
                CreatePath(FilePath);
                if (Cover)
                    File.WriteAllText(FilePath + FileName, str, encoding);
                else
                    File.AppendAllText(FilePath + FileName, str, encoding);
            }
            catch (Exception)
            { }
        }
        #endregion

        #region 04 获取 目录下的  子目录或文件 【state 1：目录 2：文件 3：目录和文件】
        /// <summary>
        /// 获取 目录下的  子目录或文件 【state 1：目录 2：文件 3：目录和文件】
        /// </summary>
        /// <param name="path"></param>
        /// <param name="state"> 1：目录 2：文件 3：目录和文件</param>
        /// <returns></returns>
        public static List<string[]> FileSystemInfo(string path, string state, string noDir = "")
        {
            List<string[]> list = new List<string[]>();
            DirectoryInfo dir = new DirectoryInfo(path);
            #region 目录
            if (state == "1")
            {
                foreach (DirectoryInfo item in dir.GetDirectories())
                {
                    list.Add(
                        new string[] {
                        item.Name,
                        item.FullName,
                        item.CreationTime.ToString() });
                }
            }
            #endregion

            #region 文件
            else if (state == "2")
            {
                foreach (FileInfo item in dir.GetFiles())
                {

                    list.Add(
                        new string[] {
                        item.Name,
                        item.FullName,
                        item.CreationTime.ToString()});
                }
            }
            #endregion

            #region 目录和文件
            else if (state == "3")
            {
                if (Directory.Exists(path))
                {
                    foreach (FileSystemInfo item in dir.GetFileSystemInfos())
                    {
                        list.Add(
                            new string[] {
                        item.Name,
                        item.FullName,
                        item.CreationTime.ToString()
                        //,
                        ////Ticks 用了统计点击数量  （没办法 没有找到可以设置的属性  将就着用着先）
                        //item.CreationTime.Ticks.ToString()
                            });
                    }
                }
            }
            #endregion

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                {
                    var aa = "";
                }
            }

            list.Sort(delegate(string[] str1, string[] str2)
            {
                if (DateTime.Parse(str1[2]) < DateTime.Parse(str2[2]))
                    return -1;
                else
                    return 1;
            });

            // list.Reverse();

            return list;
        }
        #endregion

        #region 读取文件里面的内容
        /// <summary>
        /// 读取文件里面的内容
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string GetFile(string FilePath, string FileName)
        {
            if (isFile(FilePath + FileName))
            {
                return File.ReadAllText(FilePath + FileName, encoding);
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        // public static bool is
    }
}
