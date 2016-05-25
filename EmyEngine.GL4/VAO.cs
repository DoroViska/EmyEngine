using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.GL4
{
    public sealed class VAO 
    {

        public int VertexArrayObject { get; private set; }
        public int VertexCount { get; private set; } // Число вершин для отрисовки

        public VAO(int vertexCount)
        {
            VertexCount = vertexCount;
            AcquireHandle();
        }

        private void AcquireHandle()
        {
            VertexArrayObject = GL.GenVertexArray();
        }

        public void Use()
        {
            GL.BindVertexArray(VertexArrayObject);
        }

        public void AttachVBO(int index, VBO vbo, int elementsPerVertex, VertexAttribPointerType pointerType, int offset) //where T : struct
        {
            Use();
            vbo.Use();
            GL.EnableVertexAttribArray(index);
            GL.VertexAttribPointer(index, elementsPerVertex, pointerType, false, 0, offset);
        }

        public void Draw(OpenTK.Graphics.OpenGL4.PrimitiveType RenderType,int VertexesCounts)
        {
            Use();
            GL.DrawArrays(RenderType, 0, VertexesCounts);
            GL.BindVertexArray(0);
        }

        private void ReleaseHandle()
        {
            if (VertexArrayObject == 0)
                return;

            GL.DeleteVertexArray(VertexArrayObject);

            VertexArrayObject = 0;
        }

   

        ~VAO()
        {
            if (GraphicsContext.CurrentContext != null && !GraphicsContext.CurrentContext.IsDisposed)
                ReleaseHandle();
        }
    }
}
