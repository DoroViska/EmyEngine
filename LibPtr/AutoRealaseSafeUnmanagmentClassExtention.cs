using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibPtr
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
}
