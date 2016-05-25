using EmyEngine.GL4;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinForms3DRender
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Face
    {

        public static Face Vector(Vector3 a, Vector3 b, Vector3 c)
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
            z.A = Vector3.Zero;
            z.B = Vector3.Zero;
            z.C = Vector3.Zero;
            return z;
        }

        public Vector3 Normal()
        {
            float x1 = this.A.X;
            float x2 = this.B.X;
            float x3 = this.C.X;
            float y1 = this.A.Y;
            float y2 = this.B.Y;
            float y3 = this.C.Y;
            float z1 = this.A.Z;
            float z2 = this.B.Z;
            float z3 = this.C.Z;
            Vector3 vec = Vector3.Zero;
            vec.X = (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1);
            vec.Y = (z2 - z1) * (x3 - x1) - (z3 - z1) * (x2 - x1);
            vec.Z = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);
            return vec;
        }


        public Vector3 A;
        public Vector3 B;
        public Vector3 C;
        public Vector3 this[int s]
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
                return Vector3.Zero;
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







    public class XMLModel
    {
        FlatModel flat;
  
        public void LoadFromFile(string filename, string dir)
        {
            using(System.IO.Stream t = System.IO.File.OpenRead(filename))
            {

                XmlSerializer sr = new XmlSerializer(typeof(FlatModel));
                flat = (FlatModel)sr.Deserialize(t);
            }
            
        }

        VertexVert vr;
        public void Draw()
        {


            vr.Draw();
        }
        public void Create()
        {
       
            vr = new VertexVert();

            foreach (ModelSubObject sb in flat.SubObjects)
            {

            

                for (int i = 0; i < sb.Faces.Count; i++)
                {



                    vr.AppendVertex(new Vertex(sb.Faces[i].A));
                    vr.AppendVertex(new Vertex(sb.Faces[i].B));
                    vr.AppendVertex(new Vertex(sb.Faces[i].C));



                }
            

            }
            vr.Save();

        }

    }
}
