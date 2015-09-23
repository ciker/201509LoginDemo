using System;
using LoginDemo.Commom;

namespace LoginDemo.Entity.UserAccount.QueryParameter
{
    public class UserInfoQueryParameter : BaseQueryParameter
    {
        // ReSharper disable once InconsistentNaming

        public long? ID { get; set; }
        public string Account { get; set; }

        public string Password { get; set; }

        public int? AccountType { get; set; }

        [IgnoreField]
        public bool IsPage { get; set; }

    }
}
