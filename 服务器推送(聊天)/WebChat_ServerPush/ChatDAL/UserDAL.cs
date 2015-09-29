using System;
using System.Collections.Generic;
using System.Text;
#region 命名空间
using System.Data;
using System.Data.SQLite;
using ChatModel;
#endregion

namespace ChatDAL
{
    /// <summary>
    /// 用户信息--数据访问类
    /// </summary>
    public class UserDAL
    {
        #region 根据账号及密码验证
        /// <summary>
        /// 根据账号及密码验证
        /// </summary>        
        /// <returns></returns>
        public DataSet VerifyUser(UserInfo user)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select * from UserInfo where UserId=@UserId and PassWord=@PassWord");
            SQLiteParameter[] parameters = 
            { 
                new SQLiteParameter("@UserId", DbType.String){Value=user.UserId},
                new SQLiteParameter("@PassWord", DbType.String){Value=user.PassWord}
            };
            return SqliteHelper.ExecuteDataset(sbSql.ToString(), parameters);
        }
        #endregion

        #region 获取所有用户列表
        /// <summary>
        /// 获取所有用户列表
        /// </summary>        
        /// <returns></returns>
        public DataSet GetUserAll()
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select * from UserInfo order by OnlineTime desc");
            return SqliteHelper.ExecuteDataset(sbSql.ToString(), null);
        }
        #endregion

        #region 根据用户id查询用户信息
        /// <summary>
        /// 根据用户id查询用户信息
        /// </summary>
        /// <param name="account">用户信息</param>
        /// <returns></returns>
        public DataSet GetUserOne(UserInfo user)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select * from UserInfo where UserId=@UserId");
            SQLiteParameter[] parameters = 
            { 
                new SQLiteParameter("@UserId", DbType.String){Value=user.UserId}
            };
            return SqliteHelper.ExecuteDataset(sbSql.ToString(), parameters);
        }
        #endregion

        #region 新增用户记录
        /// <summary>
        /// 新增用户记录
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public int AddUser(UserInfo user)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("insert into UserInfo values");
            sbSql.Append("(@UserId,@UserName,@PassWord,@Sex,@Age,@Email,@Status,@OnlineTime,@OfflineTime)");
            SQLiteParameter[] parameters = 
            { 
                new SQLiteParameter("@UserId", DbType.String){Value=user.UserId},
                new SQLiteParameter("@UserName", DbType.String){Value=user.UserName},
                new SQLiteParameter("@PassWord", DbType.String){Value=user.PassWord},
                new SQLiteParameter("@Sex", DbType.Int32){Value=(int)Enum.Parse(typeof(SexEnum),user.Sex)},
                new SQLiteParameter("@Age", DbType.Int32){Value=user.Age},
                new SQLiteParameter("@Email", DbType.String){Value=user.Email},
                new SQLiteParameter("@Status", DbType.Int32){Value=(int)Enum.Parse(typeof(StatusEnum),user.Status)},
                new SQLiteParameter("@OnlineTime", DbType.DateTime){Value=Convert.ToDateTime(user.OnlineTime)},
                new SQLiteParameter("@OfflineTime", DbType.DateTime){Value=Convert.ToDateTime(user.OfflineTime)}
            };
            return SqliteHelper.ExecuteNonQuery(sbSql.ToString(), parameters);
        }
        #endregion

        #region 修改用户记录
        /// <summary>
        /// 修改用户记录
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public int UpdateUser(UserInfo user)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("update UserInfo set ");
            sbSql.Append("UserName=@UserName,PassWord=@PassWord,Sex=@Sex,Age=@Age,Email=@Email,Status=@Status,OnlineTime=@OnlineTime,OfflineTime=@OfflineTime ");
            sbSql.Append("where UserId=@UserId");
            SQLiteParameter[] parameters = 
            { 
                new SQLiteParameter("@UserName", DbType.String){Value=user.UserName},
                new SQLiteParameter("@PassWord", DbType.String){Value=user.PassWord},
                new SQLiteParameter("@Sex", DbType.Int32){Value=(int)Enum.Parse(typeof(SexEnum),user.Sex)},
                new SQLiteParameter("@Age", DbType.Int32){Value=user.Age},
                new SQLiteParameter("@Email", DbType.String){Value=user.Email},
                new SQLiteParameter("@Status", DbType.Int32){Value=(int)Enum.Parse(typeof(StatusEnum),user.Status)},
                new SQLiteParameter("@OnlineTime", DbType.DateTime){Value=Convert.ToDateTime(user.OnlineTime)},
                new SQLiteParameter("@OfflineTime", DbType.DateTime){Value=Convert.ToDateTime(user.OfflineTime)},
                new SQLiteParameter("@UserId", DbType.String){Value=user.UserId}
            };
            return SqliteHelper.ExecuteNonQuery(sbSql.ToString(), parameters);
        }
        #endregion
    }
}
