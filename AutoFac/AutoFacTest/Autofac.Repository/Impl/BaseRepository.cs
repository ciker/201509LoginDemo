using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Autofac.Repository.Impl
{
    /// <summary>
    ///     EntityFramework仓储操作基类
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    public class EFRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class,new()
    {
        public EFRepositoryBase(DbContext context)
        {
            Context = context;
        }
        public DbContext Context { get; set; }

        public void Dispose()
        {
            if (Context==null)return;
            Context.Dispose();
            Context = null;
            GC.SuppressFinalize(this);
        }
        public IQueryable<TEntity> Entities()
        {
            return Context.Set<TEntity>().AsQueryable();
        }


        public void Insert(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }
        public void Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        public void Delete(TEntity entity)
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            DbEntityEntry<TEntity> entry = Context.Entry(entity);
            Context.Configuration.ValidateOnSaveEnabled = false;
            entry.State = EntityState.Deleted;
            Context.Configuration.AutoDetectChangesEnabled = true;
        }
        public void Update(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            throw new NotImplementedException();
        }
       /// <summary>
       /// 按需修改
       /// </summary>
       /// <param name="propertyExpression">需要修改的表达式</param>
        public void Update(Expression<Func<TEntity, TEntity>> propertyExpression)
        {
            try
            {
                var memberInitExpression = propertyExpression.Body as MemberInitExpression;
                var entity = CopyPropertyValue(propertyExpression);
                DbEntityEntry<TEntity> entry = Context.Entry(entity);
                entry.State = EntityState.Unchanged;
                if (memberInitExpression != null)
                    foreach (var memberInfo in memberInitExpression.Bindings)
                    {
                        string propertyName = memberInfo.Member.Name;
                        entry.Property(propertyName).IsModified = true;
                    }
                Context.Configuration.ValidateOnSaveEnabled = false;
                
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }
        #region 帮助方法
        /// <summary>
        /// 实体赋值
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public static TEntity CopyPropertyValue<TEntity>(Expression<Func<TEntity, TEntity>> propertyExpression) where TEntity : new()
        {
            var entity = new TEntity();
            var entityType = typeof(TEntity);
            var properties = entityType.GetProperties();
            if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");
            var memberInitExpression = propertyExpression.Body as MemberInitExpression;
            if (memberInitExpression == null)
                throw new ArgumentException("The update expression must be of type MemberInitExpression.",
                    "propertyExpression");
            foreach (MemberBinding binding in memberInitExpression.Bindings)
            {
                //属性字段
                string propertyName = binding.Member.Name;
                //属性值
                object propertyValue;
                var memberAssignment = binding as MemberAssignment;
                if (memberAssignment == null)
                    throw new ArgumentException(
                        "The update expression MemberBinding must only by type MemberAssignment.", "propertyExpression");

                Expression memberExpression = memberAssignment.Expression;
                if (memberExpression.NodeType == ExpressionType.Constant)
                {
                    var constantExpression = memberExpression as ConstantExpression;
                    if (constantExpression == null)
                        throw new ArgumentException(
                            "The MemberAssignment expression is not a ConstantExpression.", "propertyExpression");

                    propertyValue = constantExpression.Value;
                }
                else
                {
                    LambdaExpression lambda = Expression.Lambda(memberExpression, null);
                    propertyValue = lambda.Compile().DynamicInvoke();
                }
                foreach (var propertie in properties.Where(propertie => propertie.Name == propertyName))
                {
                    propertie.SetValue(entity, propertyValue, null);
                    break;
                }
            }
            return entity;
        }

        #endregion
    }
}