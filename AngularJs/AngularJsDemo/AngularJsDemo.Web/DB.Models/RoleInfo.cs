
namespace AngularJsDemo.Web.DB.Models
{
    public class RoleInfo : EntityBase<int>
    {
        public string Name { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }

    }
}