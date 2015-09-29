using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
using System.Net;
using WebChatSDK.PHPLibrary;
using Newtonsoft.Json;
using ChatModel;
#endregion

namespace WebChatSDK
{
    public class WebChat
    {
        #region 常用参数
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo User { get; set; }

        /// <summary>
        /// CallBackUrl
        /// </summary>
        /// <returns></returns>
        public string CallBackUrl { get; set; }

        /// <summary>
        /// ResponseResult
        /// </summary>
        public ResponseResult ResponseResult { get; private set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public WebChat() { }
        #endregion

        #region 注册用户
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <returns></returns>
        public string Register_User()
        {
            //除用户验证参数外其他参数
            PHPArray array = new PHPArray();
            array.Add("UserName", User.UserName);
            array.Add("Sex", User.Sex);
            array.Add("Age", User.Age);
            array.Add("Email", User.Email);
            string url = this.GetUrl(string.Format(GlobalURL.Register_User, GlobalURL.Version), array);
            return this.Http_Get(url);
        }
        #endregion

        #region 用户上线
        /// <summary>
        /// 用户上线
        /// </summary>
        /// <returns></returns>
        public string Verify_User()
        {
            string url = this.GetUrl(string.Format(GlobalURL.Verify_User, GlobalURL.Version));
            return this.Http_Get(url);
        }
        #endregion

        #region 用户下线
        /// <summary>
        /// 用户下线
        /// </summary>
        /// <returns></returns>
        public string Downline_User()
        {
            string url = this.GetUrl(string.Format(GlobalURL.Downline_User, GlobalURL.Version));
            return this.Http_Get(url);
        }
        #endregion

        #region 获取好友
        /// <summary>
        /// 获取好友
        /// </summary>
        /// <returns></returns>
        public string Get_Friends()
        {
            string url = this.GetUrl(string.Format(GlobalURL.Get_Friends, GlobalURL.Version));
            return this.Http_Get(url);
        }
        #endregion

        #region 添加好友
        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="friend">好友信息</param>
        /// <returns></returns>
        public string Add_Friend(UserInfo friend)
        {
            PHPArray array = new PHPArray();
            array.Add("FriendId", friend.UserId);
            string url = this.GetUrl(string.Format(GlobalURL.Get_Friends, GlobalURL.Version));
            return this.Http_Get(url);
        }
        #endregion

        #region 发送消息
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="friend">发送消息</param>
        /// <returns></returns>
        public string Send_Msg(MessageInfo message)
        {
            PHPArray array = new PHPArray();
            array.Add("ReciveUserId", message.ReciveUserId);
            array.Add("Content", message.Content);
            string url = this.GetUrl(string.Format(GlobalURL.Send_Msg, GlobalURL.Version), array);
            return this.Http_Get(url);
        }
        #endregion

        #region 私用方法
        private string GetUrl(string url)
        {
            return this.GetUrl(url, null);
        }

        private string GetUrl(string url, PHPArray array)
        {
            OAuth o = new OAuth(this);
            o.Url = url;
            o.Array = array;
            return o.GetUrl();
        }

        private string Http_Get(string url)
        {
            HttpHelper http = new HttpHelper();
            http.Url = url;
            http.Do();
            if (http.StatusCode != HttpStatusCode.OK)
            {
                ResponseResult = new ResponseResult();
                ResponseResult.ResponseDetails = StatusCodeMsg.GetMsg(http.StatusCode);
                ResponseResult.ResponseDetails = http.ErrMsg;
                return null;
            }
            return http.Html;
        }

        private string Http_POST(string url, MsMultiPartFormData form)
        {
            HttpHelper http = new HttpHelper();
            http.Url = url;
            http.Method = Method.POST;
            http.ContentType = "multipart/form-data; boundary=" + form.Boundary;
            http.PostDataByte = form.GetFormData();
            http.Do();
            return http.Html;
        }
        #endregion
    }
}
