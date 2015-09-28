using Blogs.BLL;
using Blogs.BLL.Common;
using Blogs.Common.CustomModel;
using Blogs.Helper;
using Blogs.ModelDB.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blogs.Controllers
{
    public class AdminController : Controller
    {

        /// <summary>
        /// 管理员特权
        /// </summary>
        private readonly static string admin = "admin";

        #region 01发布 && 根据blogID编辑具体文章
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Release(int? id)
        {
            var userinfo = BLLSession.UserInfoSessioin;
            if (null == userinfo)
            {
                Response.Redirect("/UserManage/Login?href=/Admin/Release");
                return null;
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("blogTag", GetDataHelper.GetAllTag(BLLSession.UserInfoSessioin.Id).ToList());
            dic.Add("blogType", CacheData.GetAllType().Where(t => t.UsersId == BLLSession.UserInfoSessioin.Id).ToList());
            ModelDB.Blogs blog = new ModelDB.Blogs();
            if (null != id)
            {
                BLL.BlogsBLL blogbll = new BlogsBLL();
                blog = blogbll.GetList(t => t.Id == id && (t.UsersId == userinfo.Id || userinfo.UserName == admin)).FirstOrDefault();
            }
            dic.Add("blog", blog);
            return View(dic);
        }
        [HttpPost]
        [ValidateInput(false)]
        public string Release()
        {
            JSData jsdata = new JSData();

            var content = Request.Form["content"];//正文内容
            var title = Request.Form["title"];//标题
            var oldtag = Request.Form["oldtag"];//旧的标签
            var newtag = Request.Form["newtag"];//新的标签
            var types = Request.Form["chk_type"];//文章类型
            var isshowhome = Request.Form["isshowhome"];//是否显示在主页
            var isshowmyhome = Request.Form["isshowmyhome"];//是否显示在个人主页
            var blogid = Request.Form["blogid"];//

            int numblogid = -1;
            int.TryParse(blogid, out numblogid);

            #region 数据验证
            if (null == BLL.Common.BLLSession.UserInfoSessioin)
                jsdata.Messg = "您还未登录~";
            else if (BLL.Common.BLLSession.UserInfoSessioin.IsLock)
                jsdata.Messg = "您的账户已经被锁定,请联系管理员~";
            else if (string.IsNullOrEmpty(content))
                jsdata.Messg = "内容不能为空~";
            else if (content.Length >= 80000)
                jsdata.Messg = "发布内容过多~";
            else if (string.IsNullOrEmpty(title))
                jsdata.Messg = "标题不能为空~";
            else if (title.Length >= 100)
                jsdata.Messg = "标题过长~";

            if (!string.IsNullOrEmpty(jsdata.Messg))
            {
                jsdata.State = EnumState.失败;
                return jsdata.ToJson();
            }
            #endregion

            BLL.BlogsBLL blogbll = new BLL.BlogsBLL();
            var blogtemp = blogbll.GetList(t => t.Id == numblogid, isAsNoTracking: false).FirstOrDefault();
            var userid = numblogid > 0 ? blogtemp.UsersId : BLLSession.UserInfoSessioin.Id;//如果numblogid大于〇证明 是编辑修改
            var sessionuserid = BLLSession.UserInfoSessioin.Id;

            //获取得 文章 类型集合 对象
            var typelist = new List<int>();
            foreach (string type in types.Split(',').ToList())
            {
                if (!string.IsNullOrEmpty(type))
                    typelist.Add(int.Parse(type));
            }
            // types.Split(',').ToList().ForEach(t => typelist.Add(int.Parse(t)));
            var myBlogTypes = new BLL.BlogTypesBLL().GetList(t => typelist.Contains(t.Id), isAsNoTracking: false).ToList();

            //获取得 文章 tag标签集合 对象
            //old
            var oldtaglist = oldtag.Split(',').ToList();
            var myOldTagTypes = new BLL.BlogTagsBLL().GetList(t => t.UsersId == userid && oldtaglist.Contains(t.TagName), isAsNoTracking: false).ToList();
            //new           
            var newtaglist = newtag.Split(',').ToList();
            AddTag(newtaglist, userid);//保存到数据库
            var myNweTagTypes = new BLL.BlogTagsBLL().GetList(t => t.UsersId == userid && newtaglist.Contains(t.TagName), isAsNoTracking: false).ToList();
            myNweTagTypes.ForEach(t => myOldTagTypes.Add(t));



            //ModelDB.Blogs blogtemp = new ModelDB.Blogs();
            if (numblogid > 0)  //如果有 blogid 则修改
            {
                //blog = blogbll.GetList(t => t.Id == numblogid, isAsNoTracking: false).FirstOrDefault();
                if (sessionuserid == blogtemp.UsersId || BLLSession.UserInfoSessioin.UserName == admin) //一定要验证更新的博客是否是登陆的用户
                {
                    blogtemp.BlogContent = content;
                    blogtemp.BlogTitle = title;
                    //blog.BlogUpTime = DateTime.Now;
                    //blog.BlogCreateTime = DateTime.Now;
                    blogtemp.IsShowMyHome = isshowmyhome == "true";
                    blogtemp.IsShowHome = isshowhome == "true";
                    blogtemp.BlogTypes.Clear();//更新之前要清空      不如会存在主外键约束异常
                    blogtemp.BlogTypes = myBlogTypes;
                    blogtemp.BlogTags.Clear();
                    blogtemp.BlogTags = myOldTagTypes;
                    blogtemp.IsDel = false;
                    blogtemp.IsForwarding = false;
                    jsdata.Messg = "修改成功~";
                }
                else
                {
                    jsdata.Messg = "您没有编辑此博文的权限~";
                    jsdata.JSurl = "/";
                    jsdata.State = EnumState.失败;
                    return jsdata.ToJson();
                }
            }
            else  //否则 新增
            {
                var blogfirst = blogbll.GetList(t => t.UsersId == sessionuserid).OrderByDescending(t => t.Id).FirstOrDefault();
                //var blogtitle = blogtemp.BlogTitle;
                //if (blogfirst != null)
                //    blogtitle = blogtemp.BlogTitle;
                if (null != blogfirst && blogfirst.BlogTitle == title)
                {
                    jsdata.Messg = "不能同时发表两篇一样标题的文章~";
                }
                else
                {
                    blogtemp = new ModelDB.Blogs()
                     {
                         UsersId = sessionuserid,
                         BlogContent = content,
                         BlogTitle = title,
                         BlogUpTime = DateTime.Now,
                         BlogCreateTime = DateTime.Now,
                         IsShowMyHome = isshowmyhome == "true",
                         IsShowHome = isshowhome == "true",
                         BlogTypes = myBlogTypes,
                         BlogTags = myOldTagTypes,
                         IsDel = false,
                         IsForwarding = false
                     };
                    blogbll.Add(blogtemp);
                    jsdata.Messg = "发布成功~";
                }
            }

            //
            if (blogbll.save(false) > 0)
            {
                #region 添加 或 修改搜索索引
                try
                {
                    var newtagList = string.Empty;
                    blogtemp.BlogTags.Where(t => true).ToList().ForEach(t => newtagList += t.TagName + " ");
                    var newblogurl = "/" + BLLSession.UserInfoSessioin.UserName + "/" + blogtemp.Id + ".html";
                    SearchResult search = new SearchResult()
                    {
                        flag = blogtemp.UsersId,
                        id = blogtemp.Id,
                        title = blogtemp.BlogTitle,
                        clickQuantity = 0,
                        blogTag = newtagList,
                        content = Blogs.Common.Helper.MyHtmlHelper.GetHtmlText(blogtemp.BlogContent),
                        url = newblogurl
                    };
                    SafetyWriteHelper<SearchResult>.logWrite(search, PanGuLuceneHelper.instance.CreateIndex);
                }
                catch (Exception)
                { }
                #endregion

                jsdata.State = EnumState.成功;
                jsdata.JSurl = "/" + CacheData.GetAllUserInfo().Where(t => t.Id == blogtemp.UsersId).First().UserName + "/" + blogtemp.Id + ".html";
                return jsdata.ToJson();
            }

            jsdata.Messg = string.IsNullOrEmpty(jsdata.Messg) ? "操作失败~" : jsdata.Messg;
            jsdata.State = EnumState.失败;
            return jsdata.ToJson();
        }

        #region 添加新的tag标签
        /// <summary>
        /// 添加新的tag标签
        /// </summary>
        /// <param name="taglist"></param>
        /// <returns></returns>
        private bool AddTag(List<string> taglist, int userid)
        {
            BlogTagsBLL blogtype = new BlogTagsBLL();
            foreach (string tag in taglist)
            {
                if (string.IsNullOrEmpty(tag) || blogtype.GetList(t => t.TagName == tag).Count() > 0)
                    continue;
                blogtype.Add(new ModelDB.BlogTags()
                {
                    TagName = tag,
                    CreateTime = DateTime.Now,
                    IsDel = false,
                    UsersId = userid
                });
            }
            return blogtype.save() > 0;
        }
        #endregion

        #endregion

        #region 02文章编辑列表
        public ActionResult Edit(int? id)
        {
            var userinfo = BLLSession.UserInfoSessioin;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("blogTag", GetDataHelper.GetAllTag(BLLSession.UserInfoSessioin.Id).ToList());
            dic.Add("blogType", CacheData.GetAllType().Where(t => t.UsersId == BLLSession.UserInfoSessioin.Id).ToList());
            List<BlogsDTO> blogs = new List<BlogsDTO>();
            if (null != id)
            {
                int ttt;
                BLL.BlogsBLL blogbll = new BlogsBLL();
                if (id == 0)//代表 未分类
                {
                    blogs = blogbll.GetList(1, 50, out ttt, t => t.BlogTypes.Count() == 0 && (t.UsersId == userinfo.Id), false, t => t.BlogCreateTime, false)
                      .ToList().Select(t => new BlogsDTO()
                      {
                          Id = t.Id,
                          BlogTitle = t.BlogTitle
                      }).ToList();
                }
                else
                {

                    blogs = blogbll.GetList(1, 50, out ttt, t => t.BlogTypes.Where(v => v.Id == id).Count() > 0 && (t.UsersId == userinfo.Id), false, t => t.BlogCreateTime, false)
                       .ToList().Select(t => new BlogsDTO()
                       {
                           Id = t.Id,
                           BlogTitle = t.BlogTitle
                       }).ToList();
                }
            }
            dic.Add("blogs", blogs);
            return View(dic);
        }
        #endregion

        #region 03删除  删除 文章
        /// <summary>
        /// 删除 文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Del(int? id)
        {
            var userinfo = BLLSession.UserInfoSessioin;
            List<BlogsDTO> blogs = new List<BlogsDTO>();
            int isdelok = -1;
            if (null != id)
            {
                BLL.BlogsBLL blogbll = new BlogsBLL();
                blogbll.Del(new ModelDB.Blogs() { Id = (int)id }, true);
                isdelok = blogbll.save(false);
                List<SearchResult> list = new List<SearchResult>();
                list.Add(new SearchResult() { id = (int)id });
                SafetyWriteHelper<SearchResult>.logWrite(list, PanGuLuceneHelper.instance.Delete);
            }
            return Content((isdelok > 0).ToString());
        }
        #endregion

        #region 04新增文章类型
        /// <summary>
        /// 新增文章类型
        /// </summary>
        /// <param name="newtypename"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult NewAddType(string newtypename)
        {
            JSData jsdata = new JSData();

            #region 数据验证
            if (null == BLLSession.UserInfoSessioin)
                jsdata.Messg = "您还未登录~";
            else if (string.IsNullOrEmpty(newtypename))
                jsdata.Messg = "类型不能为空~";

            if (!string.IsNullOrEmpty(jsdata.Messg))
            {
                jsdata.State = EnumState.失败;
                return Json(jsdata);
            }
            #endregion

            int userid = BLLSession.UserInfoSessioin.Id;
            BLL.BlogTypesBLL bll = new BlogTypesBLL();
            bll.Add(
                new ModelDB.BlogTypes()
                {
                    TypeName = newtypename,
                    UsersId = userid,
                    IsDel = false
                }
                );

            if (bll.save() > 0)//保存
            {
                BLL.Common.CacheData.GetAllType(true);//更新缓存
                jsdata.State = EnumState.成功;
                jsdata.Messg = "新增成功~";

            }
            else
            {
                jsdata.State = EnumState.失败;
                jsdata.Messg = "新增失败~";
            }
            return Json(jsdata);
        }
        #endregion

        #region 05编辑文章类型
        /// <summary>
        /// 编辑文章类型
        /// </summary>
        /// <param name="typename"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public ActionResult EditType(string typename, int typeid)
        {
            JSData jsdata = new JSData();

            #region 数据验证
            if (null == BLLSession.UserInfoSessioin)
                jsdata.Messg = "您还未登录~";
            else if (string.IsNullOrEmpty(typename))
                jsdata.Messg = "类型不能为空~";
            else if (null == typeid)
                jsdata.Messg = "未取到文章ID~";
            if (!string.IsNullOrEmpty(jsdata.Messg))
            {
                jsdata.State = EnumState.失败;
                return Json(jsdata);
            }
            #endregion

            BLL.BlogTypesBLL bll = new BlogTypesBLL();
            var blogtype = new ModelDB.BlogTypes()
            {
                Id = typeid,
                TypeName = typename
            };
            bll.Up(blogtype, "TypeName");

            if (bll.save() > 0)//保存
            {
                BLL.Common.CacheData.GetAllType(true);//更新缓存
                jsdata.State = EnumState.成功;
                // jsdata.Messg = "修改成功~";
            }
            else
            {
                jsdata.State = EnumState.失败;
                jsdata.Messg = "操作失败~";
            }
            return Json(jsdata);
        }
        #endregion

        #region 06自定义设置

        #region 设置 PC 端 主题样式
        /// <summary>
        /// 设置 PC 端 主题样式
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ConfigurePC()
        {
            ViewBag.confcss = "";
            ViewBag.confhtml = "";
            ViewBag.confjs = "";
            if (BLLSession.UserInfoSessioin != null)
            {
                string path = FileHelper.defaultpath + "/MyConfigure/" + BLLSession.UserInfoSessioin.UserName + "/";
                FileHelper.CreatePath(path);
                ViewBag.confcss = FileHelper.GetFile(path, "conf.css");
                ViewBag.confsidehtml = FileHelper.GetFile(path, "conf_side.txt");
                ViewBag.conffirsthtml = FileHelper.GetFile(path, "conf_first.txt");
                ViewBag.conftailhtml = FileHelper.GetFile(path, "conf_tail.txt");
                ViewBag.confjs = FileHelper.GetFile(path, "conf.js");
                ViewBag.IsShowCSS = BLLSession.UserInfoSessioin.UserInfo.IsShowCSS;
                ViewBag.IsDisCSS = BLLSession.UserInfoSessioin.UserInfo.IsDisCSS;
                ViewBag.TerminalType = "PC";
            }
            return View("Configure");
        }
        #endregion

        #region 设置 移动 端 主题样式
        /// <summary>
        /// 设置 移动 端 主题样式
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ConfigureMobile()
        {
            ViewBag.confcss = "";
            ViewBag.confhtml = "";
            ViewBag.confjs = "";
            if (BLLSession.UserInfoSessioin != null)
            {
                string path = FileHelper.defaultpath + "/MyConfigure/" + BLLSession.UserInfoSessioin.UserName + "/";
                FileHelper.CreatePath(path);
                ViewBag.confcss = FileHelper.GetFile(path, "Mconf.css");
                ViewBag.confsidehtml = FileHelper.GetFile(path, "Mconf_side.txt");
                ViewBag.conffirsthtml = FileHelper.GetFile(path, "Mconf_first.txt");
                ViewBag.conftailhtml = FileHelper.GetFile(path, "Mconf_tail.txt");
                ViewBag.confjs = FileHelper.GetFile(path, "Mconf.js");
                ViewBag.IsShowCSS = BLLSession.UserInfoSessioin.UserInfo.IsShowMCSS;
                ViewBag.IsDisCSS = BLLSession.UserInfoSessioin.UserInfo.IsDisMCSS;
                ViewBag.TerminalType = "Mobile";
            }
            return View("Configure");
        }
        #endregion

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Configure(string conf_css, string conf_side_html, string conf_first_html, string conf_tail_html, string conf_js)
        {
            var IsShowCSS = Request.Form["IsShowCSS"] == "on";
            var IsDisCSS = Request.Form["IsDisCSS"] == "on";
            if (BLLSession.UserInfoSessioin == null)
                return Json("您还没有登录 不能修改~"); ;
            try
            {
                //==============================================================================================================
                //遗留问题：
                //如下：如果 userinfobll.Up(BLLSession.UserInfoSessioin.UserInfo)两次的话 报异常：[一个实体对象不能由多个 IEntityChangeTracker 实例引用]
                //那么 我只能 new一个新的对象 修改  然后 同时 BLLSession.UserInfoSessioin.UserInfo里面的属性，不然 其他地方访问的话 是没有修改过来的值
                //==============================================================================================================
                var userinftemp = new ModelDB.UserInfo(); //BLLSession.UserInfoSessioin.UserInfo;
                BLL.UserInfoBLL userinfobll = new UserInfoBLL();
                if (Request.Form["TerminalType"] == "PC")//如果是PC端
                {
                    userinftemp.IsShowCSS =
                        BLLSession.UserInfoSessioin.UserInfo.IsShowCSS = IsShowCSS;
                    userinftemp.IsDisCSS =
                        BLLSession.UserInfoSessioin.UserInfo.IsDisCSS = IsDisCSS;
                    userinftemp.Id =
                        BLLSession.UserInfoSessioin.UserInfo.Id;
                    userinfobll.Up(userinftemp, "IsShowCSS", "IsDisCSS");//"IsShowHTML",, "IsShowJS"
                }
                else
                {
                    userinftemp.IsShowMCSS =
                      BLLSession.UserInfoSessioin.UserInfo.IsShowMCSS = IsShowCSS;
                    userinftemp.IsDisMCSS =
                        BLLSession.UserInfoSessioin.UserInfo.IsDisMCSS = IsDisCSS;
                    userinftemp.Id =
                        BLLSession.UserInfoSessioin.UserInfo.Id;
                    userinfobll.Up(userinftemp, "IsShowMCSS", "IsDisMCSS");
                }

                CacheData.GetAllUserInfo().FirstOrDefault(t => t.Id == BLLSession.UserInfoSessioin.Id).UserInfo
                    = BLLSession.UserInfoSessioin.UserInfo;

                userinfobll.save();

                string path = FileHelper.defaultpath + "/MyConfigure/" + BLLSession.UserInfoSessioin.UserName + "/";
                FileHelper.CreatePath(path);
                if (conf_css.Length >= 40000 ||
                    conf_tail_html.Length >= 40000 ||
                    conf_first_html.Length >= 40000 ||
                    conf_side_html.Length >= 40000 ||
                    conf_js.Length >= 40000)
                {
                    return Json("您修改的内容字符过多~");
                }

                if (Request.Form["TerminalType"] == "PC")//如果是PC端
                {
                    FileHelper.SaveFile(path, "conf.css", conf_css);
                    FileHelper.SaveFile(path, "conf_side.txt", conf_side_html);
                    FileHelper.SaveFile(path, "conf_first.txt", conf_first_html);
                    FileHelper.SaveFile(path, "conf_tail.txt", conf_tail_html);
                    FileHelper.SaveFile(path, "conf.js", conf_js);
                }
                else
                {
                    FileHelper.SaveFile(path, "Mconf.css", conf_css);
                    FileHelper.SaveFile(path, "Mconf_side.txt", conf_side_html);
                    FileHelper.SaveFile(path, "Mconf_first.txt", conf_first_html);
                    FileHelper.SaveFile(path, "Mconf_tail.txt", conf_tail_html);
                    FileHelper.SaveFile(path, "Mconf.js", conf_js);
                }


                return Json("修改成功~");
            }
            catch (Exception)
            {
                return Json("修改失败~"); ;
            }
        }
        #endregion
    }
}
