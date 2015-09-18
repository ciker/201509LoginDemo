using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LoginDemo.DAL.Interface.UserAccount;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;

namespace LoginDemo.DAL.UserAccount
{
    public class UserAccountDAL : IUserAccountDAL
    {

        public Pager<UserInfo> Query(Entity.UserAccount.QueryParameter.UserInfoQueryParameter para)
        {
            throw new NotImplementedException();
        }

        public UserInfo Save(UserInfo userInfo)
        {
            UserInfo retUser;
            #region sql
            const string sqlText = @" INSERT INTO [dbo].[User]
                                           ([UserName]
                                           ,[UserPWD]
                                           ,[Email]
                                           ,[Mobile]
                                           ,[CreateDateTime]
                                           ,[UpdateDateTime])
                                            OUTPUT INSERTED.*
                                            VALUES(
                                            @UserName
                                            ,@UserPWD
                                            ,@Email
                                            ,@Mobile
                                            ,GETDATE()
                                            ,GETDATE());
                                            ";
            #endregion

            using (var conn = SqlServerDB.GetSqlConnection())
            {
                var re = conn.Query<UserInfo>(sqlText, userInfo, conn.BeginTransaction());
                retUser = re.FirstOrDefault();
            }
            return retUser;
        }

        public UserInfo Update(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public bool Delete(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
