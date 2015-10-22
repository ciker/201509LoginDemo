﻿using System;
using System.Data.Entity;
using System.Threading.Tasks;
using UoW_MultipleDBContext.Data.Infrastructure;
using UoW_MultipleDBContext.Data.Repositories.Interface;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Data.UnitOfWork
{
    public interface IUnitOfWork<U> where U : DbContext, IDisposable
    {
        int Commit();
        Task<int> CommitAsync();

        /// <summary>
        /// Repository intefaces
        /// </summary>
        ICategoryRepository CategoryRepository { get; }

        IRepository<Department> DepartmentRepository { get; }

        IRepository<User> UserRepository { get; }
    }
}