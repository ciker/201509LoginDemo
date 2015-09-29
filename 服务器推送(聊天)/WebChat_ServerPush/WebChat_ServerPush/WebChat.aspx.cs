using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebChat_ServerPush
{
    public partial class WebChat : System.Web.UI.Page
    {
        #region 全局变量
        public string UserId;
        public string UserName;
        public string PassWord;
        public string Sex;
        public int Age;
        public string Email;
        #endregion

        #region 窗体加载
        protected void Page_Load(object sender, EventArgs e)
        {
            UserId = Request["UserId"];
            UserName = Request["UserName"];
            PassWord = Request["PassWord"];
            Sex = Request["Sex"];
            Age = Convert.ToInt32(Request["Age"]);
            Email = Request["Email"];
        }
        #endregion
    }
}