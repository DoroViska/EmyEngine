using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibPtr
{



   

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
        public bool IsDisposed {
            set { _isDisposed = value; }
            get{ return _isDisposed; }
        }



        private DisposeInformation _disposeStatus = DisposeInformation.Empty;
        private void _suppress()
        {
            _disposeStatus = Realase();           
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


        public abstract DisposeInformation Realase();
        public abstract void Acquire();

        public DisposeInformation DisposeStatus
        {
            get { return _disposeStatus; }
          
        }
    }
}
