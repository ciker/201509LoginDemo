using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Infrastructure.Core;

namespace Infrastructure
{
    public class Repository<T>
         : DisposableBase, IRepository<T> where T : class
    {
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        private readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public object GetBaseContext()
        {
            return _context;
        }

        public IQueryable<T> GetQuery()
        {
            return _dbSet;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public IEnumerable<T> GetAll<TProperty>(Expression<Func<T, TProperty>> includePath)
        {
            return _dbSet.Include<T, TProperty>(includePath);
        }

        public IEnumerable<T> GetPage<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> orderBy)
        {
            return GetQuery().OrderBy(orderBy).Skip(pageIndex * pageSize).Take(pageSize);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where);
        }

        public T Single(Expression<Func<T, bool>> where)
        {
            return _dbSet.Single(where);
        }

        public T First(Expression<Func<T, bool>> where)
        {
            return _dbSet.First(where);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> where)
        {
            return _dbSet.FirstOrDefault(where);
        }

        public virtual T GetById(params object[] id)
        {
            var e = _dbSet.Find(id);
            return e;
        }

        public void Detach(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(object id)
        {
            T entity = GetById(id);
            Delete(entity);
        }

        public void Delete(T entity)
        {
            AttachIfNeeded(entity);
            _dbSet.Remove(entity);
        }

        public void ExecuteSql(string sqlQuery, params object[] parameters)
        {
            _context.Database.ExecuteSqlCommand(sqlQuery, parameters);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            var entry = _context.Entry<T>(entity);

            var oCtx = (_context as System.Data.Entity.Infrastructure.IObjectContextAdapter).ObjectContext;
            var stateManager = oCtx.ObjectStateManager;

            var key = oCtx.CreateEntityKey(oCtx.CreateObjectSet<T>().EntitySet.Name, entity);

            if (entry.State == EntityState.Detached)
            {
                var set = _context.Set<T>();
                T attachedEntity = set.Find(key.EntityKeyValues.Select(v => v.Value).ToArray());

                if (attachedEntity != null)
                {
                    var attachedEntry = _context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified; // This should attach entity
                }
            }

            //An object with the same key already exists in the ObjectStateManager. The ObjectStateManager cannot track multiple objects with the same key.
            //_dbSet.Attach(entity);
            //_context.Entry(entity).State = EntityState.Modified;
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual IEnumerable<T> GetWithSql(string sql, params object[] parameters)
        {
            return _dbSet.SqlQuery(sql, parameters).ToList();
        }

        protected override void OnDisposing(bool disposing)
        {
            if (_context.IsNotNull())
                _context.Dispose();
        }

        private void AttachIfNeeded(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
        }
    }
}
