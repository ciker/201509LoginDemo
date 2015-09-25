using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Autofac.Repository
{
    /// <summary>
    ///     数据单元操作接口
    /// </summary>
    public interface IRepository<TEntity> : IDisposable
    {
        IQueryable<TEntity> Entities();
        void Insert(TEntity entities);
        void Insert(IEnumerable<TEntity> entities);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        void Delete(TEntity entity);

        void Update(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression);

        void Update(Expression<Func<TEntity, TEntity>> propertyExpression);
    }
}