using System;
using System.Collections.Generic;
using System.Data;
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
            var pageSqlText = new StringBuilder();
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
			                    LEFT JOIN USERINFO_ACCOUNTTYPE_MAPPING AS UM
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
                                .Append(") AS Pages FROM USERINFO  WITH(INDEX(PK_USER_USERID))   WHERE 1 =1  ")
                                .Append(conditions + ";");
                //sqlText.Append(pageSqlText);
            }
            using (var conn = SqlServerDB.GetSqlConnection())
            {
                //var data = conn.Query<UserInfo>(sqlText.ToString(), string.IsNullOrWhiteSpace(conditions) ? null : para);
                var grid = conn.QueryMultiple(sqlText.ToString(), para);
                //dataList.Value.Items = data.ToArray();
                dataList.Value.Items = grid.Read<UserInfo>().ToArray();
                if (para.IsPage)
                {
                    //var pageInfo = conn.Query(pageSqlText.ToString(), string.IsNullOrWhiteSpace(conditions) ? null : para).FirstOrDefault();
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
            #region sqlText
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
            #region transactionSqlText
            //            const string transactionSqlText = @"DECLARE @USERINFO_TEMP TABLE(
            //	                                                [ID] BIGINT,
            //	                                                [ACCOUNT] NVARCHAR(50) NOT NULL,
            //	                                                [PASSWORD] NVARCHAR(50) NOT NULL,
            //	                                                [NICKNAME] NVARCHAR(30) NULL,
            //	                                                [GENDER] BIT NULL,
            //	                                                [COMPANYNAME] NVARCHAR(50) NULL,
            //	                                                [ADDRESS] NVARCHAR(100) NULL,
            //	                                                [REMARK] NVARCHAR(100) NULL)
            //                                                DECLARE @USERINFO_ID BIGINT;
            //	                                                BEGIN TRAN
            //		                                                BEGIN TRY
            //			                                                INSERT INTO [dbo].[UserInfo]
            //			                                                ([ACCOUNT]
            //			                                                   ,[PASSWORD]
            //			                                                   ,[NICKNAME]
            //			                                                   ,[GENDER]
            //			                                                   ,[COMPANYNAME]
            //			                                                   ,[ADDRESS]
            //			                                                   ,[REMARK])
            //			                                                 OUTPUT INSERTED.* INTO @USERINFO_TEMP 
            //			                                                 VALUES(
            //			                                                 @ACCOUNT
            //                                                            ,@PASSWORD
            //                                                            ,@NICKNAME
            //                                                            ,@GENDER
            //                                                            ,@COMPANYNAME
            //                                                            ,@ADDRESS
            //                                                            ,@REMARK);
            //			                                                 PRINT @USERINFO_ID;
            //			                                                 SELECT  @USERINFO_ID =ID  FROM @USERINFO_TEMP
            //			                                                 PRINT @USERINFO_ID;
            //			                                                 INSERT INTO [DBO].[UserInfo_AccountType_Mapping] VALUES                                                                    (@USERINFO_ID,@ACCOUNT_TYPE)
            //			                                                 COMMIT
            //			                                                 SELECT * FROM @USERINFO_TEMP ;
            //		                                                END TRY
            //		                                                BEGIN CATCH
            //			                                                THROW
            //                                                            ROLLBACK
            //			                                                RETURN 
            //		                                                END CATCH";
            #endregion
            #region proc_transaction
            //const string insertProc = "PROC_INSERTUSERINFO";
            #endregion
            #endregion
            #region USE DONET TRANSACTION
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
            #endregion

            #region USE SQL TRANSACTION
            //using (var conn = SqlServerDB.GetSqlConnection())
            //{
            //    var re = conn.Query<UserInfo>(insertProc, new
            //    {
            //        Account_Type = userInfo.AccountType,
            //        ACCOUNT = userInfo.Account,
            //        PASSWORD = userInfo.Password,
            //        NICKNAME = userInfo.NickName,
            //        GENDER = userInfo.Gender,
            //        COMPANYNAME = userInfo.CompanyName,
            //        ADDRESS = userInfo.Address,
            //        REMARK = userInfo.Remark
            //    }, null, false, null, CommandType.StoredProcedure);
            //    retUser = re.FirstOrDefault();
            //}
            #endregion
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
