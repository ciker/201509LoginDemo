using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
    public abstract class DisposableBase : IDisposable
    {
        bool _isDisposed;

        [DebuggerStepThrough]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                OnDisposing(disposing);
            }
            _isDisposed = true;
        }

        protected abstract void OnDisposing(bool disposing);

        ~DisposableBase()
        {
            Dispose(false);
        }
    }
}
