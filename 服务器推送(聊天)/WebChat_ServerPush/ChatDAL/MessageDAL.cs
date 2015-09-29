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
    /// 聊天信息--数据访问类
    /// </summary>
    public class MessageDAL
    {
        #region 新增聊天信息
        /// <summary>
        /// 新增聊天信息记录
        /// </summary>
        /// <param name="message">聊天信息</param>
        /// <returns></returns>
        public int AddMessage(MessageInfo message)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("insert into MessageInfo values");
            sbSql.Append("(@Id,@SendUserId,@ReciveUserId,@Content,@SendTime,@ReciveTime)");
            SQLiteParameter[] parameters = 
            { 
                new SQLiteParameter("@Id", DbType.Int32){Value=DBNull.Value},
                new SQLiteParameter("@SendUserId", DbType.String){Value=message.SendUserId},
                new SQLiteParameter("@ReciveUserId", DbType.String){Value=message.ReciveUserId},
                new SQLiteParameter("@Content", DbType.Object){Value=message.Content},
                new SQLiteParameter("@SendTime", DbType.DateTime){Value=Convert.ToDateTime(message.SendTime)},
                new SQLiteParameter("@ReciveTime", DbType.DateTime){Value=Convert.ToDateTime(message.ReciveTime)}
            };
            return SqliteHelper.ExecuteNonQuery(sbSql.ToString(), parameters);
        }
        #endregion

        #region 修改聊天信息
        /// <summary>
        /// 修改聊天信息记录(根据id)
        /// </summary>
        /// <param name="message">聊天信息</param>
        /// <returns></returns>
        public int UpdateMessage(MessageInfo message)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("update MessageInfo set ");
            sbSql.Append("ReciveTime=@ReciveTime where Id=@Id");
            SQLiteParameter[] parameters = 
            { 
                new SQLiteParameter("@Id", DbType.Int32){Value=message.Id},
                new SQLiteParameter("@ReciveTime", DbType.DateTime){Value=Convert.ToDateTime(message.ReciveTime)}
            };
            return SqliteHelper.ExecuteNonQuery(sbSql.ToString(), parameters);
        }
        #endregion
    }
}
