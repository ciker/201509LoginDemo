
namespace LoginDemo.Commom
{
    public class CacheManager
    {
        public virtual void SetCache(string key)
        {
        }

        public virtual object GetCacheByKey<T>(string key)
        {
            return null;
        }

        public virtual void DeleteCacheByKey(string key)
        {

        }

    }
}
