using Blogs.BLL;
using Blogs.Common.CustomModel;
using Blogs.Helper;
using Blogs.ModelDB;//System.Web.Script.Serialization
using Ivony.Html;
using Ivony.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Blogs.Web.Controllers;
using System.IO;
using HtmlAgilityPack;
using Blogs.BLL.Common;

namespace Blogs.Controllers
{
    /// <summary>    
    /// 后台操作帮助类
    /// </summary>
    public class AdminHelperController : Controller
    {

        public ActionResult Index(string pass)
        {
            var mypass = "Q1";
            if (pass == mypass ||
                (BLL.Common.BLLSession.UserInfoSessioin != null && BLL.Common.BLLSession.UserInfoSessioin.UserName == "admin"))
            {
                Session["adminhelper"] = mypass;
            }
            if (null == BLL.Common.BLLSession.UserInfoSessioin)
            {
                return Redirect("/UserManage/Login?href=" + Request.Url);

            }
            //  return View("~/Views/UserManage/Login.cshtml");
            else if (null == Session["adminhelper"] || Session["adminhelper"].ToString() != mypass)
                return View("Login");
            return View();
        }

        #region 01博客迁移
        public string ButOk(string user, string iszf, string isshowhome, string isshowmyhome)
        {
            if (null == BLL.Common.BLLSession.UserInfoSessioin || null == Session["adminhelper"])
                return string.Empty;

            try
            {
                return Import(user, iszf, isshowhome, isshowmyhome);

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string blogName = string.Empty;

        #region 011博客迁移操作
        #region 根据用户导入cnblog数据
        /// <summary>
        /// 根据用户导入cnblog数据
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string Import(string userName, string iszf, string isshowhome, string isshowmyhome)
        {
            userName = userName.Trim();
            int blosNumber = 0;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string url = "http://www.cnblogs.com/" + userName + @"/mvc/blog/sidecolumn.aspx";
            HtmlAgilityPack.HtmlWeb htmlweb = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            var docment = htmlweb.Load(url);
            string userid = GetCnblogUserId(userName);
            var liS = docment.DocumentNode.SelectNodes("//*[@id='sidebar_categories']/div[1]/ul/li");
            foreach (var item in liS)
            {
                var tXPath = item.XPath;
                var href = item.SelectSingleNode(tXPath + "/a").Attributes["href"].Value;
                var blogtype = htmlweb.Load(href);
                //var entrylistItem = blogtype.DocumentNode.SelectNodes("//*[@id='mainContent']/div/div[2]/div[@class='entrylistItem']");
                var entrylistItem = blogtype.DocumentNode.SelectNodes("//div[@class='entrylistItem']");
                if (null == entrylistItem)//做兼容
                    entrylistItem = blogtype.DocumentNode.SelectNodes("//div[@class='post post-list-item']"); //    
                if (null == entrylistItem)
                {
                    continue;
                }
                foreach (var typeitem in entrylistItem)
                {
                    var typeitemXPath = typeitem.XPath;
                    var typeitemhrefObj = typeitem.SelectSingleNode(typeitemXPath + "/div/a");
                    if (null == typeitemhrefObj) //做兼容
                        typeitemhrefObj = typeitem.SelectSingleNode(typeitemXPath + "/h2/a");
                    var typeitemhref = typeitemhrefObj.Attributes["href"].Value;
                    if (IsAreBlog(typeitemhref))
                        continue;//说明这篇文章已经备份过了的
                    var bloghtml = htmlweb.Load(typeitemhref);
                    var blogcontextobj = bloghtml.DocumentNode.SelectSingleNode("//*[@id='cnblogs_post_body']");//.InnerHtml;
                    if (blogcontextobj == null) continue;//有可能是加密文章
                    var blogcontext = blogcontextobj.InnerHtml;

                    var blogNameObj = bloghtml.DocumentNode.SelectSingleNode("//*[@id='Header1_HeaderTitle']");
                    if (null == blogNameObj)
                        blogNameObj = bloghtml.DocumentNode.SelectSingleNode("//*[@id='lnkBlogTitle']");
                    try
                    {
                        blogName = blogNameObj.InnerText;
                    }
                    catch (Exception)
                    { }

                    var blogtitle = bloghtml.DocumentNode.SelectSingleNode("//*[@id='cb_post_title_url']").InnerText;
                    var blogurl = bloghtml.DocumentNode.SelectSingleNode("//*[@id='cb_post_title_url']").Attributes["href"].Value;
                    var blogtypetagurl = "http://www.cnblogs.com/mvc/blog/CategoriesTags.aspx?blogApp=" + userName + "&blogId=" + userid + "&postId=" +
                        typeitemhref.Substring(typeitemhref.LastIndexOf('/') + 1, typeitemhref.LastIndexOf('.') - typeitemhref.LastIndexOf('/') - 1);
                    var blogtag = Blogs.Common.Helper.MyHtmlHelper.GetRequest(blogtypetagurl);
                    var jsonobj = jss.Deserialize<Dictionary<string, string>>(blogtag);
                    if (null == jsonobj)
                        continue;//如果没有 则返回  (这里只能去 数字.html  不能取那种自定义的url)
                    var tagSplit = jsonobj["Tags"].Split(',');
                    var blogtagid = new List<int>();
                    for (int i = 0; i < tagSplit.Length; i++)
                    {
                        if (tagSplit[i].Length >= 1 && tagSplit[i].LastIndexOf('<') >= 1)
                        {
                            var blogtagname = tagSplit[i].Substring(tagSplit[i].IndexOf('>') + 1, tagSplit[i].LastIndexOf('<') - tagSplit[i].IndexOf('>') - 1);
                            blogtagid.Add(this.GetTagId(blogtagname, userName));
                        }
                    }
                    var categoriesSplit = jsonobj["Categories"].Split(',');
                    var blogtypeid = new List<int>();
                    for (int i = 0; i < categoriesSplit.Length; i++)
                    {
                        if (categoriesSplit[i].Length >= 1 && categoriesSplit[i].LastIndexOf('<') >= 1)
                        {
                            var blogtypename = categoriesSplit[i].Substring(categoriesSplit[i].IndexOf('>') + 1, categoriesSplit[i].LastIndexOf('<') - categoriesSplit[i].IndexOf('>') - 1);
                            blogtypeid.Add(this.GetTypeId(blogtypename, userName));
                        }
                    }
                    var blogtimeobj = bloghtml.DocumentNode.SelectSingleNode("//*[@id='post-date']");
                    var blogtime = "";
                    if (null != blogtimeobj)
                        blogtime = blogtimeobj.InnerText;

                    DateTime? createtime = null;
                    var Outcreatetime = DateTime.Now;
                    if (DateTime.TryParse(blogtime, out Outcreatetime))
                        createtime = Outcreatetime;
                    BlogsBLL blog = new BlogsBLL();
                    var myBlogTags = new BlogTagsBLL().GetList(t => blogtagid.Contains(t.Id), isAsNoTracking: false).ToList();//.ToList();                                        

                    var myBlogTypes = new BLL.BlogTypesBLL().GetList(t => blogtypeid.Contains(t.Id), isAsNoTracking: false).ToList();//.ToList();
                    try
                    {
                        var modelMyBlogs = new ModelDB.Blogs()
                        {
                            BlogContent = blogcontext,
                            BlogCreateTime = createtime,
                            BlogTitle = blogtitle,
                            BlogUrl = blogurl,
                            IsDel = false,
                            BlogTags = myBlogTags,
                            BlogTypes = myBlogTypes,
                            UsersId = GetUserId(userName),
                            BlogForUrl = blogurl,
                            IsForwarding = iszf == "true",
                            IsShowMyHome = isshowmyhome == "true",
                            IsShowHome = isshowhome == "true"
                        };
                        blog.Add(modelMyBlogs);
                        blog.save();
                        var newtag = string.Empty;
                        try
                        {
                            modelMyBlogs.BlogTags.Where(t => true).ToList().ForEach(t => newtag += t.TagName + " ");
                            var newblogurl = "/" + modelMyBlogs.BlogUsersSet.UserName + "/" + modelMyBlogs.Id + ".html";
                            SearchResult search = new SearchResult()
                            {
                                flag = modelMyBlogs.UsersId,
                                id = modelMyBlogs.Id,
                                title = blogtitle,
                                clickQuantity = 0,
                                blogTag = newtag,
                                content = getText(blogcontext, document),
                                url = newblogurl
                            };

                            SafetyWriteHelper<SearchResult>.logWrite(search, PanGuLuceneHelper.instance.CreateIndex);
                        }
                        catch (Exception)
                        {
                        }

                        var postid = blogurl.Substring(blogurl.LastIndexOf('/') + 1);
                        postid = postid.Substring(0, postid.LastIndexOf('.'));
                        testJumonyParser(modelMyBlogs.Id, postid, userName);

                        blosNumber++;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            if (blosNumber > 0)
            {
                Blogs.BLL.Common.GetDataHelper.GetAllTag();
                Blogs.BLL.Common.CacheData.GetAllType(true);
                Blogs.BLL.Common.CacheData.GetAllUserInfo(true);
                return "成功导入" + blosNumber + "篇Blog";
            }
            return "ok";
        }
        #endregion

        #region GetTagId(string tagname, string userName)
        private int GetTagId(string tagname, string userName)
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
                        UsersId = GetUserId(userName)
                    });
                    blogtag.save();
                    return GetTagId(tagname, userName);
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        #endregion

        #region  GetTypeId(string typename, string userName)
        private int GetTypeId(string typename, string userName)
        {
            BlogTypesBLL blogtype = new BlogTypesBLL();
            var blogtagmode = blogtype.GetList(t => t.TypeName == typename);
            if (blogtagmode.Count() >= 1)
                return blogtagmode.FirstOrDefault().Id;
            else
            {
                blogtype.Add(new ModelDB.BlogTypes()
                {
                    TypeName = typename,
                    CreateTime = DateTime.Now,
                    IsDel = false,
                    UsersId = GetUserId(userName)
                });
                blogtype.save();
                return GetTypeId(typename, userName);
            }
        }
        #endregion

        #region 获取haojima用户id
        /// <summary>
        /// 获取haojima用户id   如果没有则创建用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private int GetUserId(string userName)
        {
            BlogUsersSetBLL user = new BlogUsersSetBLL();
            var blogtagmode = user.GetList(t => t.UserName == userName);
            if (blogtagmode.Count() >= 1)
                return blogtagmode.FirstOrDefault().Id;
            else
            {
                user.Add(new ModelDB.BlogUsersSet()
                {
                    UserName = userName,
                    IsDel = false,
                    UserPass = "admin".MD5().MD5(),
                    UserNickname = string.IsNullOrEmpty(blogName.Trim()) ? userName : blogName,
                    IsLock = false,
                    UserMail = "无效",
                    UserInfo = new UserInfo()
                });
                try
                {
                    user.save(false);
                }
                catch (Exception ex)
                {

                    throw;
                }

                return GetUserId(userName);
            }
        }
        #endregion

        #region 检查 这个 url地址 是否被添加过
        /// <summary>
        /// 检查 这个 url地址 是否被添加过
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool IsAreBlog(string url)
        {
            BLL.BlogsBLL blog = new BLL.BlogsBLL();
            var blogs = blog.GetList(t => t.BlogUrl == url);
            return blogs.Count() >= 1;
        }
        #endregion

        #region  获取cnblog用户id
        /// <summary>
        /// 获取cnblog用户id
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetCnblogUserId(string url)
        {
            HtmlAgilityPack.HtmlWeb htmlweb = new HtmlAgilityPack.HtmlWeb();
            var docment = htmlweb.Load("http://www.cnblogs.com/" + url);
            var list = docment.DocumentNode.SelectNodes("//link[@rel='stylesheet']");
            foreach (var item in list)
            {
                if (null != item.Attributes && item.Attributes.Contains("href"))
                {
                    var href = item.Attributes["href"].Value;
                    href = href.Substring(href.LastIndexOf("/") + 1, href.IndexOf(".") - href.LastIndexOf("/") - 1);
                    int userid = -1;
                    if (int.TryParse(href, out userid))
                        return userid.ToString();
                }
            }
            return "";
        }
        #endregion

        #region 迁移cnblog评论
        /// <summary>
        /// 迁移cnblog评论
        /// </summary>
        /// <param name="BlogsId">嗨博客 博客id</param>
        /// <param name="BlogUsersId">嗨博客  评论博客用户id（因为迁移评论者 没有id 所以都默认为1）</param>
        /// <param name="postId">cnblog 博客id</param>int BlogUsersId = 1,
        /// <param name="blogApp">cnblog 博客用户名</param>
        public string testJumonyParser(int BlogsId = 1, string postId = "4368417", string blogApp = "zhaopei")
        {
            bool isNext = true;
            int i = 0;

            var BlogUsersId = 1;
            BLL.BlogUsersSetBLL userbll = new BlogUsersSetBLL();
            var usertemp = GetDataHelper.GetAllUser().Where(t => t.UserName == " ").FirstOrDefault();
            if (null == usertemp)
            {
                var user = new Blogs.ModelDB.BlogUsersSet()
                {
                    UserName = " ",
                    UserPass = " ",
                    IsDel = false,
                    IsLock = false,
                    UserMail = "无效",
                    CreateTime = DateTime.Now,
                    UserInfo = new ModelDB.UserInfo()
                };
                userbll.Add(user);
                userbll.save(false);
                BlogUsersId = user.Id;
            }
            else
                BlogUsersId = usertemp.Id;

            //List<BlogCommentSet> blogcommen = new List<BlogCommentSet>();
            BlogCommentSetBLL blogcommenbll = new BlogCommentSetBLL();
            while (isNext)
            {
                i++;
                var url = "http://www.cnblogs.com/mvc/blog/GetComments.aspx?postId=" + postId + "&blogApp=" + blogApp + "&pageIndex=" + i;
                var jumony = new JumonyParser();
                var htmlSource = jumony.LoadDocument(url).InnerHtml();

                JavaScriptSerializer _jsSerializer = new JavaScriptSerializer();
                CnBlogComments comm = _jsSerializer.Deserialize<CnBlogComments>(htmlSource);
                var commentsHtml = jumony.Parse(comm.commentsHtml);
                var pager = commentsHtml.Find("div.pager").FirstOrDefault();
                if (null != pager)
                {
                    var Next = pager.Find("*").LastOrDefault().InnerText();
                    if (Next != "Next >")
                        isNext = false;
                }
                else
                    isNext = false;

                var listComment = commentsHtml.Find("div.feedbackItem").ToList();
                foreach (var item in listComment)
                {
                    var commentDataNode = item.Find("div.feedbackListSubtitle span.comment_date").FirstOrDefault();  //
                    var commentData = DateTime.Parse(commentDataNode.InnerText());
                    var commentUserNode = item.Find("div.feedbackListSubtitle a[target='_blank']").FirstOrDefault();
                    var commentUser = commentUserNode.InnerText();
                    var Content = item.Find("div.blog_comment_body").FirstOrDefault().InnerText();

                    blogcommenbll.Add(
                        new BlogCommentSet()
                        {
                            BlogsId = BlogsId,
                            CommentID = -1,
                            IsDel = false,
                            Content = Content,
                            CreateTime = commentData,
                            ReplyUserName = commentUser,
                            BlogUsersId = BlogUsersId,
                            IsInitial = true
                        }
                        );
                }
            }

            try
            {
                blogcommenbll.save(false);
            }
            catch (Exception)
            { }
            return "ok";
        }
        #endregion
        #endregion
        #endregion

        #region 02重新获取缓存
        public string getCahe()
        {
            BLL.Common.CacheData.GetAllUserInfo(true);
            BLL.Common.CacheData.GetAllType(true);
            //BLL.Common.GetDataHelper.GetAllTag();


            BLL.BlogUsersSetBLL user = new BlogUsersSetBLL();
            //if (false)
            //{
            //    var list = user.GetList(t => true).ToList();
            //    for (int i = 0; i < list.Count(); i++)
            //    {
            //        var str = list[i].UserPass;
            //        list[i].UserPass = str.MD5().MD5();
            //        user.Up(list[i], "UserPass");
            //    }
            //    user.save(false);
            //}

            return "ok";
        }
        #endregion

        #region 03重新获取索引
        /// <summary>
        /// 重新获取索引
        /// </summary>
        /// <returns></returns>
        public ActionResult getIndex()
        {
            HtmlAgilityPack.HtmlDocument documnet = new HtmlDocument();
            BlogsBLL bloglist = new BlogsBLL();
            var list = new List<SearchResult>();

            list = bloglist.GetList(t => true).ToList().Select(t => new SearchResult()
            {
                blogTag = "",
                url = "/" + t.BlogUsersSet.UserName + "/" + t.Id + ".html",// t.BlogUrl,
                id = t.Id,//DocumentNode
                content = getText(t.BlogContent, documnet),
                clickQuantity = 3,
                title = t.BlogTitle,
                flag = t.UsersId
            }).ToList();

            PanGuLuceneHelper.instance.DeleteAll();
            SafetyWriteHelper<SearchResult>.logWrite(list, PanGuLuceneHelper.instance.CreateIndex);
            return Content("ok");
        }

        private string getText(string html, HtmlDocument documnet)
        {
            if (null == html)
                 return string.Empty;
            documnet.LoadHtml(html);
            return documnet.DocumentNode.InnerText;
        }

        //private string getUrl()
        //{
        //    // var newblogurl = "/" + BLLSession.UserInfoSessioin.UserName + "/" + blog.Id + ".html";
        //}
        #endregion

        #region 测试搜索结果
        /// <summary>
        /// 测试搜索结果
        /// </summary>
        /// <returns></returns>
        public ActionResult showIndex()
        {
            string userid = Request.QueryString["userid"];
            string key = Request.QueryString["key"];
            int PageIndex = 1;
            int PageSize = 40;
            var searchlist = PanGuLuceneHelper.instance.Search(userid, key, PageIndex, PageSize);
            return Json(searchlist);
        }
        #endregion

        //public ActionResult Login()
        //{
        //    if (null == BLL.Common.BLLSession.UserInfoSessioin)
        //        return View("~/Views/UserManage/Login.cshtml");
        //    else if (null == Session["adminhelper"])
        //        return View("Login");
        //    return View();
        //}
    }
}
