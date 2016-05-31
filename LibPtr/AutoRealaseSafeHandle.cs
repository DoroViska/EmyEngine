using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtoApp1.Desktop
{



    public static class AutoRealaseSafeUnmanagmentClassExtention
    {
        public static bool IsSucssesDisposed(this AutoRealaseSafeUnmanagmentClass current)
        {
            return current.DisposeStatus == DisposeInformation.Sucsses;
        }

        public static bool IsErrorDisposed(this AutoRealaseSafeUnmanagmentClass current)
        {
            return current.DisposeStatus == DisposeInformation.Error;
        }

        public static bool IsDisposed(this AutoRealaseSafeUnmanagmentClass current)
        {
            return current.DisposeStatus != DisposeInformation.Empty;
        }
    }

    public abstract class AutoRealaseSafeUnmanagmentClass : IDisposebleStatus, IDisposable
    {
        ~AutoRealaseSafeUnmanagmentClass()
        {
            if (!_isDisposed)
            {
                _suppress();
                _isDisposed = true;
            }
        }
        
        private bool _isDisposed = false;
        private DisposeInformation _disposeStatus = DisposeInformation.Empty;
        private void _suppress()
        {
            _disposeStatus = RealaseHandle();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _suppress();
                _isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }     


        public abstract DisposeInformation RealaseHandle();
        public abstract void LockHandle();

        public DisposeInformation DisposeStatus
        {
            get { return _disposeStatus; }
          
        }
    }
}
