using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
using System.Runtime.InteropServices;
using LibPtr;
using System.Collections;
using System.ComponentModel;

namespace EmyEngine.OpenGL
{


    [TypeConverter(typeof(VertexObjectConverter))]
    public class VertexObject : AutoRealaseSafeUnmanagmentClass, IDraweble, IEnumerable, IEnumerable<Vertex>
    {

        public VertexObject() : this(PrimitiveType.Triangles) { }
        public VertexObject(PrimitiveType type)
        {
            DrawType = type;
            Acquire();

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



        public void SetPosition(int index, Vector3 vc)
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

        public int Capacity
        {
            get
            {
                if ((this.Positions.Length | this.TextureCoords.Length | this.Normals.Length) != this.Positions.Length)
                    throw new Exception("Потеря синхронизации вершинного, текстурного и нормального буфера");
                return this.Positions.Length;
            }
            set
            {
                if (value < Size)
                    throw new Exception("Запас не может быть меньше размера текущего масива вершин");
                Vector3[] _poss = new Vector3[value];
                Vector3[] _noss = new Vector3[value];
                Vector2[] _toss = new Vector2[value];

                Array.Copy(this.Positions, _poss, Size);
                Array.Copy(this.Normals, _noss, Size);
                Array.Copy(this.TextureCoords, _toss, Size);
                this.Positions = _poss;
                this.Normals = _noss;
                this.TextureCoords = _toss;
            }
        }

        public int IndexOf(Vertex vert)
        {
            for (int i = 0; i < Size; i++)
            {
                if (this[i] == vert)
                    return i;
            }
            return -1;
        }
        public void DeleteAt(Vertex s)
        {
            DeleteAt(IndexOf(s));
        }
        public void DeleteAt(int idx)
        {
            if (idx == -1) return;
            if (idx > (Size - 1))
                throw new ArgumentOutOfRangeException();
            int blockSize = Size - 1 - idx;
            int blockIdx = idx + 1;
            Array.Copy(this.Positions, blockIdx, this.Positions, idx, blockSize);
            Size--;
        }

        private void ChekCapacity(int size)
        {
            if (size > Capacity)
            {
                Capacity = size;
            }         
        }
        
        
        public void AppendVertex(Vertex vertex)
        {          
            ChekCapacity(Size + 1);
            Positions[Size] = vertex.Position;
            Normals[Size] = vertex.Normal;
            TextureCoords[Size] = vertex.TextureCoords;
            Size++;
        }




        private int _size = 0;
        public int Size
        {
            set
            {
                ChekCapacity(value);
                _size = value;
            }
            get
            {
                return _size;
            }
        }
        Vector3[] Positions = new Vector3[0];
        Vector3[] Normals = new Vector3[0];
        Vector2[] TextureCoords = new Vector2[0];
        public PrimitiveType DrawType { set; get; }

       
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

        public override DisposeInformation Realase()
        {
            if (GraphicsContext.CurrentContext != null && !GraphicsContext.CurrentContext.IsDisposed)
            {

                GL.DeleteVertexArray(VertexArrayObject);
                GL.DeleteBuffer(PositionBufferObject);
                GL.DeleteBuffer(NormalBufferObject);
                GL.DeleteBuffer(TextureCoordsBufferObject);
            }
            else
                return DisposeInformation.Error;
            return DisposeInformation.Sucsses;
        }

        public override void Acquire()
        {
            IsDisposed = false;
            VertexArrayObject = GL.GenVertexArray();
            if (VertexArrayObject < 1)
                throw new GLInstanceNotCreated();
            GL.BindVertexArray(VertexArrayObject);

            PositionBufferObject = GL.GenBuffer();
            NormalBufferObject = GL.GenBuffer();
            TextureCoordsBufferObject = GL.GenBuffer();
            if (PositionBufferObject < 1 || NormalBufferObject < 1 || TextureCoordsBufferObject < 1)
                throw new GLInstanceNotCreated();

            GL.BindBuffer(BufferTarget.ArrayBuffer, PositionBufferObject);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, (IntPtr)0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, NormalBufferObject);
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, (IntPtr)0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, TextureCoordsBufferObject);
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, (IntPtr)0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

        }

        public class VertexObjectEnumerator : IEnumerator<Vertex>, IEnumerator
        {
            private VertexObject _object = null;

            private int idx = 0;

            private Vertex _cur;

            public VertexObjectEnumerator(VertexObject _object)
            {
                this._object = _object;
                this.idx = -1;
            }

            public Vertex Current
            {
                get { return _cur; }
            }

            object IEnumerator.Current
            {
                get { return _cur; }
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                idx++;
                if (idx > _object.Size - 1)
                    return false;
                else
                {
                    _cur = _object[idx];
                    return true;
                }
            }

            public void Reset()
            {
                this.idx = -1;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new VertexObjectEnumerator(this);
        }

        IEnumerator<Vertex> IEnumerable<Vertex>.GetEnumerator()
        {
            return new VertexObjectEnumerator(this);
        }
    }

}
