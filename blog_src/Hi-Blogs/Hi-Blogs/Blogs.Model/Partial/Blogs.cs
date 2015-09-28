using Blogs.ModelDB.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.ModelDB
{
    public partial class Blogs
    {
        public BlogsDTO ToDTO()
        {
            return new BlogsDTO()
            {
                BlogContent = this.BlogContent,//博客正文                 
                BlogReadNum = this.BlogReadNum,//文章阅读量
                BlogRemarks = this.BlogRemarks,//
                BlogTitle = this.BlogTitle,//文章标题
                BlogCreateTime = this.BlogCreateTime,//博客创建时间
                BlogUpTime = this.BlogUpTime,//博客修改时间 
                BlogUrl = this.BlogUrl,//文章链接
                BlogForUrl = this.BlogForUrl,//转发链接
                CreateTime = this.CreateTime,//创建时间
                Id = this.Id, //文章id                
                IsForwarding = this.IsForwarding,
                IsShowHome = this.IsShowHome,//是否显示在首页
                IsShowMyHome = this.IsShowMyHome,//是否显示在个人主页
                UpTime = this.UpTime,//修改时间
                UsersId = this.UsersId,//用户ID 
                UserName = this.BlogUsersSet.UserName,//用户名
                UserNickname = this.BlogUsersSet.UserNickname//用户昵称
            };
        }
    }
}
