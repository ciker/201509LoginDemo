using System;
using System.Linq;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using ServiceStack.Common;

namespace LoginDemo.Commom
{
    public class RedisRepository<TEntity> :
         IDisposable
         where TEntity : RedisEntity
    {
        readonly IRedisClient redisDB;
        IRedisTypedClient<TEntity> _redisTypedClient;
        IRedisList<TEntity> _table;
        public RedisRepository()
        {
            redisDB = RedisManager.GetClient();
            //redisTypedClient = redisDB.g<TEntity>();
            _table = _redisTypedClient.Lists[typeof(TEntity).Name];
        }

        #region IRepository<TEntity>成员

        public void Insert(TEntity item)
        {
            if (item == null) return;
            _redisTypedClient.AddItemToList(_table, item);
            redisDB.Save();
        }

        public void Delete(TEntity item)
        {
            if (item == null) return;
            var entity = Find(item.RootID);
            _redisTypedClient.RemoveItemFromList(_table, entity);
            redisDB.Save();
        }

        public void Update(TEntity item)
        {
            if (item == null) return;
            var old = Find(item.RootID);
            if (old == null) return;
            _redisTypedClient.RemoveItemFromList(_table, old);
            _redisTypedClient.AddItemToList(_table, item);
            redisDB.Save();
        }

        public IQueryable<TEntity> GetModel()
        {
            return _table.GetAll().AsQueryable();
        }

        public TEntity Find(params object[] id)
        {
            return _table.FirstOrDefault(i => i.RootID == (string)id[0]);
        }
        #endregion

        #region IDisposable成员
        public void Dispose()
        {
            this.ExplicitDispose();
        }
        #endregion

        #region Protected Methods

        /// <summary>
        /// Provides the facility that disposes the object in an explicit manner,
        /// preventing the Finalizer from being called after the object has been
        /// disposed explicitly.
        /// </summary>
        protected void ExplicitDispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing) return;
            _table = null;
            _redisTypedClient = null;
            redisDB.Dispose();
        }
        #endregion

        #region Finalization Constructs
        /// <summary>
        /// Finalizes the object.
        /// </summary>
        ~RedisRepository()
        {
            this.Dispose(false);
        }
        #endregion
    }

    public class RedisEntity
    {
        public string RootID { get; set; }
    }
}
