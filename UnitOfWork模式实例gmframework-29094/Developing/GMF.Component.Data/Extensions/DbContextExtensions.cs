﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using GMF.Component.Tools;


namespace GMF.Component.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static void Update<TEntity, TKey>(this DbContext dbContext, params TEntity[] entities) where TEntity : EntityBase<TKey>
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            if (entities == null) throw new ArgumentNullException("entities");

            foreach (TEntity entity in entities)
            {
                DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
                try
                {
                    DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity oldEntity = dbSet.Find(entity.Id);
                    dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
        }

        public static void Update<TEntity, TKey>(this DbContext dbContext, Expression<Func<TEntity, object>> propertyExpression, params TEntity[] entities)
            where TEntity : EntityBase<TKey>
        {
            if (propertyExpression == null) throw new ArgumentNullException("propertyExpression");
            if (entities == null) throw new ArgumentNullException("entities");
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)propertyExpression.Body).Members;
            foreach (TEntity entity in entities)
            {
                DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
                try
                {
                    DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
                    entry.State = EntityState.Unchanged;
                    foreach (var memberInfo in memberInfos)
                    {
                        entry.Property(memberInfo.Name).IsModified = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity originalEntity = dbSet.Local.Single(m => Equals(m.Id, entity.Id));
                    ObjectContext objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
                    ObjectStateEntry objectEntry = objectContext.ObjectStateManager.GetObjectStateEntry(originalEntity);
                    objectEntry.ApplyCurrentValues(entity);
                    objectEntry.ChangeState(EntityState.Unchanged);
                    foreach (var memberInfo in memberInfos)
                    {
                        objectEntry.SetModifiedProperty(memberInfo.Name);
                    }
                }
            }
        }

        public static int SaveChanges(this DbContext dbContext, bool validateOnSaveEnabled)
        {
            bool isReturn = dbContext.Configuration.ValidateOnSaveEnabled != validateOnSaveEnabled;
            try
            {
                dbContext.Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                return dbContext.SaveChanges();
            }
            finally
            {
                if (isReturn)
                {
                    dbContext.Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
                }
            }
        }
    }
}
