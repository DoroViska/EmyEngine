using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmyEngine.Platform
{
    public class FunctionReturnedNullException : Exception
    {
        public FunctionReturnedNullException(string funcname) : base(funcname + ": вернула NULL") { }
    }

    public class BitMapBadFormatException : Exception
    {
        public BitMapBadFormatException(int bytes_per_pixels) : base("поддерживаются картинки только с форматом RGB-8,RGB-24,RGBA-32. Требуемый формат ?-" + bytes_per_pixels + " байт") { }
    }

}
