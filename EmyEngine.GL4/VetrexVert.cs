using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.GL4
{
    public class VertexVert : IRenderer
    {


        private VAO B = null;
        private VBO BP = null;
        private VBO BN = null;
        private VBO BC = null;
        private VBO BT = null;

        private Vector3[] P;
        private Vector3[] N;
        private Color4[] C;
        private Vector2[] T;

        public List<Vertex> Vertexes { set; get; } = new List<Vertex>();
        public bool AutoNormal { set; get; } = true;
        public int DefuseTextureObject { set; get; } = 0;
        public OpenTK.Graphics.OpenGL4.PrimitiveType RenderType { set; get; } = OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles;

        public Color4 Ambient { set; get; } = new Color4(0.1f, 0.1f, 0.1f, 1f);
        public Color4 Specular { set; get; } = new Color4(1f, 1f, 1f, 1f);
        public Color4 Defuse { set; get; } = new Color4(1f, 1f, 1f, 1f);

        public void Save()
        {


            B = new VAO(Vertexes.Count);
            P = new Vector3[Vertexes.Count];
            N = new Vector3[Vertexes.Count];
            C = new Color4[Vertexes.Count];
            T = new Vector2[Vertexes.Count];
            for (int i = 0; i < Vertexes.Count; i++)
            {
                P[i] = Vertexes[i].Position;
                N[i] = Vertexes[i].Normal;

                C[i].R = Vertexes[i].Color.R * Defuse.R;
                C[i].G = Vertexes[i].Color.G * Defuse.G;
                C[i].B = Vertexes[i].Color.B * Defuse.B;
                C[i].A = Vertexes[i].Color.A * Defuse.A;

                T[i] = Vertexes[i].TextureCoords;
            }
            if(AutoNormal)
                for (int i = 0; i < Vertexes.Count; i = i + 3)
                {
                    Vector3 n = EG.GetTriangleNormal(P[i], P[i + 1], P[i + 2]);
                    n.Normalize();
                    N[i] = n;
                    N[i + 1] = n;
                    N[i + 2] = n;
                   
                }

            



            BP = new VBO();
            BP.SetData(P);
            B.AttachVBO(0,BP,3,OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float,0);

            BN = new VBO();
            BN.SetData(N);
            B.AttachVBO(1, BN, 3, OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 0);

            BC = new VBO();
            BC.SetData(C);
            B.AttachVBO(2, BC, 4, OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 0);

            BT = new VBO();
            BT.SetData(T);
            B.AttachVBO(3, BT, 2, OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 0);
       


        }

        public void AppendVertex(Vertex vertex)
        {
            Vertexes.Add(vertex);
        }
        public void AppendVertex(Vector3 vertex)
        {
            Vertexes.Add(new Vertex(vertex));
        }
        public void AppendVertex(Vector3 vertex,Vector2 texture)
        {
            Vertexes.Add(new Vertex(vertex.X,vertex.Y,vertex.Z,texture.X,texture.Y));
        }
        public void AppendVertex(float X, float Y, float Z, float U, float V)
        {
            Vertexes.Add(new Vertex(X, Y, Z, U, V));
        }

        public void Draw()
        {
            EG.UpdateStates(Ambient, Specular);
            B.Draw(RenderType,Vertexes.Count);
        }
    }
}
