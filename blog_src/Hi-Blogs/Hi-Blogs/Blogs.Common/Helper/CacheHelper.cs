using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Blogs.Common.Helper
{
    /// <summary>
    /// 数据缓存帮助类
    /// </summary>
    public static class CacheHelper
    {
        /// <summary>
        /// 根据键 取缓存
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            //using System.Runtime.Caching;
            //MemoryCache.Default;
            Cache cache = HttpRuntime.Cache;
            return cache[CacheKey];
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string CacheKey, object objObject)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string CacheKey, object objObject, TimeSpan Timeout)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Insert(CacheKey, objObject, null, DateTime.MaxValue, Timeout, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        public static void RemoveAllCache(string CacheKey)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Remove(CacheKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            Cache cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
}
