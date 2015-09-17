
using System.Data;
using LoginDemo.Commom;
using System.Data.SqlClient;
namespace LoginDemo.DAL
{
    public class SqlServerDB : BaseDB, IDataBase
    {
        private static readonly string ConnectionString = ("UserDB").GetAppConfigByKey();
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}

