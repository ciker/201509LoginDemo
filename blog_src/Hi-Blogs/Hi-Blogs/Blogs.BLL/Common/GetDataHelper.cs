using Blogs.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.BLL.Common
{
    /// <summary>
    /// 获取非缓存数据
    /// </summary>
    public class GetDataHelper
    {
        /// <summary>
        /// 获取用户所有文章类型
        /// </summary>
        /// <returns></returns>
        public static IQueryable<BlogTypes> GetAllType(string name)
        {
            BLL.BlogTypesBLL type = new BLL.BlogTypesBLL();
            return type.GetList(t => t.BlogUsersSet.UserName == name);
            //.Select(t => new { Id = t.Id, TypeName = t.TypeName })
            //.ToList()
            //.Select(t => new ModelDB.BlogTypes() { Id = t.Id, TypeName = t.TypeName }).ToList();
        }

        /// <summary>
        /// 获取用户所有文章标签
        /// </summary>
        /// <returns></returns>
        public static IQueryable<BlogTags> GetAllTag(string name)
        {
            BLL.BlogTagsBLL tag = new BLL.BlogTagsBLL();
            return tag.GetList(t => t.BlogUsersSet.UserName == name);
            //.Select(t => new { Id = t.Id, TagName = t.TagName })
            //.ToList()
            //.Select(t => new BlogTags() { Id = t.Id, TagName = t.TagName }).ToList();
        }

        public static IQueryable<BlogTags> GetAllTag(int id)
        {
            BLL.BlogTagsBLL tag = new BLL.BlogTagsBLL();
            return tag.GetList(t => t.UsersId == id);
        }

        public static IQueryable<BlogTags> GetAllTag()
        {
            BLL.BlogTagsBLL tag = new BLL.BlogTagsBLL();
            return tag.GetList(t => true);
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public static IQueryable<ModelDB.BlogUsersSet> GetAllUser()
        {
            BLL.BlogUsersSetBLL user = new BLL.BlogUsersSetBLL();
            return user.GetList(t => true);
            //.Select(t => new { Id = t.Id, UserName = t.UserName })
            //.ToList()
            //.Select(t => new ModelDB.BlogUsersSet() { Id = t.Id, UserName = t.UserName }).ToList();
        }

        public static IQueryable<ModelDB.BlogUsersSet> GetAllUser<T>(Expression<Func<BlogUsersSet, T>> TTbName, bool isAsNoTracking = true)
        {
            BLL.BlogUsersSetBLL user = new BLL.BlogUsersSetBLL();
            return user.GetList(t => true, tableName: TTbName, isAsNoTracking: isAsNoTracking);
        }

        /// <summary>
        /// 根据用户名  获取用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static BlogUsersSet GetUser(string name)
        {
            BLL.BlogUsersSetBLL user = new BLL.BlogUsersSetBLL();
            return user.GetList(t => t.UserName == name).FirstOrDefault();
        }

        public static IQueryable<UserInfo> GetUserInfo(int id)
        {
            BLL.UserInfoBLL userinfo = new BLL.UserInfoBLL();
            return userinfo.GetList(t => t.BlogUsersSet.Id == id);
        }
    }
}
