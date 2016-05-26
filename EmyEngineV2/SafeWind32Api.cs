using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EmyEngine
{
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

    }
    [Obsolete]
    public static class SafeWind32Api
    {

        public static bool IsRectEmpty(ref RECT rect)
        {
            return ((rect.left >= rect.right) || (rect.top >= rect.bottom));
        }
        public static bool SetRectEmpty(ref RECT rect)
        {
            rect.left = rect.right = rect.top = rect.bottom = 0;
            return true;
        }
        public static bool IntersectRect(ref RECT dest, ref RECT src1, ref RECT src2 )
        {
            //if (!dest || !src1 || !src2) return FALSE;
            if (IsRectEmpty(ref src1) || IsRectEmpty(ref src2) ||
                (src1.left >= src2.right) || (src2.left >= src1.right) ||
                (src1.top >= src2.bottom) || (src2.top >= src1.bottom))
            {
                SetRectEmpty(ref dest);
                return false;
            }
            dest.left   = Math.Max(src1.left, src2.left );
            dest.right  = Math.Min(src1.right, src2.right );
            dest.top    = Math.Max(src1.top, src2.top );
            dest.bottom = Math.Min(src1.bottom, src2.bottom );
            return true;
        }



    }
}
