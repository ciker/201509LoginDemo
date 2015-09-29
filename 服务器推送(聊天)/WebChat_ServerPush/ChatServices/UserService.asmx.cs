using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
#region 命名空间
using ChatServices.HelperLibrary;
using ChatBLL;
using ChatModel;
#endregion

namespace ChatServices
{
    /// <summary>
    /// UserService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class UserService : System.Web.Services.WebService
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
        /// 用户处理--业务逻辑类
        /// </summary>
        readonly UserBLL userBLL = new UserBLL();
        /// <summary>
        /// 用户关系处理--业务逻辑类
        /// </summary>
        readonly UserRelationBLL userRelationBLL = new UserRelationBLL();
        #endregion

        #region 注册用户
        /// <summary>
        /// 注册用户信息
        /// </summary>
        [WebMethod]
        public void Register_User()
        {
            UserInfo user = new UserInfo()
            {
                UserId = HttpContext.Current.Request["UserId"],
                UserName = HttpContext.Current.Request["UserName"],
                PassWord = HttpContext.Current.Request["PassWord"],
                Sex = HttpContext.Current.Request["Sex"],
                Age = Convert.ToInt32(HttpContext.Current.Request["Age"]),
                Email = HttpContext.Current.Request["Email"],
                OnlineTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Status = StatusEnum.在线.ToString()
            };
            if (!userBLL.AddUser(user))
            {
                responseResult.ResponseDetails = "注册用户失败！";
                responseResult.ResponseStatus = 0;
            }
            else
            {
                responseResult.ResponseData = user;
                responseResult.ResponseDetails = "注册用户成功！";
                responseResult.ResponseStatus = 1;
            }
            responseResult.ResponseWrite();
        }
        #endregion

        #region 用户上线
        /// <summary>
        /// 获取用户信息
        /// </summary>
        [WebMethod]
        public void Verify_User()
        {
            UserInfo user = IPublic.VerifyUser();
            if (user == null) return;
            user.OnlineTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            user.Status = StatusEnum.在线.ToString();
            if (!userBLL.UpdateUser(user))
            {
                responseResult.ResponseDetails = "修改状态失败！";
                responseResult.ResponseStatus = 0;
            }
            else
            {
                responseResult.ResponseData = user;
                responseResult.ResponseDetails = "用户上线成功！";
                responseResult.ResponseStatus = 1;
            }
            responseResult.ResponseWrite();
        }
        #endregion

        #region 用户离线
        /// <summary>
        /// 用户离线
        /// </summary>
        [WebMethod]
        public void Downline_User()
        {
            UserInfo user = IPublic.VerifyUser();
            if (user == null) return;
            user.OfflineTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            user.Status = StatusEnum.离线.ToString();
            if (!userBLL.UpdateUser(user))
            {
                responseResult.ResponseDetails = "修改状态失败！";
                responseResult.ResponseStatus = 0;
                responseResult.ResponseWrite();
            }
            responseResult.ResponseData = user;
            responseResult.ResponseDetails = "用户下线成功！";
            responseResult.ResponseStatus = 1;
            responseResult.ResponseWrite();
        }
        #endregion

        #region 获取好友
        /// <summary>
        /// 获取好友
        /// </summary>
        [WebMethod]
        public void Get_Friends()
        {
            UserInfo user = IPublic.VerifyUser();
            if (user == null) return;
            List<UserInfo> friends = userRelationBLL.GetFriends(user);
            if (friends == null)
            {
                responseResult.ResponseDetails = "没有好友！";
                responseResult.ResponseStatus = 0;
            }
            else
            {
                List<UserInfo> OnlineFriends = new List<UserInfo>();
                List<UserInfo> OfflineFriends = new List<UserInfo>();
                foreach (UserInfo friend in friends)
                {
                    if (friend.Status == StatusEnum.在线.ToString())
                    {
                        OnlineFriends.Add(friend);
                    }
                    else
                    {
                        OfflineFriends.Add(friend);
                    }
                }
                responseResult.ResponseData = new Friends()
                {
                    OnlineFriends = OnlineFriends,
                    OfflineFriends = OfflineFriends
                };
                responseResult.ResponseDetails = "获取好友成功！";
                responseResult.ResponseStatus = 1;
            }
            responseResult.ResponseWrite();
        }

        class Friends
        {
            public List<UserInfo> OnlineFriends { get; set; }
            public List<UserInfo> OfflineFriends { get; set; }
        }
        #endregion

        #region 添加好友
        /// <summary>
        /// 添加好友
        /// </summary>
        [WebMethod]
        public void Add_Friend()
        {
            UserInfo user = IPublic.VerifyUser();
            if (user == null) return;
            UserRelation user_relation = new UserRelation()
            {
                UserId = HttpContext.Current.Request["UserId"],
                FriendId = HttpContext.Current.Request["FriendId"]
            };
            if (!userRelationBLL.AddUserRelation(user_relation))
            {
                responseResult.ResponseDetails = "添加好友失败！";
                responseResult.ResponseStatus = 0;
            }
            else
            {
                responseResult.ResponseData = user_relation;
                responseResult.ResponseDetails = "添加好友成功！";
                responseResult.ResponseStatus = 1;
            }
            responseResult.ResponseWrite();
        }
        #endregion
    }
}