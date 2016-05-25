using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
using System.Runtime.InteropServices;

namespace EmyEngine.OpenGL
{
    public class VertexObject : IDraweble
    {
        ~VertexObject()
        {
            if (GraphicsContext.CurrentContext != null && !GraphicsContext.CurrentContext.IsDisposed)
            {

                GL.DeleteVertexArray(VertexArrayObject);
                GL.DeleteBuffer(PositionBufferObject);
                GL.DeleteBuffer(NormalBufferObject);
                GL.DeleteBuffer(TextureCoordsBufferObject);
            }
        }
        public VertexObject() : this(PrimitiveType.Triangles) { }
        public VertexObject(PrimitiveType type)
        {
            DrawType = type;

            VertexArrayObject = GL.GenVertexArray();
            if(VertexArrayObject < 1)
                throw new GLInstanceNotCreated();
            GL.BindVertexArray(VertexArrayObject);

            PositionBufferObject = GL.GenBuffer();
            NormalBufferObject = GL.GenBuffer();
            TextureCoordsBufferObject = GL.GenBuffer();
            if (PositionBufferObject < 1 || NormalBufferObject < 1 || TextureCoordsBufferObject < 1)
                throw new GLInstanceNotCreated();

            GL.BindBuffer(BufferTarget.ArrayBuffer,PositionBufferObject);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0,3,VertexAttribPointerType.Float,false,0,(IntPtr)0);
         
            GL.BindBuffer(BufferTarget.ArrayBuffer, NormalBufferObject);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, (IntPtr)0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, TextureCoordsBufferObject);
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, (IntPtr)0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

         
        }


        public int VertexArrayObject { private set; get; }

        public int PositionBufferObject { private set; get; }
        public int NormalBufferObject { private set; get; }
        public int TextureCoordsBufferObject { private set; get; }


        public Material Material = Material.Defult;

        public Vertex this[int index]
        {
            set
            {
                if (index >= Size || index < 0)
                    throw new IndexOutOfRangeException();
                Positions[index] = value.Position;
                Normals[index] = value.Normal;
                TextureCoords[index] = value.TextureCoords;

            }
            get
            {
                if (index >= Size || index < 0)
                    throw new IndexOutOfRangeException();
                Vertex onrez = new Vertex();
                onrez.Position = Positions[index];
                onrez.Normal = Normals[index];
                onrez.TextureCoords = TextureCoords[index];
                return onrez;

            }
        }
        public int Size = 0;
        Vector3[] Positions = new Vector3[0];
        Vector3[] Normals = new Vector3[0];
        Vector2[] TextureCoords = new Vector2[0];
        public PrimitiveType DrawType { set; get; }

        public void SetPosition(int index,Vector3 vc)
        {         
            if (index >= Size || index < 0)
                throw new IndexOutOfRangeException();
            this.Positions[index] = vc;

        }
        public void SetNormal(int index, Vector3 vc)
        {
            if (index >= Size || index < 0)
                throw new IndexOutOfRangeException();
            this.Normals[index] = vc;

        }

        public void SetTextureCoord(int index, Vector2 vc)
        {
            if (index >= Size || index < 0)
                throw new IndexOutOfRangeException();
            this.TextureCoords[index] = vc;

        }

        public void PushArray(IEnumerable<Vertex> vertexes)
        {

            this.Size = vertexes.Count();
            Array.Resize(ref Positions, Size);
            Array.Resize(ref Normals, Size);
            Array.Resize(ref TextureCoords, Size);          
            IEnumerator<Vertex> v = vertexes.GetEnumerator();
            v.Reset();
            for (int i = 0; v.MoveNext() ; i++)
            {
                this[i] = v.Current;
            }        
        }



        public void CaptResize(int size)
        {       
            Array.Resize(ref Positions, size);
            Array.Resize(ref Normals, size);
            Array.Resize(ref TextureCoords, size);
        }

        public void AppendVertexStore(Vertex vertex)
        {
            Size++;       
            this[Size - 1] = vertex;
        }

        public void AppendVertex(Vertex vertex)
        {
            Size++;
            Array.Resize(ref Positions,Size);
            Array.Resize(ref Normals, Size);
            Array.Resize(ref TextureCoords, Size);
            this[Size - 1] = vertex;
        }

        bool ItsSaved = false;
        public void Save()
        {
            if (ItsSaved)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, PositionBufferObject);
                GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, (IntPtr)(Marshal.SizeOf(typeof(Vector3)) * Positions.Length), Positions);



                GL.BindBuffer(BufferTarget.ArrayBuffer, NormalBufferObject);
                GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, (IntPtr)(Marshal.SizeOf(typeof(Vector3)) * Normals.Length), Normals );

                GL.BindBuffer(BufferTarget.ArrayBuffer, TextureCoordsBufferObject);
                GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, (IntPtr)(Marshal.SizeOf(typeof(Vector2)) * TextureCoords.Length), TextureCoords);

                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            }
            else
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, PositionBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer,(IntPtr)(Marshal.SizeOf(typeof(Vector3)) * Positions.Length), Positions,BufferUsageHint.StaticDraw);
              
                GL.BindBuffer(BufferTarget.ArrayBuffer, NormalBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Marshal.SizeOf(typeof(Vector3)) * Normals.Length), Normals, BufferUsageHint.StaticDraw);
               

                GL.BindBuffer(BufferTarget.ArrayBuffer, TextureCoordsBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Marshal.SizeOf(typeof(Vector2)) * TextureCoords.Length), TextureCoords, BufferUsageHint.StaticDraw);
                

                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            }
            ItsSaved = true;

        }


        public void Draw()
        {
            if(this.Size != 0 && this.VertexArrayObject != 0)
            {
                G.UpdateShader(this.Material);
                GL.BindVertexArray(this.VertexArrayObject);
                GL.DrawArrays(DrawType, 0, Size);
                GL.BindVertexArray(0);
            }
           
        }


        public void Draw(PrimitiveType rendertype)
        {
            if (this.Size != 0 && this.VertexArrayObject != 0)
            {
                G.UpdateShader(this.Material);
                GL.BindVertexArray(this.VertexArrayObject);
                GL.DrawArrays(rendertype, 0, Size);
                GL.BindVertexArray(0);
            }
        }

        public void CalculateTrianglesNormals()
        {
            if (DrawType != PrimitiveType.Triangles)
                throw new Exception("Bad PrimitiveType: Нужен Трианжел");
            if((this.Size % 3) != 0)
                throw new Exception("Bad Triangles Array: Сайз в говно");

            for (int i = 0;i < this.Size;i = i + 3)
            {
                Vector3 calcualters = G.GetTriangleNormal(this.Positions[i], this.Positions[i + 1], this.Positions[i + 2]);

                this.Normals[i] = calcualters;
                this.Normals[i + 1] = calcualters;
                this.Normals[i + 2] = calcualters;

            }

        }

       
    }

}
