using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LoginDemo.Commom;
using LoginDemo.DAL.Interface.UserAccount;
using LoginDemo.Entity;
using LoginDemo.Entity.UserAccount;

namespace LoginDemo.DAL.UserAccount
{
    public class UserAccountDAL : IUserAccountDAL
    {

        public Pager<UserInfo> Query(Entity.UserAccount.QueryParameter.UserInfoQueryParameter para)
        {
            var dataList = new Lazy<Pager<UserInfo>>();
            var sqlText = new StringBuilder();
            sqlText.Append(@"SELECT 
		                     U.ID
		                    ,U.ACCOUNT
                            ,U.PASSWORD
		                    ,U.NICKNAME
		                    ,U.GENDER
		                    ,U.COMPANYNAME
		                    ,U.ADDRESS
		                    ,U.REMARK
		
		                    FROM USERINFO AS U
			                    INNER JOIN USERINFO_ACCOUNTTYPE_MAPPING AS UM
			                    ON U.ID = UM.USERINFO_ID
                                WHERE 1 = 1  
		                    ");
            var conditions = string.Empty.GenerateCondition(para);
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
                                .Append(") AS Pages FROM [USER] WHERE 1 =1  ")
                                .Append(conditions + ";");
            }
            using (var conn = SqlServerDB.GetSqlConnection())
            {
                var grid = conn.QueryMultiple(sqlText.ToString(), para);
                dataList.Value.Items = grid.Read<UserInfo>().ToArray();
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
            return dataList.Value;
        }

        public UserInfo Save(UserInfo userInfo)
        {
            UserInfo retUser = null;
            #region sql
            const string sqlText = @" INSERT INTO [dbo].[UserInfo]
			                            ([ACCOUNT]
			                               ,[PASSWORD]
			                               ,[NICKNAME]
			                               ,[GENDER]
			                               ,[COMPANYNAME]
			                               ,[ADDRESS]
			                               ,[REMARK])
                                            OUTPUT INSERTED.*
                                            VALUES(
                                            @ACCOUNT
                                            ,@PASSWORD
                                            ,@NICKNAME
                                            ,@GENDER
                                            ,@COMPANYNAME
                                            ,@ADDRESS
                                            ,@REMARK
                                            ); ";
            const string mappingSqlText = @"INSERT INTO [DBO].[UserInfo_AccountType_Mapping] VALUES(@USERINFO_ID,@ACCOUNT_TYPE)";
            #endregion

            using (var conn = SqlServerDB.GetSqlConnection())
            {
                conn.Open();
                var trans = conn.BeginTransaction();
                try
                {

                    var re = conn.Query<UserInfo>(sqlText, userInfo, trans);
                    retUser = re.FirstOrDefault();
                    if (retUser != null)
                    {
                        conn.Query(mappingSqlText,
                            new { USERINFO_ID = retUser.ID, ACCOUNT_TYPE = userInfo.AccountType }, trans);

                        trans.Commit();
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
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
