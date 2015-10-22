using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure
{
    public interface IRepository<T> : IDisposable where T : class
    {

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll<TProperty>(Expression<Func<T, TProperty>> includePath);

        IEnumerable<T> GetPage<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> orderBy);

        IEnumerable<T> Where(Expression<Func<T, bool>> where);

        T Single(Expression<Func<T, bool>> where);

        T First(Expression<Func<T, bool>> where);

        T FirstOrDefault(Expression<Func<T, bool>> where);

        T GetById(params object[] id);

        void Delete(object id);

        void Delete(T entity);

        void ExecuteSql(string sqlQuery, params object[] parameters);

        void Insert(T entity);

        void Update(T entity);

        void Save();

        IQueryable<T> GetQuery();

        IEnumerable<T> GetWithSql(string sql, params object[] parameters);
    }
}
