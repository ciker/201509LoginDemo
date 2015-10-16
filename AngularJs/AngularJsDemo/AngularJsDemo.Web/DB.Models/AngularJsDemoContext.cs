using System.Data.Entity;

namespace AngularJsDemo.Web.DB.Models
{
    public class AngularJsDemoContext : DbContext
    {
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<RoleInfo> Roles { get; set; }


    }
}