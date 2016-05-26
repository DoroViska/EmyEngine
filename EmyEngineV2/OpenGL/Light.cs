using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using System.Runtime.InteropServices;

namespace EmyEngine.OpenGL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LightSource
    {
        public int Type;
        public Vector3 Position;
        public Vector3 Attenuation;
        public Vector3 Direction;
        public Vector3 Colour;
        public float OuterCutoff;
        public float InnerCutoff;
        public float Exponent;
    }
}
