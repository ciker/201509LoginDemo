using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
using System.Data;
using ChatDAL;
using ChatModel;
#endregion

namespace ChatBLL
{
    /// <summary>
    /// 用户关系--业务逻辑类
    /// </summary>
    public class UserRelationBLL
    {
        #region 公用对象
        /// <summary>
        /// 用户关系处理
        /// </summary>
        readonly UserRelationDAL userrelationDAL = new UserRelationDAL();
        /// <summary>
        /// 用户信息处理
        /// </summary>
        readonly UserBLL userBLL = new UserBLL();
        #endregion

        #region 根据用户id查询好友
        /// <summary>
        /// 根据用户id查询好友
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public List<UserInfo> GetFriends(UserInfo user)
        {
            DataSet ds = userrelationDAL.GetFriends(user);
            List<UserInfo> users = new List<UserInfo>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                users.Add(userBLL.GetUserOne(new UserInfo()
                {
                    UserId = item["FriendId"].ToString()
                }));
            }
            if (users.Count > 0) return users;
            return null;
        }
        #endregion

        #region 新增用户关系
        /// <summary>
        /// 新增用户关系
        /// </summary>
        /// <param name="user">关系信息</param>
        /// <returns></returns>
        public bool AddUserRelation(UserRelation user_relation)
        {
            int AffectRows = userrelationDAL.AddUserRelation(user_relation);
            return AffectRows > 0;
        }
        #endregion
    }
}
