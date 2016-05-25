using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable
namespace EmyEngine.GL4
{
    public enum MatrixType
    {
        Model,View,Projection
    }

    public enum DrawPrimitive
    {
        Triangles, Lines, Points
    }

    public class BadMatrixException : Exception
    {
        public BadMatrixException() : base("Матрицы с таким типом нету.") { }
    }

    /// <summary>
    /// Это глабальный toolkit 
    /// </summary>
    public static class EG
    {

        public static readonly Matrix4 IndentyYInverted = new Matrix4(Vector4.UnitX, -Vector4.UnitY, Vector4.UnitZ, Vector4.UnitW);


        #region SLOWRENDER
        public static ShaderProgram BaseShader = null;

        static Vector3[] P = new Vector3[4];
        static Vector3[] N = new Vector3[4];
        static Color4[] C = new Color4[4];
        static Vector2[] T = new Vector2[4];


        private static VAO B = null;
        private static VBO BP = null;
        private static VBO BN = null;
        private static VBO BC = null;
        private static VBO BT = null;

        private static int ViewLoc = 0;
        private static int ModelLoc = 0;
        private static int ProjectionLoc = 0;
        private static int BiasLoc = 0;

        private static int TextureLoc = 0;
        private static int CAmbientLoc = 0;
        private static int CSpecularLoc = 0;
        private static int LPosLoc = 0;
        private static int LEnableLoc = 0;
        private static int ShadowMapLoc = 0;

        public static Vector3 LightPosition { get { return LPos; } }

        private static  Vector3  LPos;
        private static Vector4 CAmbient,CSpecular;
        private static int _LightEnable = 0;

        public static bool LightEnable {
            set
            {
                if (value == true)
                    _LightEnable = 1;
                else
                    _LightEnable = 0;
            }
            get
            {
                if (_LightEnable == 0)
                    return false;
                else
                    return true;
            }
        }
        public static Matrix4 Bias;
        public static int ShadowMap;

        public static Action NotDefultUpdateStates { set; get; }
        public static ShaderProgram NotDefultProgram { set; get; }        
        public static void UpdateStates(Color4 Ambient, Color4 Specular)
        {
            if (NotDefultProgram != null)
            {
                NotDefultProgram.Use();
                NotDefultUpdateStates();
                return;
            }
            BaseShader.Use();
            CAmbient = new Vector4(Ambient.R, Ambient.G, Ambient.B, Ambient.A);
            CSpecular = new Vector4(Specular.R, Specular.G, Specular.B, Specular.A);



            
            GL.Uniform1(ShadowMapLoc, ShadowMap);
            GL.Uniform1(LEnableLoc, _LightEnable);
            GL.Uniform3(LPosLoc, ref LPos);
            GL.Uniform4(CAmbientLoc, ref CAmbient);
            GL.Uniform4(CSpecularLoc, ref CSpecular);
            GL.UniformMatrix4(ModelLoc, false, ref EG.Model);
            GL.UniformMatrix4(ViewLoc, false, ref EG.View);
            GL.UniformMatrix4(ProjectionLoc, false, ref EG.Projection);
            GL.UniformMatrix4(BiasLoc, false, ref EG.Bias);
        }


        public static void Sampler(int texureObject)
        {
            GL.BindTexture(TextureTarget.Texture2D,texureObject);
            GL.Uniform1(TextureLoc, texureObject);
            
        }
        public static void ClearContext()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.UseProgram(0);
        }


        public static void Init()
        {
            NotDefultProgram = null;
            BaseShader = ShaderProgram.FromSrcDir("basic");
            ViewLoc = BaseShader.GetUniformLocation("View");
            BiasLoc = BaseShader.GetUniformLocation("Bias");
            ModelLoc = BaseShader.GetUniformLocation("Model");
            ProjectionLoc = BaseShader.GetUniformLocation("Projection");
            TextureLoc = BaseShader.GetUniformLocation("Texture");
            CAmbientLoc = BaseShader.GetUniformLocation("CAmbient");
            CSpecularLoc = BaseShader.GetUniformLocation("CSpecular");
            LPosLoc = BaseShader.GetUniformLocation("LPos");
            LEnableLoc = BaseShader.GetUniformLocation("LEnable"); 
    
            ShadowMapLoc = BaseShader.GetUniformLocation("ShadowMap"); 

            LPos = new Vector3(0f,5f, 0f);         
            CAmbient = new Vector4(0.1f, 0.1f, 0.1f, 1f);
            CSpecular = new Vector4(1f,1f,1f,1f);
      


            B = new VAO(4);
            BP = new VBO();
            BP.SetData(P);
            B.AttachVBO(0, BP, 3, OpenTK.Graphics.OpenGL4.VertexAttribPointerType.Float, 0);

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

    

        public static void DrawLine(Vector3 vec, Vector3 vec1, Color4 color)
        {
            P[0] = vec;
            P[1] = vec1;

            C[0] = color;
            C[1] = color;

            BP.SubData(P);
            //BN.SubData(N);
            BC.SubData(C);
            //BT.SubData(T);
            UpdateStates(new Color4(0.1f, 0.1f, 0.1f, 1.0f),Color4.White);
            B.Draw(PrimitiveType.Lines, 2);
        }

        public static void DrawTriangle(Vector3 vec, Vector3 vec1, Vector3 vec2, Color4 color)
        {

            P[0] = vec;
            P[1] = vec1;
            P[2] = vec2;

            C[0] = color;
            C[1] = color;
            C[2] = color;

            Vector3 normal = EG.GetTriangleNormal(vec, vec1, vec2);
            N[0] = normal;
            N[1] = normal;
            N[2] = normal;

            BP.SubData(P);
            BN.SubData(N);
            BC.SubData(C);
            //BT.SubData(T);
            UpdateStates(new Color4(0.1f, 0.1f, 0.1f, 1.0f), Color4.White);
            B.Draw(PrimitiveType.Triangles, 3);
        }






        #endregion

        #region MATRIX CONVISIONS

        static Stack<Matrix4> MatrixStack = new Stack<Matrix4>();
        static MatrixType CurrentMatrixType = MatrixType.Projection;

        public static Matrix4 Projection;
        public static Matrix4 Model,View;


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



        public static Matrix4 GetRezult()
        {
            return Model * View * Projection;
        }

        public static void Translate(float X,float Y,float Z)
        {           
            MultMatrix(Matrix4.CreateTranslation(X, Y, Z));
        }
        public static void Scale(float X, float Y, float Z)
        {
            MultMatrix(Matrix4.CreateScale(X, Y, Z));
        }
        public static void Rotate(float angle, float X, float Y, float Z)
        {
            MultMatrix(Matrix4.Rotate(new Vector3(X, Y, Z),angle));
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
                        break;
                    case MatrixType.View:
                        return View;
                        break;
                    case MatrixType.Projection:
                        return Projection;
                        break;
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
            //if (CurrentMatrixType == MatrixType.Model)
            //{
            //    Model = Matrix4.Identity;
            //    Model.M22 = -1f;
            //    return;
            //}
            CurrentMatrix = Matrix4.Identity;          
        }

        public static void MultMatrix(Matrix4 mat)
        {
            CurrentMatrix = mat * CurrentMatrix;
        }

    

        #endregion

    }
}
