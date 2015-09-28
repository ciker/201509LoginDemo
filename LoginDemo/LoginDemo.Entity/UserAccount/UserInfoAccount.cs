namespace LoginDemo.Entity.UserAccount
{
    public class UserInfoAccount : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public long ID { get; set; }

        /// <summary>
        /// account
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// UserInfo ID
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public long UserInfoID { get; set; }

        /// <summary>
        /// user account type
        ///  mean   1：mobile 2：emial 0：username 
        /// </summary>
        public int AccountType { get; set; }

        public bool? IsDelete { get; set; }


        public int? DataStatus { get; set; }


        public System.DateTime? CreateDateTime { get; set; }


        public System.DateTime? UpdateDateTime { get; set; }

    }
}
