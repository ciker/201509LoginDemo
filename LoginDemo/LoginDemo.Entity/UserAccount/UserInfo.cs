namespace LoginDemo.Entity.UserAccount
{
    public class UserInfo : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// account
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// password 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// nickname
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// company name 
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// remark
        /// </summary>
        public string Remark { get; set; }

    }
}
