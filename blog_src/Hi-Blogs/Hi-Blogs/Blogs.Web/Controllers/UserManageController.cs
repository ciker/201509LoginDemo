using Blogs.BLL.Common;
using Blogs.Common.CustomModel;
using Blogs.Helper;
using Blogs.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogs.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserManageController : Controller
    {
        private static string tempUserinfo = "tempUserinfo";
        private static string jihuoma = "jihuoma";

        #region 邮件配置
        /// <summary>
        /// 发件人密码
        /// </summary>
        private static string s_mailPwd = Blogs.Helper.ConfigHelper.GetAppSettings("mailPwd");  //"";
        /// <summary>
        /// SMTP邮件服务器
        /// </summary>
        private static string s_host = Blogs.Helper.ConfigHelper.GetAppSettings("mailHost");
        /// <summary>
        /// 发件人邮箱
        /// </summary>
        private static string s_mailFrom = Blogs.Helper.ConfigHelper.GetAppSettings("mailFrom");
        #endregion

        #region 登录
        [HttpGet]
        public ActionResult Login(int? id) { return View(); }

        [HttpPost]
        public JsonResult Login(BlogUsersSet user, string ischeck)
        {
            JSData objJson = new JSData();
            //var listUser = CacheData.GetAllUserInfo().Where(t => (t.UserName == user.UserName || t.UserMail == user.UserName) && t.UserPass == user.UserPass.MD5().MD5());
            var pass = user.UserPass.MD5().MD5();
            var listUser = GetDataHelper.GetAllUser(t => t.UserInfo).Where(t => (t.UserName == user.UserName || t.UserMail == user.UserName) && t.UserPass == pass);

            if (listUser.Count() > 0)
            {
                Session[tempUserinfo] = listUser.FirstOrDefault();

                #region 1.验证邮箱是否有效  无效则跳转到邮箱验证页面
                if (listUser.Where(t => t.UserMail == "无效" || string.IsNullOrEmpty(t.UserMail)).Count() > 0)
                {
                    objJson.State = EnumState.失败;
                    objJson.Messg = "检测到你注册的邮箱无效~请输入正确的邮箱~";
                    objJson.JSurl = "/UserManage/EmailValidation";
                }
                #endregion

                #region 2.用户是否是激活状态 否:发送激活码 并跳转到激活页面
                else if (listUser.Where(t => t.IsLock == true).Count() > 0)
                {
                    //邮件发送激活码
                    //JSData jsdata;
                    GetActivate(out objJson);
                    //return Json(jsdata); 
                }
                #endregion

                #region 3.登录成功
                else
                {
                    //  var objuser = GetDataHelper.GetAllUser(t => t.UserInfo).Where(t => t.UserName == lodname.Trim() && t.UserPass == lodpass.Trim().MD5().MD5() && t.IsLock == false).FirstOrDefault();

                    BLLSession.UserInfoSessioin = listUser.FirstOrDefault();  //Messg = "登录成功",  //不给提示     直接跳转                   
                    objJson.State = EnumState.正常重定向;
                    if (!string.IsNullOrEmpty(Request.QueryString["href"]))
                        objJson.JSurl = Request.QueryString["href"];
                    else
                        objJson.JSurl = "/";
                    if (ischeck == "on")
                    {
                        //Helper.CookiesHelper.AddCookie("hib_name", user.UserName);
                        //Helper.CookiesHelper.AddCookie("hib_pass", user.UserPass);

                        HttpCookie Cookie = CookiesHelper.GetCookie("userInfo");
                        if (Cookie == null)
                        {
                            Cookie = new HttpCookie("userInfo");
                            Cookie.Values.Add("userName", user.UserName);
                            Cookie.Values.Add("userPass", user.UserPass);
                            //设置Cookie过期时间
                            Cookie.Expires = DateTime.Now.AddDays(365);
                            CookiesHelper.AddCookie(Cookie);
                        }
                        else
                        {
                            if (!Cookie.Values["userName"].Equals(user.UserName))
                                CookiesHelper.SetCookie("userInfo", "userName", user.UserName);
                            if (!Cookie.Values["userPass"].Equals(user.UserPass))
                                CookiesHelper.SetCookie("userInfo", "userPass", user.UserPass);
                        }
                    }
                    else
                    {
                        Helper.CookiesHelper.RemoveCookie("userInfo");
                    }
                }
                #endregion
            }
            else
            {
                objJson.Messg = "用户名或密码错误~";
                objJson.State = EnumState.失败;
            }
            return Json(objJson); //json;
        }
        #endregion

        #region 注册

        [HttpGet]
        public ActionResult Regis(int? id) { return View(); }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Regis(BlogUsersSet blog)
        {
            JsonResult jsonresult = new JsonResult();
            var json = new Common.CustomModel.JSData();

            #region 1.数据检验
            if (CacheData.GetAllUserInfo(true).Where(t => t.UserMail == blog.UserMail).Count() > 0)
                json.Messg = "此邮箱已经被注册~换个邮箱吧~";
            else if (CacheData.GetAllUserInfo().Where(t => t.UserName == blog.UserName).Count() > 0)
                json.Messg = "此用户名已经存在~";
            if (!string.IsNullOrEmpty(json.Messg))
            {
                json.State = EnumState.失败;
                jsonresult.Data = json;
                return jsonresult;
            }
            #endregion

            Blogs.ModelDB.BlogUsersSet user = new ModelDB.BlogUsersSet()
            {
                UserName = blog.UserName,
                UserPass = blog.UserPass,
                UserMail = blog.UserMail,
                UserNickname = blog.UserNickname,
                IsLock = true,
                UserInfo = new UserInfo()
            };
            Session[tempUserinfo] = user;
            JSData jsdata;

            #region 2.邮件发送失败
            if (!GetActivate(out jsdata)) //
            {
                jsdata.State = EnumState.失败;
                jsdata.Messg = jsdata.Messg + " ~请重新输入邮箱~";
            }
            #endregion

            #region 3.邮件发送成功
            else
            {

                BLL.BlogUsersSetBLL userBll = new BLL.BlogUsersSetBLL();
                userBll.Add(user);
                //在保存前 再做次验证
                if (GetDataHelper.GetAllUser().Where(t => t.UserName == blog.UserName || t.UserMail == blog.UserMail).Count() > 0)
                {
                    json.Messg = "此用户名后邮箱已经存在~";
                    json.State = EnumState.失败;
                    jsonresult.Data = json;
                    return jsonresult;
                }
                else
                    userBll.save();

                //验证是否注册成功 （并重新加载缓存信息）
                if (CacheData.GetAllUserInfo(true).Where(t => t.UserName == blog.UserName && t.UserPass == blog.UserPass).Count() > 0)
                {
                    BLLSession.UserInfoSessioin = user;
                }
                else
                {
                    json = new JSData()
                    {
                        Messg = "注册失败",
                        State = EnumState.失败
                    };
                    jsonresult.Data = json;
                    return jsonresult;
                }
            }
            #endregion

            jsonresult.Data = jsdata;
            return jsonresult;
        }

        #endregion

        #region 激活 (实际上是验证激活码后  修改用户信息：包括是否激活IsLock、邮箱地址、密码 修改值是根据 Session[tempUserinfo] 里的值 )

        [HttpGet]
        public ActionResult Activate(int? id)
        {
            if (null == Session[tempUserinfo] || (BLLSession.UserInfoSessioin != null && !BLLSession.UserInfoSessioin.IsLock))
                Response.Redirect(Url.RouteUrl("Default", new { controller = "Home", action = "Index" }));
            return View();
        }

        /// <summary>
        /// 激活 (实际上是验证激活码后  修改用户信息：包括是否激活IsLock、邮箱地址、密码 )
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Activate()
        {
            var json = new Common.CustomModel.JSData();

            #region 1.判断是否从正常途径访问此页面 如果是的话 默认存在 Session[tempUserinfo]  2.如果是已经登录状态则直接无视 跳转

            if (null == Session[tempUserinfo])
            {
                json.State = EnumState.失败;//json.Messg = "请您通过正常途径访问激活页面~";
                json.JSurl = "/";
                return json.ToJson();
            }
            if (BLLSession.UserInfoSessioin != null && !BLLSession.UserInfoSessioin.IsLock)
            {
                json.State = EnumState.失败; //json.Messg = "您都已经登录的还想获取激活码？别玩了~";
                json.JSurl = "/";
                return json.ToJson();
            }
            #endregion

            var tempuser = ((ModelDB.BlogUsersSet)Session[tempUserinfo]);
            var activate = Request.Form["txt_activate"];//激活码

            #region 2.验证激活码  (更新缓存 发送通知邮件 清空无用session)
            if (activate.Trim() == Session[jihuoma].ToString().Trim()) //验证激活码
            {
                BLL.BlogUsersSetBLL user = new BLL.BlogUsersSetBLL();
                var objuser = user.GetList(t => t.Id == tempuser.Id, isAsNoTracking: false).FirstOrDefault();
                if (null != objuser)
                {
                    objuser.IsLock = false;
                    objuser.UserPass = tempuser.UserPass.MD5().MD5();//【】这里有个小BUG 暂未处理（如果是迁移的用户 这第一次登录需要激活密码）
                    objuser.UserMail = tempuser.UserMail;
                }
                user.save();
                #region bug 记录
                //ModelDB.BlogUsers objuser = new ModelDB.BlogUsers();
                //objuser.Id = id;
                //objuser.IsLock = false;
                // user.Up(objuser, "IsLock");  //这个方法 正常情况用没问题，如果先添加   然后修改就有问题  （不能用）    
                #endregion
                bool islock = BLL.Common.CacheData.GetAllUserInfo(true).Where(t => t.Id == tempuser.Id).FirstOrDefault().IsLock;
                if (!islock)
                {
                    #region 发送邮件 告知 激活成功
                    Helper.EmailHelper email = new Helper.EmailHelper()
                       {
                           mailPwd = s_mailPwd,
                           host = s_host,
                           mailFrom = s_mailFrom,
                           mailSubject = "欢迎您注册 嗨-博客",
                           mailBody = objuser.UserNickname + " 您好！欢迎注册 嗨-博客</br></br>" +
                           "您注册的的帐号：" + objuser.UserName +
                               //"   密码是：" + objuser.UserPass + 
                           "</br></br>" +
                           "请您妥善保管~",
                           mailToArray = new string[] { ((ModelDB.BlogUsersSet)Session[tempUserinfo]).UserMail }
                       };

                    try
                    { email.Send(); }
                    catch (Exception)
                    { }
                    #endregion

                    Session[jihuoma] = null;
                    Session[tempUserinfo] = null;

                    BLLSession.UserInfoSessioin = objuser;
                    return new JSData()
                    {
                        Messg = "恭喜您~激活成功~",
                        State = EnumState.正常重定向,
                        JSurl = "/"
                    }.ToJson();
                }
                else
                {
                    return new JSData()
                    {
                        Messg = "激活失败，请联系管理员~",
                        State = EnumState.失败
                    }.ToJson();
                }
            }
            #endregion

            return new JSData()
            {
                Messg = "您输入的激活码错误，你可以重新激活~",
                State = EnumState.失败
            }.ToJson();
        }

        #endregion

        #region  获取激活码
        /// <summary>
        /// 获取激活码 （邮件发送成功 默认跳转到激活页面）
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="mail">邮箱地址</param>
        /// <returns></returns>
        public bool GetActivate(out JSData jsdata)
        {
            var json = new Common.CustomModel.JSData();

            #region 1.验证是否正常途径访问获取激活码 方法
            if (null == Session[tempUserinfo])
            {
                json.State = EnumState.失败; //json.Messg = "请您通过正常途径访问激活页面~";
                json.JSurl = "/";
                jsdata = json;
                return false;
            }
            #endregion

            Session[jihuoma] = new Random().Next(999999999).ToString();

            #region 2.发送邮件 如果邮件发送成功    默认跳转到 激活页面
            Helper.EmailHelper email = new Helper.EmailHelper()
            {
                mailPwd = s_mailPwd,
                host = s_host,
                mailFrom = s_mailFrom,
                mailSubject = "[嗨-博客]激活码",
                mailBody = "欢迎激活 “嗨-博客”</br></br>您注册的的帐号：" + ((ModelDB.BlogUsersSet)Session[tempUserinfo]).UserName +
                " 激活码：" + Session[jihuoma].ToString(),
                mailToArray = new string[] { ((ModelDB.BlogUsersSet)Session[tempUserinfo]).UserMail }
            };

            try
            {
                email.Send();
                json.State = EnumState.正常重定向;
                json.Messg = "激活码已经发送邮箱~请注意查收~";
                json.JSurl = "/UserManage/Activate";
                jsdata = json;
                return true;
            }
            catch (Exception ex)
            {
                json.State = EnumState.失败;
                json.Messg = ex.Message;
                jsdata = json;
                return false;
            }

            #endregion
        }
        /// <summary>
        /// 主要是通过post  且不需要返回值的时候
        /// </summary>
        /// <returns></returns>        
        [HttpPost]
        public string GetActivate() { JSData jsdata; GetActivate(out jsdata); return jsdata.ToJson(); }
        #endregion

        #region 重置密码
        public ActionResult ResetPass(int? id) { return View(); }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ResetPass(BlogUsersSet blog)
        {

            JsonResult jsonRe = new JsonResult();
            var pass = blog.UserPass;
            var email = blog.UserMail;

            var objJson = new Common.CustomModel.JSData();

            #region 1.数据验证
            if (string.IsNullOrEmpty(pass.Trim()))
                objJson.Messg = "新密码不能为空~";
            if (string.IsNullOrEmpty(email.Trim()))
                objJson.Messg = "邮箱不能为空~";
            if (!string.IsNullOrEmpty(objJson.Messg))
            {
                objJson.State = EnumState.失败;
                jsonRe.Data = objJson;
                return jsonRe;
            }
            #endregion

            var obj = GetDataHelper.GetAllUser().Where(t => t.UserMail == email);
            if (null == obj || obj.Count() <= 0)
            {
                objJson.State = EnumState.失败;
                objJson.Messg = "您输入的邮箱不是注册时候的邮箱~";
                jsonRe.Data = objJson;
            }
            else
            {
                Session[tempUserinfo] = obj.FirstOrDefault();
                var userobj = (ModelDB.BlogUsersSet)Session[tempUserinfo];
                userobj.UserPass = pass;//z                
                GetActivate(out  objJson);
                jsonRe.Data = objJson;
            }

            return jsonRe;
        }
        #endregion

        #region （无效邮箱）重新绑定邮箱
        [HttpGet]
        public ActionResult EmailValidation(int? id) { return View(); }


        /// <summary>
        /// （无效邮箱）重新绑定邮箱  邮箱发送成功 默认跳转到激活页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EmailValidation(string UserMail)
        {
            var email = UserMail;

            JsonResult jsonRe = new JsonResult();

            JSData jsdata = new JSData();

            if (null == Session[tempUserinfo])
            {
                jsdata.State = EnumState.失败;
                jsdata.JSurl = "/UserManage/Login";
            }
            else if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email.Trim()))
            {
                jsdata.Messg = "邮箱不能为空~";
                jsdata.State = EnumState.失败;
            }
            else if (BLL.Common.CacheData.GetAllUserInfo().Where(t => t.UserMail == email.Trim()).Count() >= 1)
            {
                jsdata.Messg = "此邮箱已被占用~";
                jsdata.State = EnumState.失败;
            }
            else// if (null != Session[tempUserinfo])
            {
                var userobj = (ModelDB.BlogUsersSet)Session[tempUserinfo];
                userobj.UserMail = email;//z                
                GetActivate(out jsdata);
                // return jsdata.ToJson();
            }
            jsonRe.Data = jsdata;
            return jsonRe;
        }
        #endregion

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Modify() { return View(); }

        #region 用户注销
        /// <summary>
        /// 用户注销
        /// </summary>
        public void Cancellation()
        {
            Helper.CookiesHelper.RemoveCookie("userInfo");
            BLL.Common.BLLSession.UserInfoSessioin = null;//注销
        }
        #endregion
    }
}

