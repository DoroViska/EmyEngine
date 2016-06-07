
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmyEngine
{

    public struct Binder : IDisposable
    {
        private IBinderable _context;
        public Binder(IBinderable cntx)
        {
            if (cntx != null)
                cntx.Use();
            this._context = cntx;
        }

        public void Dispose()
        {
            if(_context != null)
                _context.UnUsed();
        }
    }
}
