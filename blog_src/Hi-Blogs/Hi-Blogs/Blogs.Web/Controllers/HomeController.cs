using Blogs.BLL.Common;
using Blogs.Common.Helper;
using Blogs.ModelDB;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogs.Controllers
{
    /// <summary>
    /// 主页
    /// 后续更新请关注博客：http://www.cnblogs.com/zhaopei/p/4737958.html
    /// </summary> 
    public class HomeController : Controller
    {
        static int sizePage = 20;

        #region 01 数据初始化
        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <returns></returns>
        [OutputCache(CacheProfile = "CachePu")]
        public ActionResult Index(int? id)
        {
            //
            int total;
            Response.Cache.SetOmitVaryStar(true);
            id = id ?? 1;
            int idex = int.Parse(id.ToString());
            BLL.BlogsBLL blog = new BLL.BlogsBLL();
            var bloglist = blog.GetList<DateTime?>(idex, sizePage, out total, t => t.IsShowHome == true, false, t => t.BlogCreateTime, false)
                .Select(t => new
                {
                    Id = t.Id,
                    BlogTitle = t.BlogTitle,
                    BlogContent = t.BlogContent,
                    UserName = t.BlogUsersSet.UserName,
                    UserNickname = t.BlogUsersSet.UserNickname,
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
                    BlogUsersSet = new ModelDB.BlogUsersSet()
                    {
                        UserName = t.UserName,
                        UserNickname = t.UserNickname
                    },
                    BlogReadNum = t.BlogReadNum,
                    BlogCommentNum = t.BlogCommentNum
                }).ToList();

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("blog", bloglist);
            dic.Add("users", CacheData.GetAllUserInfo().Where(t => t.IsLock == false).ToList());
            dic.Add("SessionUser", BLL.Common.BLLSession.UserInfoSessioin);
            dic.Add("total", total);
            return View(dic);
        }
        #endregion

        #region 02获取博客 用户登录信息
        /// <summary>
        /// 获取博客 用户登录信息
        /// </summary>
        /// <returns></returns>
        public ActionResult BlogHead()
        {
            return PartialView(BLLSession.UserInfoSessioin);
        }
        #endregion

        public ActionResult Path404()
        {
            return View();
        }

        #region 注释
        ///// <summary>
        ///// 读取文章内容
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public ActionResult p(int id)
        //{
        //    BLL.BlogsBLL blog = new BLL.BlogsBLL();
        //    var blogobj = blog.GetList(t => t.Id == id).FirstOrDefault();

        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    dic.Add("blog", blogobj);
        //    dic.Add("blogType", GetBlogHelper.GetAllType());
        //    dic.Add("blogTag", GetBlogHelper.GetAllTag());

        //    return View(dic);
        //}

        ///// <summary>
        ///// 获取文章类型下的所以文章
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public ActionResult GetTypeBlogs(int? id)
        //{
        //    if (null == id)
        //        return View();
        //    BLL.BlogTypesBLL typebll = new BLL.BlogTypesBLL();
        //    var type = typebll.GetList(t => t.Id == id).FirstOrDefault();

        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    if (type != null)
        //    {
        //        var listblog = type.Blogs.Select(t => new { BlogContent = t.BlogContent, BlogTitle = t.BlogTitle, Id = t.Id })
        //            .ToList()
        //            .Select(t => new Model.Blogs() { Id = t.Id, BlogTitle = t.BlogTitle, BlogContent = MyHtmlHelper.GetHtmlText(t.BlogContent) })
        //            .ToList();
        //        dic.Add("blog", listblog);
        //        dic.Add("type", type.TypeName);
        //        dic.Add("blogType", GetBlogHelper.GetAllType());
        //        dic.Add("blogTag", GetBlogHelper.GetAllTag());
        //        return View(dic);
        //    }
        //    return View();
        //}

        ///// <summary>
        ///// 获取标签下的所有文章
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public ActionResult GetTagBlogs(int? id)
        //{
        //    if (null == id)
        //        return View();
        //    BLL.BlogTagsBLL tagbll = new BLL.BlogTagsBLL();
        //    var tag = tagbll.GetList(t => t.Id == id).FirstOrDefault();

        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    if (tag != null)
        //    {
        //        var listblog = tag.Blogs.Select(t => new { BlogContent = t.BlogContent, BlogTitle = t.BlogTitle, Id = t.Id })
        //           .ToList()
        //           .Select(t => new Model.Blogs() { Id = t.Id, BlogTitle = t.BlogTitle, BlogContent = MyHtmlHelper.GetHtmlText(t.BlogContent) })
        //           .ToList();
        //        dic.Add("blog", listblog);
        //        dic.Add("tag", tag.TagName);
        //        dic.Add("blogType", GetBlogHelper.GetAllType());
        //        dic.Add("blogTag", GetBlogHelper.GetAllTag());
        //        return View(dic);
        //    }
        //    return View();
        //} 
        #endregion
    }
}
