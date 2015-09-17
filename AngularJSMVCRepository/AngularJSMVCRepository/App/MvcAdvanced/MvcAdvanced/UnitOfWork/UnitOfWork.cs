using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcAdvanced.GenericRepository;

namespace MvcAdvanced.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private MvcAdvancedEntities context = new MvcAdvancedEntities();
       
        private GenericRepository<Employee> employeeRepository;
        private GenericRepository<EmpRole> empRoleRepository;

        public GenericRepository<Employee> EmployeeRepository
        {
            get
            {
                if (this.employeeRepository == null)
                    this.employeeRepository = new GenericRepository<Employee>(context);
                return employeeRepository;
            }
        }

        public GenericRepository<EmpRole> EmpRoleRepository
        {
            get
            {
                if (this.empRoleRepository == null)
                    this.empRoleRepository = new GenericRepository<EmpRole>(context);
                return empRoleRepository;
            }
        }   

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            return;

            if (disposing)
            {
                //Free any other managed objects here. 
                context.Dispose();
            }

            // Free any unmanaged objects here. 
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}