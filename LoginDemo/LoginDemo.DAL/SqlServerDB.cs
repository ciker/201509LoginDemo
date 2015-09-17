
using System.Data;
using LoginDemo.Commom;
using System.Data.SqlClient;
namespace LoginDemo.DAL
{
    public class SqlServerDB : BaseDB, IDataBase
    {
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(("UserDB").GetAppConfigByKey());
        }
    }
}

