using Blogs.Helper;
using Ivony.Html.Parser;
using Ivony.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogs.BLL.Common;

namespace Blogs.Web.Controllers
{
    /// <summary>
    /// 搜索
    /// </summary>
    public class SearchController : Controller
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



        public ActionResult Index()
        {
            return View();
        }

        #region 加载 Lucene.net 的搜索结果
        /// <summary>
        /// 加载 Lucene.net 的搜索结果
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowLuceneResult()
        {
            if (!Request.QueryString.AllKeys.Contains("key"))
                return null;
            string key = Request.QueryString["key"];

            var zhankey = key.Split(' ');//分割关键字
            var blogName = string.Empty;
            if (zhankey.Length >= 2)
            {
                var str = zhankey[0].Trim();
                if (str.Length > 6 && str.Substring(0, 5) == "blog:")
                    blogName = str.Substring(5);//取得用户名
            }

            string userid = Request.QueryString.AllKeys.Contains("userid") ? Request.QueryString["userid"] : "";

            //这里判断是否 用户名不为空  然后取得用户对应的 用户ID  （因为 我在做Lucene 是用用户id 来标记的）
            if (!string.IsNullOrEmpty(blogName))
            {
                key = key.Substring(key.IndexOf(' '));
                var userinfo = CacheData.GetAllUserInfo().Where(t => t.UserName == blogName).FirstOrDefault();
                if (null != userinfo)
                    userid = userinfo.Id.ToString();
            }

            string pIndex = Request.QueryString.AllKeys.Contains("p") ? Request.QueryString["p"] : "";
            int PageIndex = 1;
            int.TryParse(pIndex, out PageIndex);

            int PageSize = 10;
            var searchlist = PanGuLuceneHelper.instance.Search(userid, key, PageIndex, PageSize);
            return PartialView(searchlist);
        }
        #endregion

        #region  加载 bing  的搜索结果
        /// <summary>
        /// 加载 bing  的搜索结果
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowBingResult()
        {
            if (!Request.QueryString.AllKeys.Contains("key"))
                return null;
            string key = Request.QueryString["key"];//搜索关键字
            JumonyParser jumony = new JumonyParser();
            //http://cn.bing.com/search?q=AJAX+site%3ablog.haojima.net&first=11&FORM=PERE
            string pIndex = Request.QueryString.AllKeys.Contains("p") ? Request.QueryString["p"] : "";
            int PageIndex = 1;
            int.TryParse(pIndex, out PageIndex);
            PageIndex--;

            //如：blog:JeffreyZhao 博客
            var zhankey = key.Split(' ');//先用空格分割
            var blogName = string.Empty;
            if (zhankey.Length >= 2)
            {
                var str = zhankey[0].Trim();
                if (str.Length > 6 && str.Substring(0, 5) == "blog:")
                    blogName = "/" + str.Substring(5);//这里取得 用户名
            }
            if (!string.IsNullOrEmpty(blogName))
                key = key.Substring(key.IndexOf(' '));

            //如：
            var url = "http://cn.bing.com/search?q=" + key + "+site:" + GetSiteUrl() + blogName + "&first=" + PageIndex + "1&FORM=PERE";
            var document = jumony.LoadDocument(url);
            var list = document.Find("#b_results .b_algo").ToList().Select(t => t.ToString()).ToList();

            var listli = document.Find("li.b_pag nav ul li");
            if (PageIndex > 0 && listli.Count() == 0)
                return null;

            if (listli.Count() > 1)
            {
                var text = document.Find("li.b_pag nav ul li").Last().InnerText();
                int npage = -1;
                if (text == "下一页")
                {
                    if (listli.Count() > 1)
                    {
                        var num = listli.ToList()[listli.Count() - 2].InnerText();
                        int.TryParse(num, out npage);
                    }
                }
                else
                    int.TryParse(text, out npage);
                if (npage <= PageIndex)
                    list = null;
            }

            return PartialView(list);
        }
        #endregion
    }
}
