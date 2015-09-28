using Blogs.BLL.Common;
using Blogs.ModelDB;
using Ivony.Html.Parser;
using Ivony.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Blogs.BLL;



namespace Blogs.Web.Controllers
{
    /// <summary>
    /// 转发插件
    /// </summary>
    public class PlugInUnitController : Controller
    {

        static string siteUrl = string.Empty;// "blog.haojima.net";

        /// <summary>
        /// 站内搜索地址
        /// </summary>
        public string GetSiteUrl()
        {
            if (string.IsNullOrEmpty(siteUrl))
                siteUrl = Request.Url.Host;
            return siteUrl;
        }

        /// <summary>
        /// 登录
        /// </summary>
        public void Login()
        {
            var data = Request.QueryString["mydata"];
            string callback = Request.QueryString["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, string> dic = jss.Deserialize<Dictionary<string, string>>(data);
            var name = dic["username"].Trim();
            var pass = dic["password"].Trim();
            var userinfo = CacheData.GetAllUserInfo().Where(t => t.UserName == name && t.UserPass == pass.MD5().MD5()).FirstOrDefault();
            object tyeList = null;
            if (userinfo != null)
                tyeList = CacheData.GetAllType().Where(t => t.UsersId == userinfo.Id).Select(t => new
                {
                    TypeName = t.TypeName,
                    Id = t.Id
                }).ToList();
            var cc = callback + "('" + tyeList.ToJson() + "')";
            Response.ContentType = "application/json";
            Response.Write(cc);
        }

        /// <summary>
        /// 转发
        /// </summary>
        public void Forward()
        {
            Response.ContentType = "application/json";
            var ResultValue = string.Empty;
            var data = Request.QueryString["mydata"];
            string callback = Request.QueryString["callback"];
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, string> dic = jss.Deserialize<Dictionary<string, string>>(data);
            var name = dic["username"].Trim();
            var pass = dic["password"].Trim();
            var userinfo = CacheData.GetAllUserInfo().Where(t => t.UserName == name && t.UserPass == pass.MD5().MD5()).FirstOrDefault();
            object tyeList = null;
            if (userinfo != null)
            {
                var tag = dic["tag"].Trim();
                var type = dic["type"].Trim();
                var url = dic["url"].Trim();
                int typeint = -1;
                int.TryParse(type, out typeint);
                var tags = tag.Split(',');

                var jp = new JumonyParser();
                var html = jp.LoadDocument(url);
                var titlehtml = html.Find(".postTitle a").FirstOrDefault().InnerHtml();
                titlehtml = "【转】" + titlehtml;
                var bodyhtml = html.Find("#cnblogs_post_body").FirstOrDefault().InnerHtml();
                bodyhtml += "</br><div class='div_zf'>==================================<a  href='" + url + "' target='_blank'>原文链接</a>==================================</div>";

                var mtag = BLL.Common.GetDataHelper.GetAllTag().Where(t => tags.Contains(t.TagName)).ToList();

                var blogtagid = new List<int>();
                for (int i = 0; i < tags.Length; i++)
                {
                    blogtagid.Add(this.GetTagId(tags[i], userinfo.Id));
                }
                //&& t.UsersId == userinfo.Id         理论是不用 加用户id 筛选
                var myBlogTags = new BlogTagsBLL().GetList(t => blogtagid.Contains(t.Id), isAsNoTracking: false).ToList();
                var myBlogTypes = new BLL.BlogTypesBLL().GetList(t => t.Id == typeint, isAsNoTracking: false).ToList();

                object obj = null;
                string call = string.Empty;
                BLL.BlogsBLL blogbll = new BLL.BlogsBLL();
                var blogtitle = blogbll.GetList(t => t.UsersId == userinfo.Id).OrderByDescending(t => t.Id).FirstOrDefault().BlogTitle;
                if (blogtitle == titlehtml)
                {
                    obj = new { s = "no", m = "已存在相同标题博客文章~", u = GetSiteUrl() };
                    call = callback + "('" + obj.ToJson() + "')";
                    Response.Write(call);
                    return;
                }

                var blogmode = new Blogs.ModelDB.Blogs()
                    {
                        UsersId = userinfo.Id,
                        BlogTitle = titlehtml,
                        BlogTypes = myBlogTypes,
                        BlogTags = myBlogTags,
                        BlogContent = bodyhtml,
                        CreateTime = DateTime.Now,
                        BlogCreateTime = DateTime.Now,
                        BlogUpTime = DateTime.Now,
                        IsShowMyHome = true
                    };

                blogbll.Add(blogmode);

                if (blogbll.save() > 0)
                {
                    obj = new { s = "ok", m = "发布成功", u = GetSiteUrl() + "/" + userinfo.UserName + "/" + blogmode.Id + ".html" };
                    call = callback + "('" + obj.ToJson() + "')";
                    Response.Write(call);
                    return;
                }
                obj = new { s = "no", m = "发布失败", u = GetSiteUrl() + "/" + userinfo.UserName + "/" + blogmode.Id + ".html" };
                call = callback + "('" + obj.ToJson() + "')";
                Response.Write(call);
                return;
            }
            else
            {
                var obj = new { s = "no", m = "发布失败", u = GetSiteUrl() + "/" };
                var call = callback + "('" + obj.ToJson() + "')";
                Response.Write(call);
                return;
            }
            //var cc = callback + "('ok')";
            //Response.ContentType = "application/json";
            //Response.Write(cc);
        }

        #region GetTagId(string tagname, string userName)
        private int GetTagId(string tagname, int userid)
        {
            BlogTagsBLL blogtag = new BlogTagsBLL();
            try
            {
                var blogtagmode = blogtag.GetList(t => t.TagName == tagname);
                if (blogtagmode.Count() >= 1)
                    return blogtagmode.FirstOrDefault().Id;
                else
                {
                    blogtag.Add(new ModelDB.BlogTags()
                    {
                        TagName = tagname,
                        IsDel = false,
                        UsersId = userid
                    });
                    blogtag.save();
                    return GetTagId(tagname, userid);
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

    }
}
