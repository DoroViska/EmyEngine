using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EmyEngine.Imaging
{
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct UniColor1
    {
        public byte C;
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct UniColor2
    {
        public byte B, W;
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct UniColor3
    {
        public byte R, G, B;
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct UniColor4
    {
        public byte R, G, B, A;
    }

    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct UniColor2F
    {
        public float B, W;
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct UniColor3F
    {
        public float R, G, B;
    }
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct UniColor4F
    {
        public float R, G, B, A;
    }

}
