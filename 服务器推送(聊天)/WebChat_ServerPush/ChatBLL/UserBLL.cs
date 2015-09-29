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
    /// 用户信息--业务逻辑类
    /// </summary>
    public class UserBLL
    {
        #region 公用对象
        /// <summary>
        /// 用户信息处理
        /// </summary>
        readonly UserDAL userDAL = new UserDAL();
        #endregion

        #region 根据账号及密码验证
        /// <summary>
        /// 根据账号及密码验证
        /// </summary>        
        /// <returns></returns>
        public UserInfo VerifyUser(UserInfo user)
        {
            DataSet ds = userDAL.VerifyUser(user);
            List<UserInfo> users = DataSetToList(ds);
            if (users == null) return null;
            user = DataSetToList(ds)[0];
            return user;
        }
        #endregion

        #region 获取所有用户列表
        /// <summary>
        /// 获取所有用户列表
        /// </summary>        
        /// <returns></returns>
        public List<UserInfo> GetUserAll()
        {
            DataSet ds = userDAL.GetUserAll();
            return DataSetToList(ds);
        }
        #endregion

        #region 根据用户id查询用户信息
        /// <summary>
        /// 根据用户id查询用户信息
        /// </summary>
        /// <param name="account">用户信息</param>
        /// <returns></returns>
        public UserInfo GetUserOne(UserInfo user)
        {
            DataSet ds = userDAL.GetUserOne(user);
            List<UserInfo> users = DataSetToList(ds);
            if (users == null) return null;
            return users[0];
        }
        #endregion

        #region 新增用户记录
        /// <summary>
        /// 新增用户记录
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public bool AddUser(UserInfo user)
        {
            int AffectRows = userDAL.AddUser(user);
            return AffectRows > 0;
        }
        #endregion

        #region 修改用户记录
        /// <summary>
        /// 修改用户记录
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public bool UpdateUser(UserInfo user)
        {
            int AffectRows = userDAL.UpdateUser(user);
            return AffectRows > 0;
        }
        #endregion

        #region 私有公共方法
        /// <summary>
        /// 将查询数据集转换成用户集合
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns></returns>
        private List<UserInfo> DataSetToList(DataSet ds)
        {
            List<UserInfo> users = new List<UserInfo>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                users.Add(new UserInfo()
                {
                    UserId = item["UserId"].ToString(),
                    UserName = item["UserName"].ToString(),
                    PassWord = item["PassWord"].ToString(),
                    Sex = Enum.ToObject(typeof(SexEnum), Convert.ToInt32(item["Sex"])).ToString(),
                    Age = Convert.ToInt32(item["Age"]),
                    Email = item["Email"].ToString(),
                    Status = Enum.ToObject(typeof(StatusEnum), Convert.ToInt32(item["Status"])).ToString(),
                    OnlineTime = item["OnlineTime"].ToString(),
                    OfflineTime = item["OfflineTime"].ToString()
                });
            }
            if (users.Count > 0) return users;
            return null;
        }
        #endregion
    }
}
