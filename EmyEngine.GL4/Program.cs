using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
using System.IO;
using WinForms3DRender;

namespace EmyEngine.GL4
{
    class Program
    {
        static Vertex[] arrray = new Vertex[] {

        new Vertex(     -1.0f,-1.0f,-1.0f), // triangle 1 : begin
            new Vertex( -1.0f,-1.0f, 1.0f),
           new Vertex(  -1.0f, 1.0f, 1.0f), // triangle 1 : end
          new Vertex(   1.0f, 1.0f,-1.0f), // triangle 2 : begin
          new Vertex(   -1.0f,-1.0f,-1.0f),
          new Vertex(   -1.0f, 1.0f,-1.0f), // triangle 2 : end
          new Vertex(   1.0f,-1.0f, 1.0f),
          new Vertex(   -1.0f,-1.0f,-1.0f),
          new Vertex(   1.0f,-1.0f,-1.0f),
          new Vertex(   1.0f, 1.0f,-1.0f),
          new Vertex(   1.0f,-1.0f,-1.0f),
          new Vertex(   -1.0f,-1.0f,-1.0f),
           new Vertex(  -1.0f,-1.0f,-1.0f),
           new Vertex(  -1.0f, 1.0f, 1.0f),
          new Vertex(   -1.0f, 1.0f,-1.0f),
           new Vertex(  1.0f,-1.0f, 1.0f),
           new Vertex(  -1.0f,-1.0f, 1.0f),
           new Vertex(  -1.0f,-1.0f,-1.0f),
           new Vertex(  -1.0f, 1.0f, 1.0f),
          new Vertex(   -1.0f,-1.0f, 1.0f),
         new Vertex(    1.0f,-1.0f, 1.0f),
         new Vertex(    1.0f, 1.0f, 1.0f),
         new Vertex(    1.0f,-1.0f,-1.0f),
         new Vertex(    1.0f, 1.0f,-1.0f),
          new Vertex(   1.0f,-1.0f,-1.0f),
         new Vertex(    1.0f, 1.0f, 1.0f),
          new Vertex(   1.0f,-1.0f, 1.0f),
          new Vertex(   1.0f, 1.0f, 1.0f),
           new Vertex(  1.0f, 1.0f,-1.0f),
        new Vertex(     -1.0f, 1.0f,-1.0f),
          new Vertex(   1.0f, 1.0f, 1.0f),
           new Vertex(  -1.0f, 1.0f,-1.0f),
         new Vertex(    -1.0f, 1.0f, 1.0f),
          new Vertex(   1.0f, 1.0f, 1.0f),
         new Vertex(    -1.0f, 1.0f, 1.0f),
         new Vertex(    1.0f,-1.0f, 1.0f)


        };
        static GameWindow wnd;
        static void Main(string[] args)
        {


            wnd = new GameWindow();
            wnd.RenderFrame += Wnd_RenderFrame;
            wnd.Load += Wnd_Load;
       

            wnd.Run();

        }

        private static void Wnd_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Окно загружено!");
            GL.Viewport(0, 0, wnd.Width, wnd.Height);

            GL.Enable(EnableCap.DepthTest);
       
  
            EG.Init();


            vr = new VertexVert();
            vr.Vertexes = new List<Vertex>(arrray);
            vr.Save();

         
            model = new XMLModel();
            model.LoadFromFile("./model.xml", null);
            model.Create();

            ShadowShader = ShaderProgram.FromSrcDir("shadowshader");
            int[] textures = new int[56];
            GL.GenTextures(56, textures);



            ShadowFraemBuffer = new FraemBuffer();


        }

        static XMLModel model;

        static VertexVert vr;


        static ShaderProgram ShadowShader;
        static FraemBuffer ShadowFraemBuffer;








        private static void Wnd_RenderFrame(object sender, FrameEventArgs e)
        {
            val += 0.04f;
            EG.LightEnable = true;

            {
                ShadowFraemBuffer.Use();
                //GL.DrawBuffer(DrawBufferMode.None);

                GL.Viewport(0, 0, 4098, 4098);
                GL.Clear(ClearBufferMask.DepthBufferBit);

                EG.MatrixMode(MatrixType.Projection);
                EG.LoadIndenty();
                EG.MultMatrix(Matrix4.CreateOrthographicOffCenter(-10, 10, -10, 10, -10, 20));


                EG.MatrixMode(MatrixType.View);
                EG.LoadIndenty();
                EG.MultMatrix(Matrix4.LookAt(new Vector3(1f, 19f, 1f), new Vector3(0f), Vector3.UnitY));

                EG.MatrixMode(MatrixType.Model);
                EG.LoadIndenty();
                

                Matrix4 bias_new = new Matrix4(
                    new Vector4(0.5f, 0f, 0f, 0f),
                    new Vector4(0f, 0.5f, 0f, 0f),
                    new Vector4(0f, 0f, 0.5f, 0f),
                    new Vector4(0.5f, 0.5f, 0.5f, 1f));

                EG.Bias = (EG.Model * (EG.View * EG.Projection)) * bias_new;


              
                EG.NotDefultProgram = ShadowShader;
                EG.NotDefultUpdateStates = () => 
                {
                    
                    Matrix4 onResult = (EG.Model * (EG.View * EG.Projection));
                    //  Matrix4 onResult = EG.Model * (EG.View * EG.Projection);
                    GL.UniformMatrix4( ShadowShader.GetUniformLocation("depthMVP"),false,ref onResult);
                };
                RenderWorld();


            }
            
        




            {

                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
                GL.Viewport(0, 0, wnd.Width, wnd.Height);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);



                EG.NotDefultProgram = null;
              

                GL.ActiveTexture(TextureUnit.Texture5);
                GL.BindTexture(TextureTarget.Texture2D,ShadowFraemBuffer.DepthTextureObject);
                EG.ShadowMap = 5;
                

              
                EG.MatrixMode(MatrixType.Projection);
                EG.LoadIndenty();
                EG.MultMatrix(Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)wnd.Width / (float)wnd.Height * (float)1.0f, 0.1f, 1000f));


                EG.MatrixMode(MatrixType.View);
                EG.LoadIndenty();
                EG.MultMatrix(Matrix4.LookAt(-5f, 5f, -5f, 0f, 0f, 0f, 0f, 1f, 0f));

                EG.MatrixMode(MatrixType.Model);
                EG.LoadIndenty();


                RenderWorld();

                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
       


            ErrorCode code = GL.GetError();
            if (code != ErrorCode.NoError)
            {
                Console.WriteLine(code);
                
            }

            wnd.SwapBuffers();

   
        }

        static float val = 0.1f;
        static void RenderWorld()
        {



            EG.LightEnable = false;



            EG.DrawLine(new Vector3(0f), new Vector3(1f, 0f, 0f), Color4.Red);
            EG.DrawLine(new Vector3(0f), new Vector3(0f, 1f, 0f), Color4.Green);
            EG.DrawLine(new Vector3(0f), new Vector3(0f, 0f, 1f), Color4.Blue);



            EG.LightEnable = true;



            EG.PushMatrix();
            {


                EG.Translate(0f, -2f, 0f);
                EG.Scale(20.0f, 1.0f, 20.0f);
                vr.Draw();

            }
            EG.PopMatrix();



            EG.PushMatrix();
            {
                EG.Translate(5f, 0f, 5f);

                EG.Rotate(val, 0f, 1f, 0f);
             
                vr.Draw();

            }
            EG.PopMatrix();


            EG.PushMatrix();
            {

               // EG.Translate(-5f, 0f, -5f);
                EG.Rotate(val, 0f, 1f, 0f);
                EG.Rotate((float)Math.PI / 2, -1f, 0f, 0f);
          
                EG.Scale(0.3f, 0.3f, 0.3f);

                model.Draw();

            }
            EG.PopMatrix();


        }




    }
}
