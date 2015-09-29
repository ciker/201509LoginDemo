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
    /// 用户关系--数据访问类
    /// </summary>
    public class UserRelationDAL
    {
        #region 根据用户id查询好友
        /// <summary>
        /// 根据用户id查询好友
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public DataSet GetFriends(UserInfo user)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select * from UserRelation where UserId=@UserId");
            SQLiteParameter[] parameters = 
            { 
                new SQLiteParameter("@UserId", DbType.String){Value=user.UserId}
            };
            return SqliteHelper.ExecuteDataset(sbSql.ToString(), parameters);
        }
        #endregion

        #region 新增用户关系
        /// <summary>
        /// 新增用户关系
        /// </summary>
        /// <param name="user">关系信息</param>
        /// <returns></returns>
        public int AddUserRelation(UserRelation user_relation)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("insert into UserRelation values");
            sbSql.Append("(@Id,@UserId,@FriendId);");
            sbSql.Append("insert into UserRelation values");
            sbSql.Append("(@Id,@FriendId,@UserId);");
            SQLiteParameter[] parameters = 
            { 
                new SQLiteParameter("@Id", DbType.Int32){Value=DBNull.Value},
                new SQLiteParameter("@UserId", DbType.String){Value=user_relation.UserId},
                new SQLiteParameter("@FriendId", DbType.String){Value=user_relation.FriendId}
            };
            return SqliteHelper.ExecuteNonQuery(sbSql.ToString(), parameters);
        }
        #endregion
    }
}
