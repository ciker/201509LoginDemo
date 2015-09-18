using System;
namespace LoginDemo.Entity.UserAccount.QueryParameter
{
    public class UserInfoQueryParameter : BaseQueryParameter
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public int UserAccountType { get; set; }
        public bool IsPage { get; set; }

    }
}
