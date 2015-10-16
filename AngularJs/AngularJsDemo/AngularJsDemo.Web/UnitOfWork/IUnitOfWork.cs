using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularJsDemo.Web.UnitOfWork
{
    public interface IUnitOfWork
    {
        bool IsCommitted { get; }

        int Commit(bool validateOnSaveEnable = true);

        void Rollback();
    }
}
