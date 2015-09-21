namespace LoginDemo.Entity.UserAccount
{
    public class UserAccountTypeMapping : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// UserInfo ID
        /// </summary>
        public long UserInfoID { get; set; }

        /// <summary>
        /// user account type
        ///  mean   1：mobile 2：emial 0：username 
        /// </summary>
        public int UserAccountType { get; set; }
    }
}
