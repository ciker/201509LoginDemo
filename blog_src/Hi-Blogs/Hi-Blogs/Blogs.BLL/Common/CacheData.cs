using Blogs.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Blogs.BLL.Common
{
    /// <summary>
    /// 缓存数据
    /// </summary>
    public static class CacheData
    {
        //#region 01 取得所有博客标签
        ///// <summary>
        ///// 取得所有博客标签
        ///// </summary>
        ///// <param name="newCache">是否重新获取</param>
        ///// <returns></returns>
        //public static List<BlogTags> GetAllTag(bool newCache = false)
        //{
        //    if (null == HttpRuntime.Cache["BlogTag"] || newCache)
        //    {
        //        BLL.BlogTagsBLL tag = new BlogTagsBLL();
        //        HttpRuntime.Cache["BlogTag"] = tag.GetList(t => true).ToList();
        //    }
        //    return (List<BlogTags>)HttpRuntime.Cache["BlogTag"];
        //}
        //#endregion

        #region 02 获得所有博客的分类
        /// <summary>
        /// 获得所有博客的分类
        /// </summary>
        /// <param name="newCache">是否重新获取</param>
        /// <returns></returns>
        public static List<BlogTypes> GetAllType(bool newCache = false)
        {
            if (null == HttpRuntime.Cache["BlogType"] || newCache)
            {
                BLL.BlogTypesBLL tag = new BlogTypesBLL();
                HttpRuntime.Cache["BlogType"] = tag.GetList(t => true).ToList().OrderBy(t => t.TypeName).ToList();
            }
            return (List<BlogTypes>)HttpRuntime.Cache["BlogType"];
        }
        #endregion

        #region 03 获取所有博客的用户信息
        /// <summary>
        /// 获取所有博客的用户信息
        /// </summary>
        /// <param name="newCache">是否重新获取</param>
        /// <returns></returns>
        public static List<BlogUsersSet> GetAllUserInfo(bool newCache = false)
        {
            if (null == HttpRuntime.Cache["UserInfo"] || newCache)
            {
                BLL.BlogUsersSetBLL user = new BlogUsersSetBLL();
                HttpRuntime.Cache["UserInfo"] = user.GetList(t => true).ToList();
            }
            return (List<BlogUsersSet>)HttpRuntime.Cache["UserInfo"];
        }
        #endregion
    }
}
