namespace AngularJsDemo.Web.DB.Models
{
    public class UserInfo : EntityBase<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}