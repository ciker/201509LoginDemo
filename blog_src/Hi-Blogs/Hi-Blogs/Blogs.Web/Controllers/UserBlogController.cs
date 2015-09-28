using Blogs.BLL.Common;
using Blogs.Common.Helper;
using Blogs.Helper;
using Blogs.Helper.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Blogs.Controllers
{
    /// <summary>
    /// 用户博客显示
    /// </summary>
    public class UserBlogController : Controller
    {
        static int sizePage = 20;

        #region 特殊页面 blogsid        
        /// <summary>
        /// 自定义 特殊页面的 的 KEY 和 blogid
        /// </summary>
        static Dictionary<string, int> MyPageId = new Dictionary<string, int>();

        #endregion

        static string admin = "admin";
        #region admin 用户id
        /// <summary>
        /// admin 用户id
        /// </summary>
        static int adminuserid = -1;
        #endregion

        int total;

        #region 01根据博客id 获取博客
        /// <summary>
        /// 根据博客id 获取博客
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="id">博客id</param>
        /// <returns></returns>
        public ActionResult UserBlog(string name, int id)
        {
            return View(GetUserBlog(name, id));
        }

        #region 根据博客id 获取博客(公用)
        /// <summary>
        /// 根据博客id 获取博客
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetUserBlog(string name, int id)
        {
            BLL.BlogsBLL blog = new BLL.BlogsBLL();

            #region 优化前
            //var blogobj = blog.GetList(t => t.Id == id && t.Users.UserName == name).FirstOrDefault();

            ////这里看 能不能只查询一次
            //var blogNext = blog.GetList(t => t.Id > id && t.Users.UserName == name).OrderBy(t => t.Id).FirstOrDefault();
            //var blogLast = blog.GetList(t => t.Id < id && t.Users.UserName == name).OrderBy(t => t.Id).FirstOrDefault(); 
            #endregion

            //优化后 只查一次数据库
            //比如id 为3 那么 last取3，4  next取2，3
            var last = blog.GetList(t => t.Id >= id && t.BlogUsersSet.UserName == name, isAsNoTracking: false, tableName: t => t.BlogUsersSet).OrderBy(t => t.Id).Take(2);
            var next = blog.GetList(t => t.Id <= id && t.BlogUsersSet.UserName == name, isAsNoTracking: false, tableName: t => t.BlogUsersSet).OrderByDescending(t => t.Id).Take(2);
            //去重复合并
            var blogUnion = (from c in last select c).Union(from a in next select a).ToList();

            var blogNext = blogUnion.Where(t => t.Id > id).FirstOrDefault();
            var blogLast = blogUnion.Where(t => t.Id < id).FirstOrDefault();
            var blogobj = blogUnion.Where(t => t.Id == id).FirstOrDefault();

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("blog", blogobj);
            dic.Add("blogNext", blogNext);
            dic.Add("blogLast", blogLast);
            dic.Add("contentBlogType", blogobj.BlogTypes.ToList());//当前博客所属的类型
            dic.Add("contentBlogTag", blogobj.BlogTags.ToList());//当前博客所有的tag标签

            //Description 网页描述
            var BlogContent = MyHtmlHelper.GetHtmlText(blogobj.BlogContent);
            BlogContent = BlogContent.Length >= 300 ? BlogContent.Substring(0, 300) : BlogContent;
            dic.Add("blogConText", BlogContent);

            SetDic(dic, name);

            #region 保存 标记 此文已经阅读过
            var BlogReadInfo = "BlogReadInfo";
            HttpCookie Cookie = CookiesHelper.GetCookie(BlogReadInfo);
            if (null == Cookie)
            {
                Cookie = new HttpCookie(BlogReadInfo);
                Cookie.Values.Add(blogobj.Id.ToString(), "true");
                //设置Cookie过期时间
                Cookie.Expires = DateTime.Now.AddHours(24);//一天 
                CookiesHelper.AddCookie(Cookie);
                //........................异步调用....................
                new SaveReadDelegate(SaveReadNum).BeginInvoke(blogobj, GetUserDistinguish(Request), null, null);
            }
            else
            {
                if (Cookie.Values[blogobj.Id.ToString()] == null || !Cookie.Values[blogobj.Id.ToString()].Equals("true"))
                {
                    CookiesHelper.SetCookie(BlogReadInfo, blogobj.Id.ToString(), "true");
                    //........................异步调用....................
                    new SaveReadDelegate(SaveReadNum).BeginInvoke(blogobj, GetUserDistinguish(Request), null, null);
                }
            }
            #endregion

            return dic;
        }
        #endregion

        #region 统计阅读量 异步调用方法
        delegate void SaveReadDelegate(ModelDB.Blogs blogobj, string md5);
        private void SaveReadNum(ModelDB.Blogs blogobj, string md5)
        {
            LogSave.TrackLogSave(GetUserDistinguish(Request, false), "ReadBlogLog");
            var isup = true;
            BLL.BlogsBLL blogbll = new BLL.BlogsBLL();
            var blogtemp = blogbll.GetList(t => t.Id == blogobj.Id, isAsNoTracking: false).FirstOrDefault();
            if (blogtemp.BlogReadNum == null)
                blogtemp.BlogReadNum = 1;
            else if (!IsRead(blogtemp, md5))
                blogtemp.BlogReadNum++;
            else
                isup = false;
            if (isup)
                BLL.BlogCommentSetBLL.StaticSave();
        }


        #region 获取客户端标识（伪）
        /// <summary>
        ///  获取客户端标识 用来判断是否已经阅读过此文章
        /// </summary>
        /// <param name="requestt"></param>
        /// <param name="IsMD5">是否已经md5加密</param>
        /// <returns></returns>
        private string GetUserDistinguish(HttpRequestBase requestt, bool IsMD5 = true)
        {
            //request
            StringBuilder str = new StringBuilder();
            string ip = "";
            if (requestt.ServerVariables.AllKeys.Contains("HTTP_X_FORWARDED_FOR") && requestt.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
                ip = requestt.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            else
                ip = requestt.ServerVariables.Get("Remote_Addr").ToString().Trim();
            str.Append("Ip:" + ip);
            str.Append("\r\n浏览器:" + requestt.Browser.Browser.ToString());
            str.Append("\r\n浏览器版本:" + requestt.Browser.MajorVersion.ToString());
            str.Append("\r\n操作系统:" + requestt.Browser.Platform.ToString());
            str.Append("\r\n页面：" + requestt.Url.ToString());
            //str.Append("客户端IP：" + requestt.UserHostAddress);
            str.Append("\r\n用户信息：" + User);
            str.Append("\r\n浏览器标识：" + requestt.Browser.Id);
            str.Append("\r\n浏览器版本号：" + requestt.Browser.Version);
            str.Append("\r\n浏览器是不是测试版本：" + requestt.Browser.Beta);
            //str.Append("<br/>浏览器的分辨率(像素)：" + Request["width"].ToString() + "*" + Request["height"].ToString());//1280/1024                        
            str.Append("\r\n是不是win16系统：" + requestt.Browser.Win16);
            str.Append("\r\n是不是win32系统：" + requestt.Browser.Win32);
            if (IsMD5)
                return str.ToString().GetMd5_16();
            else
                return str.ToString();
        }
        #endregion

        #region 判断是否阅读过 如果没有 这在BlogReadInfo 插入一条标识信息
        private bool IsRead(Blogs.ModelDB.Blogs blogobj, string md5)
        {
            if (blogobj.BlogReadInfo.Where(t => t.MD5 == md5 && t.LastTime.AddHours(24) > DateTime.Now).Count() > 0)
                return true;
            else
            {
                //BLL.
                blogobj.BlogReadInfo.Add(new Blogs.ModelDB.BlogReadInfo()
                {
                    MD5 = md5,
                    IsDel = false,
                    BlogsId = blogobj.Id,
                    CreateTime = DateTime.Now,
                    UpTime = DateTime.Now,
                    LastTime = DateTime.Now
                });
                return false;
            }
        }
        #endregion

        #endregion

        #endregion

        #region 02分页获取博客简介   注意：这里其实如果按ID排序的话 效率更高，
        //但是对于导入过来的博文，可能就无法显示为按博客发布日期排序了
        /// <summary>
        /// 分页获取博客简介(简单分页，后续如果数据量大 导致分页效率 再优化)
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="id">第几页</param>
        /// <returns></returns>
        [OutputCache(CacheProfile = "CacheUserIndexPage")]
        public ActionResult UserBlogList(string name, int? id)
        {
            //加不加 Response.Cache.SetOmitVaryStar(true)，服务端的缓存情况都是一样的。
            //只是不加 SetOmitVaryStar(true) 时，对于同一个客户端浏览器，每隔一次请求，服务器端就不管客户端浏览器的缓存，
            //重新发送页面内容，但是只要在缓存有效期内，内容还是从服务器端缓存中读取。
            //http://www.cnblogs.com/dudu/archive/2012/08/27/asp_net_mvc_outputcache.html
            Response.Cache.SetOmitVaryStar(true);
            int myid = id ?? 1;
            BLL.BlogsBLL blog = new BLL.BlogsBLL();
            var bloglist = blog.GetList<DateTime?>(myid, sizePage, out total, t => t.BlogUsersSet.UserName == name && t.IsShowMyHome == true, false, t => t.BlogCreateTime, false)
                .Select(t => new
                {
                    Id = t.Id,
                    BlogTitle = t.BlogTitle,
                    BlogContent = t.BlogContent,
                    BlogCreateTime = t.BlogCreateTime,
                    BlogReadNum = t.BlogReadNum,
                    BlogCommentNum = t.BlogCommentNum
                })
                .ToList()
                .Select(t => new ModelDB.Blogs()
                {
                    Id = t.Id,
                    BlogTitle = t.BlogTitle,
                    BlogContent = MyHtmlHelper.GetHtmlText(t.BlogContent),
                    BlogCreateTime = t.BlogCreateTime,
                    BlogReadNum = t.BlogReadNum,
                    BlogCommentNum = t.BlogCommentNum
                }).ToList();

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("blogList", bloglist);
            //dic.Add("blogUserName", name);
            dic.Add("total", total);
            SetDic(dic, name);
            return View(dic);
        }
        #endregion

        #region 03获取文章类型下的所以文章
        /// <summary>
        /// 获取文章类型下的所以文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OutputCache(CacheProfile = "CacheUserTypePage")]
        public ActionResult GetTypeBlogs(int id, string name, int? typeId)
        {
            Response.Cache.SetOmitVaryStar(true);
            BLL.BlogTypesBLL typebll = new BLL.BlogTypesBLL();
            var type = typebll.GetList(t => t.Id == typeId && t.BlogUsersSet.UserName == name).FirstOrDefault();

            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (type != null)
            {
                var listblog = type.Blogs.Where(t => t.IsDel == false).OrderByDescending(t => t.BlogCreateTime).Skip((id - 1) * sizePage).Take(sizePage)
                    .Select(t => new
                    {
                        BlogCreateTime = t.BlogCreateTime,
                        BlogContent = t.BlogContent,
                        BlogTitle = t.BlogTitle,
                        Id = t.Id,
                        BlogReadNum = t.BlogReadNum,
                        BlogCommentNum = t.BlogCommentNum
                    })
                    .ToList()
                    .Select(t => new ModelDB.Blogs()
                    {
                        Id = t.Id,
                        BlogTitle = t.BlogTitle,
                        BlogCreateTime = t.BlogCreateTime,
                        BlogContent = MyHtmlHelper.GetHtmlText(t.BlogContent),
                        BlogReadNum = t.BlogReadNum,
                        BlogCommentNum = t.BlogCommentNum
                    })
                    .ToList();
                dic.Add("blog", listblog);
                dic.Add("type", type.TypeName);

                int mcount = type.Blogs.Count();
                int total = (mcount / sizePage) + (mcount % sizePage > 0 ? 1 : 0);
                dic.Add("total", total);

                SetDic(dic, name);
                return View(dic);
            }
            return View();
        }
        #endregion

        #region 04获取标签下的所有文章
        /// <summary>
        /// 获取标签下的所有文章
        /// </summary>
        /// <param name="id">页码</param>
        /// <param name="name">用户名</param>
        /// <param name="typeId">tagID</param>
        /// <returns></returns>
        [OutputCache(CacheProfile = "CacheUserTagPage")]
        public ActionResult GetTagBlogs(int id, string name, int? typeId)
        {
            Response.Cache.SetOmitVaryStar(true);
            BLL.BlogTagsBLL tagbll = new BLL.BlogTagsBLL();
            var tag = tagbll.GetList(t => t.Id == typeId && t.BlogUsersSet.UserName == name).FirstOrDefault();

            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (tag != null)
            {
                //这里因为 是直接 分页查询        所以一定要记住 过滤 isdel
                var listblog = tag.Blogs.Where(t => t.IsDel == false).OrderByDescending(t => t.BlogCreateTime).Skip((id - 1) * sizePage).Take(sizePage)
                    .Select(t => new
                    {
                        BlogContent = t.BlogContent,
                        BlogCreateTime = t.BlogCreateTime,
                        BlogTitle = t.BlogTitle,
                        Id = t.Id,
                        BlogReadNum = t.BlogReadNum,
                        BlogCommentNum = t.BlogCommentNum
                    })
                   .ToList()
                   .Select(t => new ModelDB.Blogs()
                   {
                       Id = t.Id,
                       BlogTitle = t.BlogTitle,
                       BlogCreateTime = t.BlogCreateTime,
                       BlogContent = MyHtmlHelper.GetHtmlText(t.BlogContent),
                       BlogReadNum = t.BlogReadNum,
                       BlogCommentNum = t.BlogCommentNum
                   })
                   .ToList();
                dic.Add("blog", listblog);
                dic.Add("tag", tag.TagName);

                int mcount = tag.Blogs.Count();
                int total = (mcount / sizePage) + (mcount % sizePage > 0 ? 1 : 0);
                dic.Add("total", total);

                SetDic(dic, name);
                return View(dic);
            }
            return View();
        }
        #endregion

        #region 05特殊页面

        #region 自定义 特殊页面 公共方法
        /// <summary>
        /// 自定义 特殊页面 公共方法 (弃用)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="messid"></param>
        private void Forum(string str, ref int messid)
        {
            #region 检测是否存在 admin 用户

            if (adminuserid <= 0)
            {
                BLL.BlogUsersSetBLL userbll = new BLL.BlogUsersSetBLL();
                var user = userbll.GetList(t => t.UserName == admin).FirstOrDefault();
                if (null == user)
                {
                    var usertemp = new Blogs.ModelDB.BlogUsersSet()
                    {
                        UserName = admin,
                        UserPass = "admin".MD5().MD5(),
                        IsDel = false,
                        IsLock = false,
                        UserNickname = "",
                        UserInfo = new ModelDB.UserInfo()
                    };
                    userbll.Add(usertemp);
                    userbll.save(false);
                    adminuserid = usertemp.Id;
                }
                else
                    adminuserid = user.Id;
            }

            #endregion

            #region 是否存在 自定义 特殊页面 （返回 blogid）
            if (messid == 0)
            {
                BLL.BlogsBLL blogbll = new BLL.BlogsBLL();
                var blog = blogbll.GetList(t => t.BlogTitle == str).FirstOrDefault();
                if (null == blog)
                {
                    var blogtemp = new Blogs.ModelDB.Blogs()
                    {
                        Id = 0,
                        UsersId = adminuserid,
                        IsDel = false,
                        BlogTitle = str
                    };
                    blogbll.Add(blogtemp);
                    blogbll.save(false);
                    CacheData.GetAllUserInfo(true);//更新缓存
                    messid = blogtemp.Id;
                }
                else
                    messid = blog.Id;
            }
            #endregion

        }

        /// <summary>
        /// 自定义 特殊页面 公共方法
        /// </summary>
        /// <param name="str">BlogTitle名字</param>
        /// <param name="pageName">存在MyPageId的KEY名</param>
        private void Forum(string str, string pageName)
        {
            #region 检测是否存在 admin 用户

            if (adminuserid <= 0)
            {
                BLL.BlogUsersSetBLL userbll = new BLL.BlogUsersSetBLL();
                var user = userbll.GetList(t => t.UserName == admin).FirstOrDefault();
                if (null == user)
                {
                    var usertemp = new Blogs.ModelDB.BlogUsersSet()
                    {
                        UserName = admin,
                        UserPass = "admin".MD5().MD5(),
                        IsDel = false,
                        IsLock = false,
                        UserNickname = "",
                        UserInfo = new ModelDB.UserInfo()
                    };
                    userbll.Add(usertemp);
                    userbll.save(false);
                    adminuserid = usertemp.Id;
                }
                else
                    adminuserid = user.Id;
            }

            #endregion

            #region 是否存在 自定义 特殊页面 （返回 blogid）
            if (MyPageId[pageName] == 0)
            {
                BLL.BlogsBLL blogbll = new BLL.BlogsBLL();
                var blog = blogbll.GetList(t => t.BlogTitle == str).FirstOrDefault();
                if (null == blog)
                {
                    var blogtemp = new Blogs.ModelDB.Blogs()
                    {
                        Id = 0,
                        UsersId = adminuserid,
                        IsDel = false,
                        BlogTitle = str
                    };
                    blogbll.Add(blogtemp);
                    blogbll.save(false);
                    CacheData.GetAllUserInfo(true);//更新缓存
                    MyPageId[pageName] = blogtemp.Id;
                }
                else
                    MyPageId[pageName] = blog.Id;
            }
            #endregion

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ikey"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        private ActionResult MyPage(string ikey, string title)
        {
            var key = ikey;
            var blogTitle = title;
            if (!MyPageId.Keys.Contains(key))
                MyPageId.Add(key, 0);
            Forum(blogTitle, key);
            ViewBag.blogid = MyPageId[key]; //blogid            
            return View("Message", GetUserBlog(admin, MyPageId[key]));
        }
        #endregion

        #region 05.1留言板
        /// <summary>
        /// 留言板
        /// </summary>
        /// <returns></returns>
        /// 
        [ValidateInput(false)]
        public ActionResult Message()
        {           
            //var key = "MessageId";
            //var blogTitle = "留言板";
            //if (!MyPageId.Keys.Contains(key))
            //    MyPageId.Add(key, 0);
            //Forum(blogTitle, key);
            //ViewBag.blogid = MyPageId[key]; //blogid            
            //return View("Message", GetUserBlog(admin, MyPageId[key]));
            return MyPage("MessageId", "留言板");
        }
        #endregion 

        #region 05.2友情链接
        /// <summary>
        /// 友情链接
        /// </summary>
        /// <returns></returns>
        /// 
        [ValidateInput(false)]
        public ActionResult FriendlyLink()
        {          
            //var key = "FriendlyLinkId";
            //var blogTitle = "友情链接";
            //if (!MyPageId.Keys.Contains(key))
            //    MyPageId.Add(key, 0);
            //Forum(blogTitle, key);
            //ViewBag.blogid = MyPageId[key]; //blogid            
            //return View("Message", GetUserBlog(admin, MyPageId[key]));
            return MyPage("FriendlyLinkId", "友情链接");
        }
        #endregion

        #region 05.3bug提交
        /// <summary>
        /// bug提交
        /// </summary>
        /// <returns></returns>
        public ActionResult BUG()
        {          
            //var key = "BugId";
            //var blogTitle = "需求bug记录";
            //if (!MyPageId.Keys.Contains(key))
            //    MyPageId.Add(key, 0);
            //Forum(blogTitle, key);
            //ViewBag.blogid = MyPageId[key]; //blogid            
            //return View("Message", GetUserBlog(admin, MyPageId[key]));
            return MyPage("BugId", "需求bug记录");
        }
        #endregion

        #region 05.4WebAPI
        /// <summary>
        /// WebAPI
        /// </summary>
        /// <returns></returns>
        public ActionResult WebAPI()
        {
            //var key = "WebAPI";
            //var blogTitle = "WebAPI";
            //if (!MyPageId.Keys.Contains(key))
            //    MyPageId.Add(key, 0);
            //Forum(blogTitle, key);
            //ViewBag.blogid = MyPageId[key]; //blogid            
            //return View("Message", GetUserBlog(admin, MyPageId[key]));
            return MyPage("WebAPI", "WebAPI");
        }
        #endregion

        #region 05.5版本下载
        /// <summary>
        /// 版本下载
        /// </summary>
        /// <returns></returns>
        public ActionResult Download()
        {           
            return MyPage("Download", "版本下载");
        }
        #endregion 

        #endregion

        #region 公共数据存储
        /// <summary>
        /// 公共数据存储(从数据缓存中取 如何以后数据量大的话 再考虑是否实时查询)
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="name"></param>
        private void SetDic(Dictionary<string, object> dic, string name)
        {
            var user = CacheData.GetAllUserInfo().FirstOrDefault(t => t.UserName == name);
            //dic.Add("blogName", name);
            dic.Add(Constant.blogUser, user);
            dic.Add(Constant.userBlogTag, GetDataHelper.GetAllTag(user.Id).ToList());
            dic.Add(Constant.userBlogType, CacheData.GetAllType().Where(t => t.UsersId == user.Id).ToList());
            dic.Add(Constant.SessionUser, BLL.Common.BLLSession.UserInfoSessioin);
        }
        #endregion
    }
}
