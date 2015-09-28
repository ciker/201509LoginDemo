using Blogs.BLL.Common;
using Blogs.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blogs.Web.WebAPI
{
    /// <summary>
    /// 主页内容显示
    /// </summary>
    public class HomeController : ApiController
    {
        #region (弃用)
        /// <summary>
        /// (弃用)
        /// </summary>
        /// <param name="index">页码</param>
        /// <param name="sizePage">页容量</param>
        /// <param name="ContentLength">内容截取长度</param>
        /// <returns></returns>
        public object Get(int idex, int sizePage, int ContentLength)
        {
            int total;
            BLL.BlogsBLL blog = new BLL.BlogsBLL();
            var bloglist = blog.GetList(idex, sizePage, out total, t => t.IsShowHome == true, false, t => t.BlogCreateTime, false, tableName: t => t.BlogUsersSet)//           
                .ToList()
                .Select(t => new ModelDB.Blogs()
                {
                    Id = t.Id,
                    BlogTitle = t.BlogTitle,
                    BlogContent = MyHtmlHelper.GetHtmlText(t.BlogContent, ContentLength),
                    BlogCreateTime = t.BlogCreateTime,
                    BlogUsersSet = new ModelDB.BlogUsersSet()
                    {
                        UserName = t.BlogUsersSet.UserName,
                        UserNickname = t.BlogUsersSet.UserNickname
                    },
                    BlogReadNum = t.BlogReadNum,
                    BlogCommentNum = t.BlogCommentNum
                }).ToList();

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("blog", bloglist);
            dic.Add("total", total);
            //dic.Add("users", CacheData.GetAllUserInfo().Where(t => t.IsLock == false).ToList());
            //dic.Add("SessionUser", BLL.Common.BLLSession.UserInfoSessioin);
            return dic;
        }
        #endregion

        /// <summary>
        /// 分页获取 博客 内容
        /// </summary>
        /// <param name="idex">页码</param>
        /// <param name="sizePage">页容量</param>
        /// <param name="ContentLength">内容截取长度</param>
        /// <returns></returns>
        public object GetBlogContent(int index, int sizePage, int contentLength)
        {
            int total;
            BLL.BlogsBLL blog = new BLL.BlogsBLL();
            var bloglist = blog.GetList(index, sizePage, out total, t => t.IsShowHome == true, false, t => t.BlogCreateTime, false, tableName: t => t.BlogUsersSet)//           
                .ToList()
                .Select(t => new ModelDB.Blogs()
                {
                    Id = t.Id,//博客id
                    BlogTitle = t.BlogTitle,//博客标题
                    BlogContent = MyHtmlHelper.GetHtmlText(t.BlogContent, contentLength),//博客简介
                    BlogCreateTime = t.BlogCreateTime,//博客创建时间
                    BlogUsersSet = new ModelDB.BlogUsersSet()
                    {
                        UserName = t.BlogUsersSet.UserName,//用户名
                        UserNickname = t.BlogUsersSet.UserNickname//昵称
                    },
                    BlogReadNum = t.BlogReadNum,//博客阅读量
                    BlogCommentNum = t.BlogCommentNum//博客评论量
                }).ToList();

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("BlogBrief", bloglist);//博客简介
            dic.Add("Total", total);//总页数
            //dic.Add("users", CacheData.GetAllUserInfo().Where(t => t.IsLock == false).Select(t => new
            //    {
            //        UserName = t.UserName,
            //        UserImage = t.UserImage,
            //        UserNickname = t.UserNickname
            //    }
            //    ).ToList());
            return dic;
        }

        public object GetBlogInfo(int blogId)
        {
            BLL.BlogsBLL blog = new BLL.BlogsBLL();
            var blogobj= blog.GetList(t=>t.Id==blogId).FirstOrDefault();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Blog", blogobj.ToDTO()); 
            return dic;
        }
    }
}