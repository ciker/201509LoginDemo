
/* ****************************************************************************************
 * Copyright ：©2015 JangoCheng
 * Function  ： DTO (Data Model)
 * Structure ：
 * Author    ：(程正国)JangoCheng
 * CreateDate：2015-09-25 15:24:19 
 * History   ：
 * ****************************************************************************************
 * Modified *******************************************************************************             
 * Author    ：
 * Date      ：
 * Remark    ：
 * ****************************************************************************************/

using System;
namespace LoginDemo.Entity
{

    /// <summary>
    /// UserInfo
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        #region 属性
        /// <summary>
        /// ID 
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// PASSWORD 
        /// </summary>
        public string PASSWORD { get; set; }

        /// <summary>
        /// NICKNAME 
        /// </summary>
        public string NICKNAME { get; set; }

        /// <summary>
        /// GENDER 
        /// </summary>
        public int? GENDER { get; set; }

        /// <summary>
        /// COMPANYNAME 
        /// </summary>
        public string COMPANYNAME { get; set; }

        /// <summary>
        /// ADDRESS 
        /// </summary>
        public string ADDRESS { get; set; }

        /// <summary>
        /// REMARK 
        /// </summary>
        public string REMARK { get; set; }

        #endregion
    }
}




