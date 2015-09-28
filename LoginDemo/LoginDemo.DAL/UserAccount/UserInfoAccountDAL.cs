using System;
using System.Linq;
using System.Text;
using Dapper;
using LoginDemo.Commom;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;
using LoginDemo.Entity.UserAccount.QueryParameter;
using LoginDemo.DAL.Interface;

namespace LoginDemo.DAL
{
    public class UserInfoAccountDAL : IUserInfoAccountDAL
    {
        public Pager<UserInfoAccount> Query(UserInfoQueryParameter para)
        {
            var dataList = new Lazy<Pager<UserInfoAccount>>();
            var sqlText = new StringBuilder();
            //var pageSqlText = new StringBuilder();
            sqlText.Append(@"SELECT 
		                     ID
                            ,UserInfoID
		                    ,Account
		                    ,AccountType		
		                    FROM UserInfoAccount
                                WHERE 1 = 1  
		                    ");
            var conditions = para.GenerateByOperate(GenerateOperate.Condition); //string.Empty.GenerateCondition(para);
            sqlText.Append(conditions);
            sqlText.Append(" ORDER BY Id DESC  OFFSET  ");
            sqlText.Append(((para.Skip) * para.Take).ToString());
            sqlText.Append(" ROWS  FETCH NEXT ");
            sqlText.Append(para.Take.ToString());
            sqlText.Append(" ROWS ONLY;");
            if (para.IsPage)
            {
                sqlText.Append(" SELECT  COUNT(1) AS Total ,CEILING((COUNT(1)+0.0)/")
                                .Append(para.Take.ToString())
                                .Append(") AS Pages FROM UserInfoAccount    WHERE 1 =1  ")
                                .Append(conditions + ";");
                //sqlText.Append(pageSqlText);
            }
            using (var conn = SqlServerDB.GetSqlConnection())
            {
                using (var grid = conn.QueryMultiple(sqlText.ToString(), para))
                {
                    //dataList.Value.Items = data.ToArray();
                    dataList.Value.Items = grid.Read<UserInfoAccount>().ToArray();
                    if (para.IsPage)
                    {
                        var pageInfo = grid.Read().FirstOrDefault();
                        if (pageInfo == null) return dataList.Value;
                        dataList.Value.Total = (int)pageInfo.Total;
                        dataList.Value.Pages = (int)pageInfo.Pages;
                    }
                    else
                    {
                        return dataList.Value;
                    }
                }
            }
            return dataList.Value;
        }

        public UserInfoAccount Save(UserInfoAccount userInfoAccount)
        {
            UserInfoAccount retUser;
            #region sqlText
            const string sqlText = @"INSERT INTO [DBO].[USERINFOACCOUNT] 
                                                                    VALUES(NEXT VALUE FOR UserDBSequence
                                                                            ,@USERINFOID
                                                                            ,@ACCOUNTTYPE
                                                                            ,@ACCOUNT)";

            #endregion
            using (var conn = SqlServerDB.GetSqlConnection())
            {
                var re = conn.Query<UserInfoAccount>(sqlText, userInfoAccount);
                retUser = re.FirstOrDefault();
            }
            return retUser;
        }

        public UserInfoAccount Update(UserInfoAccount userInfo)
        {
            throw new NotImplementedException();
        }

        public int Delete(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
