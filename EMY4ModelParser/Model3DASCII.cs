using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Face
    {

        public static Face Vector(Vector3D a, Vector3D b, Vector3D c)
        {
            Face z;
            z.A = a;
            z.B = b;
            z.C = c;
            return z;
        }

        public static Face Zero()
        {
            Face z;
            z.A = Vector3D.Zero();
            z.B = Vector3D.Zero();
            z.C = Vector3D.Zero();
            return z;
        }




        public Vector3D A;
        public Vector3D B;
        public Vector3D C;
        public Vector3D this[int s]
        {
            set
            {
                if (s == 0)
                    A = value;
                if (s == 1)
                    B = value;
                if (s == 2)
                    C = value;
            }
            get
            {
                if (s == 0)
                    return A;
                if (s == 1)
                    return B;
                if (s == 2)
                    return C;
                return Vector3D.Zero();
            }
        }
    }



    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class ModelSubObject
    {
        public int TextureId = -1;
        public bool NeedTexture
        {
            get
            {
                if (TextureId < 0)
                    return false;
                return true;
            }
        }

        public List<Face> Faces;
        public List<Face> TFaces;

    }


    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class TextureObject
    {
        public string TextureName;
        public uint NativeHandle;
    }


    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class FlatModel
    {
        public List<ModelSubObject> SubObjects;
        public List<TextureObject> Textures;
    }


}
