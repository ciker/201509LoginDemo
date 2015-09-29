using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
#region 命名空间
using ChatModel;
using ChatBLL;
using ChatServices.HelperLibrary;
#endregion

namespace ChatServices
{
    /// <summary>
    /// MessageService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class MessageService : System.Web.Services.WebService
    {
        #region 参数说明
        /*(用户验证参数)
         *  UserId--用户唯一标识
         *  PassWord--用户密码验证
         */
        #endregion

        #region 公共对象
        /// <summary>
        /// 返回处理结果
        /// </summary>
        ResponseResult responseResult = new ResponseResult();
        /// <summary>
        /// 消息处理--业务逻辑类
        /// </summary>
        readonly MessageBLL messageBLL = new MessageBLL();
        #endregion

        #region 发送消息
        /// <summary>
        /// 发送消息
        /// </summary>
        [WebMethod]
        public void Send_Msg()
        {
            UserInfo user = IPublic.VerifyUser();
            if (user == null) return;
            MessageInfo message = new MessageInfo()
            {
                SendUserId = HttpContext.Current.Request["UserId"],
                ReciveUserId = HttpContext.Current.Request["ReciveUserId"],
                Content = HttpContext.Current.Request["Content"],
                SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            if (!messageBLL.AddMessage(message))
            {
                responseResult.ResponseDetails = "消息发送失败！";
                responseResult.ResponseStatus = 0;
            }
            else
            {
                responseResult.ResponseData = message;
                responseResult.ResponseDetails = "消息发送成功！";
                responseResult.ResponseStatus = 1;
            }
            responseResult.ResponseWrite();
        }
        #endregion
    }
}
