
using Blogs.Helper;
using Blogs.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;

namespace Blogs.BLL.Common
{
    public class BLLSession
    {
        #region 01 户信息Session
        /// <summary>
        /// 用户信息Session
        /// </summary>
        public static ModelDB.BlogUsersSet UserInfoSessioin
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["userinfo"] != null)
                    return HttpContext.Current.Session["userinfo"] as BlogUsersSet;
                else
                {
                    HttpCookie Cookie = CookiesHelper.GetCookie("userInfo");
                    if (Cookie != null)
                    {
                        var lodname = Cookie.Values["userName"];
                        var lodpass = Cookie.Values["userPass"];
                        if (!string.IsNullOrEmpty(lodname) && !string.IsNullOrEmpty(lodpass))
                        {
                            //var objuser = Common.CacheData.GetAllUserInfo().Where(t => t.UserName == lodname.Trim() && t.UserPass == lodpass.Trim().MD5().MD5() && t.IsLock == false).FirstOrDefault();
                            var pass = lodpass.Trim().MD5().MD5();
                            var objuser = GetDataHelper.GetAllUser(t => t.UserInfo, true).Where(t => t.UserName == lodname && t.UserPass == pass && t.IsLock == false).FirstOrDefault();                         

                            if (null != objuser)
                            {
                                HttpContext.Current.Session["userinfo"] = objuser;
                                return objuser;
                            }
                        }
                    }
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session != null)
                    HttpContext.Current.Session["userinfo"] = value;
            }
        }
        #endregion
    }
}
