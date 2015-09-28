using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.ModelDB.DTO
{
    public class BlogsDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<System.DateTime> CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<System.DateTime> UpTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BlogContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BlogRemarks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BlogTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BlogUrl { get; set; }
        /// <summary>
        /// 是否是转发文章
        /// </summary>
        public Nullable<bool> IsForwarding { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<System.DateTime> BlogCreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<System.DateTime> BlogUpTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UsersId { get; set; }
        /// <summary>
        /// 文章阅读量
        /// </summary>
        public Nullable<int> BlogReadNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BlogForUrl { get; set; }
        /// <summary>
        /// 是否显示在首页
        /// </summary>
        public Nullable<bool> IsShowHome { get; set; }
        /// <summary>
        /// 是否显示在个人主页
        /// </summary>
        public Nullable<bool> IsShowMyHome { get; set; }

        public int BlogCommentNum { get; set; }

        public string UserName { get; set; }

        public string UserNickname { get; set; }

        //public BlogUsersSet BlogUsersSet { get; set; }
    }
}
