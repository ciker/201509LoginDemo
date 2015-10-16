using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularJsDemo.Web.DB.Models;

namespace AngularJsDemo.Web.UnitOfWork
{
    public interface IUnitOfWorkContext : IUnitOfWork, IDisposable
    {
        void RegisterNew<T, TKey>(T t) where T : EntityBase<TKey>;
        void RegisterNew<T, TKey>(IEnumerable<T> t) where T : EntityBase<TKey>;


        void RegisterModified<T, TKey>(T t) where T : EntityBase<TKey>;
        void RegisterModified<T, TKey>(IEnumerable<T> t) where T : EntityBase<TKey>;


        void RegisterDeleted<T, TKey>(T t) where T : EntityBase<TKey>;
        void RegisterDeleted<T, TKey>(IEnumerable<T> t) where T : EntityBase<TKey>;
    }
}