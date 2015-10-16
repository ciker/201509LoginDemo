using System;
using System.Collections.Generic;
using System.Data.Entity;
using AngularJsDemo.Web.Extensions;


namespace AngularJsDemo.Web.UnitOfWork
{
    public abstract class UnitOfWorkContextBase : IUnitOfWorkContext
    {


        protected abstract DbContext Context { get; }

        /// <summary>
        ///  获取 当前单元操作是否已被提交
        /// </summary>
        public bool IsCommitted { get; private set; }

        public DbContext DbContext { get { return Context; } }


        public int Commit(bool validateOnSaveEnable = true)
        {
            if (IsCommitted)
            {
                return 0;
            }
            var result = Context.SaveChanges(validateOnSaveEnable);
            IsCommitted = true;
            return result;
        }

        public void Rollback()
        {
            IsCommitted = false;
        }

        public void Dispose()
        {
            if (IsCommitted)
            {
                Commit();
            }
            Context.Dispose();
        }



        public void RegisterNew<T, TKey>(T t) where T : DB.Models.EntityBase<TKey>
        {
            var state = Context.Entry(t).State;
            if (state == EntityState.Detached)
            {
                Context.Entry(t).State = EntityState.Added;
            }
            IsCommitted = false;
        }

        public void RegisterNew<T, TKey>(IEnumerable<T> t) where T : DB.Models.EntityBase<TKey>
        {
            throw new NotImplementedException();
        }

        public void RegisterModified<T, TKey>(T t) where T : DB.Models.EntityBase<TKey>
        {
            throw new NotImplementedException();
        }

        public void RegisterModified<T, TKey>(IEnumerable<T> t) where T : DB.Models.EntityBase<TKey>
        {
            throw new NotImplementedException();
        }

        public void RegisterDeleted<T, TKey>(T t) where T : DB.Models.EntityBase<TKey>
        {
            throw new NotImplementedException();
        }

        public void RegisterDeleted<T, TKey>(IEnumerable<T> t) where T : DB.Models.EntityBase<TKey>
        {
            throw new NotImplementedException();
        }

    }
}