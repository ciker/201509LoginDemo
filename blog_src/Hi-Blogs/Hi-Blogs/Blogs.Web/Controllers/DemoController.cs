using Blogs.Common.CustomModel;
using Blogs.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Blogs.Controllers
{
    public class DemoController : Controller
    {
        /// <summary>
        /// 统计访问量
        /// </summary>
        /// <returns></returns>
        public string TrafficStatistics()
        {

            #region MyRegion
            //string ip;
            //if (Request.ServerVariables["HTTP_VIA"] != null) // using proxy
            //{
            //    ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();  // Return real client IP.
            //}
            //else// not using proxy or can't get the Client IP
            //{
            //    ip = Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.
            //}

            ////            当前电脑名：static System.Environment.MachineName 
            ////当前电脑所属网域：static System.Environment.UserDomainName 
            ////当前电脑用户：static System.Environment.UserName 

            //string str = "ip:" + ip + "</br>" +
            //    "Request.UserHostName:" + Request.UserHostName + "</br>" +
            //    "当前电脑名:" + System.Environment.MachineName + "</br>" +
            //    "当前电脑所属网域:" + System.Environment.UserDomainName + "</br>" +
            //    "当前电脑用户:" + System.Environment.UserName+ "</br>" +
            //  ""
            //    ;

            ////if (!string.IsNullOrEmpty(str) && IsIP(str))
            ////{
            ////    return str;
            ////}
            ////return "127.0.0.1";

            //return str; 
            #endregion

            //List<ModelDB.BlogComment> list = new List<ModelDB.BlogComment>();
            //list.Add(new BlogComment() { Id = 1, BlogUsersId = 3, ReplyUserID = 3, Content = "测试1", IsInitial = true, CommentID = null });
            //list.Add(new BlogComment() { Id = 2, BlogUsersId = 5, ReplyUserID = 3, Content = "测试2", IsInitial = false, CommentID = 1 });
            //list.Add(new BlogComment() { Id = 3, BlogUsersId = 4, ReplyUserID = 3, Content = "测试3", IsInitial = false, CommentID = 1 });
            //list.Add(new BlogComment() { Id = 4, BlogUsersId = 6, ReplyUserID = 5, Content = "测试4", IsInitial = false, CommentID = 1 });
            //list.Add(new BlogComment() { Id = 5, BlogUsersId = 3, ReplyUserID = 4, Content = "测试5", IsInitial = false, CommentID = 1 });
            //list.Add(new BlogComment() { Id = 6, BlogUsersId = 4, ReplyUserID = 6, Content = "测试6", IsInitial = false, CommentID = 7 });
            //list.Add(new BlogComment() { Id = 7, BlogUsersId = 5, ReplyUserID = 3, Content = "测试7", IsInitial = true, CommentID = null });
            //list.Add(new BlogComment() { Id = 8, BlogUsersId = 6, ReplyUserID = 4, Content = "测试8", IsInitial = false, CommentID = 7 });
            //list.Add(new BlogComment() { Id = 9, BlogUsersId = 4, ReplyUserID = 5, Content = "测试9", IsInitial = false, CommentID = 7 });

            //List<List<BlogComment>> m = new List<List<BlogComment>>();
            //var ini = list.Where(t => t.IsInitial == true).ToList();
            //for (int i = 0; i < ini.Count; i++)
            //{
            //    m.Add(Get(ini[i], list));
            //}

            var bc = Request.Browser;// Request.Browser;
            var list = string.Empty;

            list = "";
            list += "操作系统：" + bc.Platform + "<br>";
            list += "是否是 Win16 系统：" + bc.Win16 + "<br>";
            list += "是否是 Win32 系统：" + bc.Win32 + "<br>";
            list += "---<br>";

            list += "浏览器：" + bc.Browser + "<br>";
            list += "浏览器标识：" + bc.Id + "<br>";
            list += "浏览器版本：" + bc.Version + "<br>";
            list += "浏览器 MajorVersion：" + bc.MajorVersion.ToString() + "<br>";
            list += "浏览器 MinorVersion：" + bc.MinorVersion.ToString() + "<br>";
            list += "浏览器是否是测试版本：" + bc.Beta.ToString() + "<br>";
            list += "是否是 America Online 浏览器：" + bc.AOL + "<br>";
            list += "客户端安装的 .NET Framework 版本：" + bc.ClrVersion + "<br>"; //即使安装了 .NET Framework，如果不是 IE 浏览器，检测版本都是 0.0。
            list += "是否是搜索引擎的网络爬虫：" + bc.Crawler + "<br>";
            list += "是否是移动设备：" + bc.IsMobileDevice + "<br>";
            list += "---<br>";

            list += "显示的颜色深度：" + bc.ScreenBitDepth + "<br>";
            list += "显示的近似宽度（以字符行为单位）：" + bc.ScreenCharactersWidth + "<br>";
            list += "显示的近似高度（以字符行为单位）：" + bc.ScreenCharactersHeight + "<br>";
            list += "显示的近似宽度（以像素行为单位）：" + bc.ScreenPixelsWidth + "<br>";
            list += "显示的近似高度（以像素行为单位）：" + bc.ScreenPixelsHeight + "<br>";
            list += "---<br>";

            list += "是否支持 CSS：" + bc.SupportsCss + "<br>";
            list += "是否支持 ActiveX 控件：" + bc.ActiveXControls.ToString() + "<br>";
            list += "是否支持 JavaApplets：" + bc.JavaApplets.ToString() + "<br>";
            //list += "是否支持 JavaScript：" + bc.JavaScript.ToString() + "<br>";
            list += "JScriptVersion：" + bc.JScriptVersion.ToString() + "<br>";
            list += "是否支持 VBScript：" + bc.VBScript.ToString() + "<br>";
            list += "是否支持 Cookies：" + bc.Cookies + "<br>";
            list += "支持的 MSHTML 的 DOM 版本：" + bc.MSDomVersion + "<br>";
            list += "支持的 W3C 的 DOM 版本：" + bc.W3CDomVersion + "<br>";
            list += "是否支持通过 HTTP 接收 XML：" + bc.SupportsXmlHttp + "<br>";
            list += "是否支持框架：" + bc.Frames.ToString() + "<br>";
            list += "超链接 a 属性 href 值的最大长度：" + bc.MaximumHrefLength + "<br>";
            list += "是否支持表格：" + bc.Tables + "<br>";


            return list;

        }


        public List<BlogCommentSet> Get(BlogCommentSet com, List<BlogCommentSet> list)
        {
            var li = list.Where(t => t.CommentID == com.Id).ToList();
            li.Insert(0, com);
            return li;
        }

        [HttpGet]
        public ActionResult mytest(int? id)
        {
            return View();
        }

        [HttpPost]
        public JsonResult mytest(BlogUsersSet blog)
        {
            JsonResult json = new JsonResult();
            bool isLogin = BLL.Common.CacheData.GetAllUserInfo().Where(t => t.Id == blog.Id && t.UserPass == blog.UserPass.MD5().MD5()).Count() > 0;
            if (isLogin)
            {
                json.Data = new JSData() { JSurl = "/", Messg = "登录成功~", State = EnumState.成功 };
            }
            else
            {
                json.Data = new JSData() { Messg = "登录失败~", State = EnumState.失败 };
            }
            return json;
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
