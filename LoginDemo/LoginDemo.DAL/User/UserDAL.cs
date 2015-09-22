using System;
using Dapper;
using LoginDemo.DAL.Interface;
using LoginDemo.Entity;
using System.Data;
using System.Linq;
using System.Text;
using LoginDemo.Commom;

namespace LoginDemo.DAL
{
    public class UserDAL : IUserDAL
    {
        /// <summary>
        /// 获取查询结果
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public Pager<User> QueryUsersByParameter(UserQueryParameter para)
        {
            var dataList = new Lazy<Pager<User>>();
            var sqlText = new StringBuilder();
            #region sql
            sqlText.Append(
                @"SELECT  [Id]
                  ,[UserName]
                  ,[UserPWD]
                  ,[Email]
                  ,[CreateDateTime]   
                  ,[Mobile] FROM [USER]  WHERE 1 = 1  ");
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
                                .Append(") AS Pages FROM [USER] WHERE 1 =1  ")
                                .Append(conditions + ";");
            }
            #endregion
            using (IDbConnection conn = SqlServerDB.GetSqlConnection())
            {
                var grid = conn.QueryMultiple(sqlText.ToString(), para);
                dataList.Value.Items = grid.Read<User>().ToArray();
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

        public User Save(User user)
        {
            //const string execName = "Proc_InsertUser";
            var sqlText = new StringBuilder();
            User retUser;
            #region sql
            sqlText.Append(@" INSERT INTO [dbo].[User]
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
                                            ");
            #endregion

            using (var conn = SqlServerDB.GetSqlConnection())
            {
                var re = conn.Query<User>(sqlText.ToString(), user);
                retUser = re.FirstOrDefault();
            }
            return retUser;
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }
    }
}
