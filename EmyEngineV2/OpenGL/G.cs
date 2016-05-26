using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using EmyEngine.GUI;
using Jitter.LinearMath;

namespace EmyEngine.OpenGL
{
    public static class G
    {

        public struct Clip2D
        {
            public Clip2D(Vector2 lt, Vector2 rb)
            {
                this.lt = lt;
                this.rb = rb;
                 
            }

            public Vector2 lt;
            public Vector2 rb;

            public static Clip2D Indenty(int w,int h)
            {
                Clip2D pt;
                pt.lt = new Vector2(0, 0);
                pt.rb = new Vector2(w, h);
                return pt;
            }
        }


        public static Stack<Clip2D> ClipStack { set; get; } = new Stack<Clip2D>();
        public static Clip2D Clip;
        public static void PushClip()
        {
            ClipStack.Push(Clip);
        }
        public static void PopClip()
        {
            Clip = ClipStack.Pop();
        }
        public static void MultClip(Clip2D b)
        {
            RECT a;
            a.left = (int)b.lt.X;
            a.top = (int)b.lt.Y;
            a.right = (int)b.rb.X;
            a.bottom = (int)b.rb.Y;

            RECT e;
            e.left = (int)Clip.lt.X;
            e.top = (int)Clip.lt.Y;
            e.right = (int)Clip.rb.X;
            e.bottom = (int)Clip.rb.Y;

            RECT rz;
            rz.left = 0; rz.right = 0; rz.top = 0; rz.bottom = 0;
            SafeWind32Api.IntersectRect(ref rz, ref a, ref e);
            Clip.lt.X = rz.left;
            Clip.lt.Y = rz.top;
            Clip.rb.X = rz.right;
            Clip.rb.Y = rz.bottom;

        }





        #region Draweble
        public static IGraphics Graphics { set; get; }
        public static IShaderInstance Instance {set;get;}
        public static void UpdateShader(Material Material)
        {
            
            GL.UseProgram(Instance.Program.Handle);     
            Instance.UpdateState(Material);
   
        }

        public static void SetContextZero()
        {
            for (int i = 0; i < 32; i++)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + i);
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
            GL.BindBuffer(BufferTarget.ArrayBuffer,0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer,0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);         
        }


        #endregion







        #region MATRIX CONVISIONS
        static Matrix4 TransformBuffer = new Matrix4();
        public static void SetTransform(JVector pos, JMatrix matrix)
        {
            TransformBuffer.Row0.X = matrix.M11;
            TransformBuffer.Row0.Y = matrix.M12;
            TransformBuffer.Row0.Z = matrix.M13;
            TransformBuffer.Row0.W = 0.0f;
            TransformBuffer.Row1.X = matrix.M21;
            TransformBuffer.Row1.Y = matrix.M22;
            TransformBuffer.Row1.Z = matrix.M23;
            TransformBuffer.Row1.W = 0.0f;
            TransformBuffer.Row2.X = matrix.M31;
            TransformBuffer.Row2.Y = matrix.M32;
            TransformBuffer.Row2.Z = matrix.M33;
            TransformBuffer.Row2.W = 0.0f;
            TransformBuffer.Row3.X = pos.X;
            TransformBuffer.Row3.Y = pos.Y;
            TransformBuffer.Row3.Z = pos.Z;
            TransformBuffer.Row3.W = 1f;
            G.MultMatrix(TransformBuffer);         
        }


        static Stack<Matrix4> MatrixStack = new Stack<Matrix4>(20);
        static MatrixType CurrentMatrixType = MatrixType.Projection;
        public static Matrix4 Model, View, Projection, Bias;
        public static Vector3 GetTriangleNormal(Vector3 A, Vector3 B, Vector3 C)
        {
            float x1 = A.X;
            float x2 = B.X;
            float x3 = C.X;
            float y1 = A.Y;
            float y2 = B.Y;
            float y3 = C.Y;
            float z1 = A.Z;
            float z2 = B.Z;
            float z3 = C.Z;
            Vector3 vec = Vector3.Zero;
            vec.X = (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1);
            vec.Y = (z2 - z1) * (x3 - x1) - (z3 - z1) * (x2 - x1);
            vec.Z = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);
            return vec;
        }
        public static Vector3 GetLineNormal(Vector3 A, Vector3 B)
        {
            Vector3 C = B;
            float x1 = A.X;
            float x2 = B.X;
            float x3 = C.X;
            float y1 = A.Y;
            float y2 = B.Y;
            float y3 = C.Y;
            float z1 = A.Z;
            float z2 = B.Z;
            float z3 = C.Z;
            Vector3 vec = Vector3.Zero;
            vec.X = (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1);
            vec.Y = (z2 - z1) * (x3 - x1) - (z3 - z1) * (x2 - x1);
            vec.Z = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);
            return vec;
        }
        public static Matrix4 GetMatrixRezult()
        {
            return Model * (View * Projection);
        }
        public static void Translate(float X, float Y, float Z)
        {
            MultMatrix(Matrix4.CreateTranslation(X, Y, Z));
        }
        public static void Translate(Vector3 v)
        {
            MultMatrix(Matrix4.CreateTranslation(v));
        }

        public static void Scale(float X, float Y, float Z)
        {
            MultMatrix(Matrix4.CreateScale(X, Y, Z));
        }
        public static void Rotate(float angle, float X, float Y, float Z)
        {
       
            MultMatrix(Matrix4.CreateFromAxisAngle(new Vector3(X, Y, Z), angle));
        }
        public static void PushMatrix()
        {
            MatrixStack.Push(CurrentMatrix);
        }
        public static void PopMatrix()
        {
            CurrentMatrix = MatrixStack.Pop();
        }
        public static Matrix4 CurrentMatrix
        {
            set
            {
                switch (CurrentMatrixType)
                {
                    case MatrixType.Model:
                        Model = value;
                        break;
                    case MatrixType.View:
                        View = value;
                        break;
                    case MatrixType.Projection:
                        Projection = value;
                        break;
                    default:
                        throw new BadMatrixException();
                }

            }
            get
            {
                switch (CurrentMatrixType)
                {
                    case MatrixType.Model:
                        return Model;   
                    case MatrixType.View:
                        return View;                
                    case MatrixType.Projection:
                        return Projection;
                    default:
                        throw new BadMatrixException();
                }

            }


        }
        public static void MatrixMode(MatrixType type)
        {
            CurrentMatrixType = type;
        }
        public static void LoadIndenty()
        {
            CurrentMatrix = Matrix4.Identity;
        }
        public static void MultMatrix(Matrix4 mat)
        {
            CurrentMatrix = mat * CurrentMatrix;
        }

      

        #endregion


    }

    public enum MatrixType
    {
        Model, View, Projection
    }

    public enum DrawPrimitive
    {
        Triangles, Lines, Points
    }

    public class BadMatrixException : Exception
    {
        public BadMatrixException() : base("Матрицы с таким типом нету.") { }
    }
}
