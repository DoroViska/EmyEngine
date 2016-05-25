using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmyEngine.Platform
{
    [Serializable]
    public unsafe struct Color
    {
        public override bool Equals(object obj)
        {
            if (obj == null && !(obj is Color))
                return false;
            if (((Color)obj).R == this.R && ((Color)obj).G == this.G && ((Color)obj).B == this.B && ((Color)obj).A == this.A)
                return true;
            else
                return false;
        }
        public override int GetHashCode()
        {
            return Tuple.Create(R, G, B, A).GetHashCode();
        }


        public static bool operator ==(Color a, Color b)
        {
            if (a.A != b.A) return false;
            if (a.R != b.R) return false;
            if (a.G != b.G) return false;
            if (a.B != b.B) return false;
            return true;
        }
        public static bool operator !=(Color a, Color b)
        {
            if (a.A == b.A) return false;
            if (a.R == b.R) return false;
            if (a.G == b.G) return false;
            if (a.B == b.B) return false;
            return true;
        }





        public static Color Bytes(byte r, byte g, byte b)
        {
            Color c;
            c.R = r;
            c.G = g;
            c.A = 255;
            c.B = b;
            return c;
        }
        public static Color Bytes(byte r, byte g, byte b, byte a)
        {
            Color c;
            c.R = r;
            c.G = g;
            c.A = a;
            c.B = b;
            return c;
        }
        public static Color Zero()
        {
            Color c;
            c.R = 0;
            c.G = 0;
            c.A = 0;
            c.B = 0;
            return c;
        }

        public static Color Red { get { return Color.Bytes(255, 0, 0, 255); } }
        public static Color Green { get { return Color.Bytes(0, 255, 0, 255); } }
        public static Color Blue { get { return Color.Bytes(0, 0, 255, 255); } }
        public static Color Black { get { return Color.Bytes(0, 0, 0, 255); } }
        public static Color White { get { return Color.Bytes(255, 255, 255, 255); } }

        public const float FloatFriction = 1f / 255f;



        public byte R;
        public byte G;
        public byte B;
        public byte A;
    }
}
