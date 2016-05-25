using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
using OpenTK.Graphics;

namespace EmyEngine.GL4
{
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct Vertex
    {
        public Vertex(float x,float y,float z)
        {
            Position = new Vector3(x,y,z);
            Color = new Color4(1f,1f,1f,1f);
            TextureCoords = new Vector2(0f,0f);
            Normal = new Vector3(0f, 0f, 0f);
        }
        public Vertex(Vector3 pos)
        {
            Position = pos;
            Color = new Color4(1f, 1f, 1f, 1f);
            TextureCoords = new Vector2(0f, 0f);
            Normal = new Vector3(0f, 0f, 0f);
        }


        public Vertex(float x, float y, float z,Color4 color)
        {
            Position = new Vector3(x, y, z);
            Color = color;
            TextureCoords = new Vector2(0f, 0f);
            Normal = new Vector3(0f, 0f, 0f);
        }
        public Vertex(Vector3 pos, Color4 color)
        {
            Position = pos;
            Color = color;
            TextureCoords = new Vector2(0f, 0f);
            Normal = new Vector3(0f, 0f, 0f);
        }


        public Vertex(float x, float y, float z,float u,float v)
        {
            Position = new Vector3(x, y, z);
            Color = new Color4(1f, 1f, 1f, 1f);
            TextureCoords = new Vector2(u, v);
            Normal = new Vector3(0f, 0f, 0f);
        }
        public Vertex(Vector3 pos,Vector2 tex)
        {
            Position = pos;
            Color = new Color4(1f, 1f, 1f, 1f);
            TextureCoords = tex;
            Normal = new Vector3(0f,0f,0f);
        }


        public Vector3 Position;
        public Vector3 Normal;
        public Color4 Color;
        public Vector2 TextureCoords;


    }
}
