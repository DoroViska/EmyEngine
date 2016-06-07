using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace EmyEngine.OpenGL
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoords;

        public override string ToString()
        {
            return string.Format("[V {0}][N {1}][T {2}]", this.Position, this.Normal, this.TextureCoords);
        }

        public override int GetHashCode()
        {
            return this.Position.GetHashCode() ^ this.TextureCoords.GetHashCode() * this.Normal.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
                if (obj is Vertex)
                    return ((Vertex)obj).Position == this.Position && ((Vertex)obj).TextureCoords == this.TextureCoords && ((Vertex)obj).Normal == this.Normal;
            return false;
        }

        public static bool operator ==(Vertex a, Vertex b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vertex a, Vertex b)
        {
            return !a.Equals(b);
        }

        public Vertex(float x, float y, float z)
        {
         
            TextureCoords = new Vector2(0f, 0f);
            Position = new Vector3(x,y,z);
            Normal = new Vector3(0f, 0f, 0f);
        }    

        public Vertex(Vector3 pos)
        {
            Position = pos;          
            TextureCoords = new Vector2(0f, 0f);
            Normal = new Vector3(0f, 0f, 0f);
        }

        public Vertex(float x, float y, float z, float u, float v)
        {
            Position = new Vector3(x, y, z);
           
            TextureCoords = new Vector2(u, v);
            Normal = new Vector3(0f, 0f, 0f);
        }

        public Vertex(Vector3 pos, Vector2 tex)
        {
            Position = pos;
       
            TextureCoords = tex;
            Normal = new Vector3(0f, 0f, 0f);
        }
   
    }
}
