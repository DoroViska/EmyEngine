﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EmyEngine;
using EmyEngine.Game;
using EmyEngine.GUI;
using EmyEngine.OpenGL;
using EmyEngine.ResourceManagment;

using OpenTK.Input;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Jitter.Dynamics;
using Jitter.LinearMath;

namespace SdkGame
{
    public class GameApplication
    {
        private GameInstance _instanceFromGame;
        private MainForm _instanceFromGui;
        private FraemBuffer _fraemBufferMain;
        private IShaderInstance _shaderGui;
        private IShaderInstance _shaderZMain;
        private IShaderInstance _shader3DMain;
        private DebugDrawer _slowDebugDrawer;
        private MainForm _forma;
        private List<IDrawable> _decorations; 

        // shaders/fbuffers
        public IShaderInstance Shader3DMain
        {
            set { _shader3DMain = value; }
            get { return _shader3DMain; }
        }

        public IShaderInstance ShaderZMain
        {
            set { _shaderZMain = value; }
            get { return _shaderZMain; }
        }

        public IShaderInstance ShaderGui
        {
            set { _shaderGui = value; }
            get { return _shaderGui; }
        }

        public IShaderInstance ShaderSub { set; get; }

        public FraemBuffer FraemBufferMain
        {
            set { _fraemBufferMain = value; }
            get { return _fraemBufferMain; }
        }


        // instances
        public GameInstance InstanceFromGame
        {     
            get { return _instanceFromGame; }
        }

        public GameUI InstanceFromGui
        {
            get { return _instanceFromGui; }
        }


        //tools
        public DebugDrawer SlowDebugDrawer
        {
            set { _slowDebugDrawer = value; }
            get { return _slowDebugDrawer; }
        }

        public MainForm Forma
        {
            set { _forma = value; }
            get { return _forma; }
        }

        public List<IDrawable> Decorations
        {
            get {  return _decorations; }
            set {  _decorations = value; }
        }

        public void Initialize()
        {
            ResourcesArc reses = new ResourcesArc();              
            foreach (FileInfo pnp in new DirectoryInfo("./").GetFiles())
            {
                if((pnp.Extension.ToLower() == ".eedata") || (pnp.Extension.ToLower() == ".zip"))
                    reses.Open(pnp.FullName);
            }

           
            G.Graphics = new SlowRender();
            _slowDebugDrawer = new DebugDrawer();
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);


            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.ClipPlane0);

            GL.ClearColor(1f / 255f * 55, 1f / 255f * 125, 1f / 255f * 185, 1f);


            EE.СurrentResources = reses;
            EE.СurrentGuiResources = reses;
            EE.СurrentFont = new Font(reses, "/Arial_50");
            EE.CurentTransleter = new TaskTransleter();
            _fraemBufferMain = new FraemBuffer();
            _rezultBuffer = new FraemBuffer(FramebufferAttachment.ColorAttachment0, 1000, 800);
            _decorations = new List<IDrawable>();
            _shaderGui = new GUIShader();
            _shaderZMain = new ZShader();
            _shader3DMain = new ShaderNew();

            ((ShaderNew) _shader3DMain).ShadowMapObject = _fraemBufferMain.DepthTextureObject;

            _instanceFromGame = new GameInstance();
            _instanceFromGui = new MainForm(this);

   

            //Lamps
            LightSource s = new LightSource();
            s.Position = new Vector3(100f, 100f, 100f);
            s.Attenuation = new Vector3(0.2f, 0.2f, 0.2f);
            s.Colour = new Vector3(1f,1f,1f);
            s.Exponent = 0.000f;
            s.InnerCutoff = 100f;
            s.OuterCutoff = 100f;
            s.Direction = new Vector3(0.0f, -1.0f, 0.0f);
            s.Type = 1;
            ((ShaderNew)_shader3DMain).AddLamp(s);



            //_glowBloom = new GlowBloomShader();
           



        }
        FraemBuffer _rezultBuffer;
        GlowBloomShader _glowBloom;
        public class GlowBloomShader : IShaderInstance
        {
            public ShaderProgram Program {  set;    get; }

            public void UpdateState(EmyEngine.OpenGL.Material Material)
            {
                   
            }
        }



        public FlyingCamera Camera { set; get; } = new FlyingCamera();


        public void Render(int widith, int height,int mX, int mY, bool mL)
        {

            //if (ts.IsButtonDown(MouseButton.Right))
            //    fly.UpdateState(Window, true);
            Camera.UpdateState(GameClass.TestWindow, GameClass.UseEscape);


            using (_fraemBufferMain.Bind())
            {
                GL.CullFace(CullFaceMode.Front);
                G.Instance = _shaderZMain;
                GL.Viewport(0, 0, 8192, 8192);
                GL.Clear(ClearBufferMask.DepthBufferBit);


                G.MatrixMode(MatrixType.Projection);
                G.LoadIndenty();
                //G.MultMatrix(Matrix4.CreateOrthographicOffCenter(-20, 20, -20, 20, 0, 255));

                G.MultMatrix(Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 2, 1.0f, 2.0f, 500.0f));
                //G.MultMatrix(Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)8192 / (float)8192 * (float)1.0f, 0.1f, 1000f));


                G.MatrixMode(MatrixType.View);
                G.LoadIndenty();
                //G.MultMatrix(Matrix4.LookAt(P1, P2, Vector3.UnitY));
                G.MultMatrix(Matrix4.LookAt(new Vector3(25, 10, 0), new Vector3(0, 0, 0), Vector3.UnitY));

                G.MatrixMode(MatrixType.Model);
                G.LoadIndenty();


                Matrix4 bias_new = new Matrix4(
                    new Vector4(0.5f, 0f, 0f, 0f),
                    new Vector4(0f, 0.5f, 0f, 0f),
                    new Vector4(0f, 0f, 0.5f, 0f),
                    new Vector4(0.5f, 0.5f, 0.5f, 1f));

                G.Bias = G.GetMatrixRezult() * bias_new;

                for (int i = 0; i < Decorations.Count; i++)
                {
                    Decorations[i].Draw();
                }
                for (int i = 0; i < _instanceFromGame.Length; i++)
                {
                    _instanceFromGame[i].Draw();
                }
                G.PushMatrix();
                G.Scale(1.1f, 1.1f, 1.1f);
               // EE.СurrentResources.GetResource<IDraweble>("/models/Skys.obj").Draw();
                G.PopMatrix();
              

            }


            // using (_rezultBuffer.Bind())
            //{
            GL.CullFace(CullFaceMode.Back);
            G.Instance = _shader3DMain;

            GL.Viewport(0, 0, widith, height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            G.MatrixMode(MatrixType.Projection);
            G.LoadIndenty();
            G.MultMatrix(Matrix4.CreatePerspectiveFieldOfView((float)Math.PI * 5 / 12, (float)widith / (float)height * (float)1.0f, 0.1f, 1000f));


            G.MatrixMode(MatrixType.View);
            G.LoadIndenty();
            //G.MultMatrix(Matrix4.LookAt(10f, 5f, 10f, 0f, 0f, 0f, 0f, 1f, 0f));
            Camera.MultMatrix();
            G.MatrixMode(MatrixType.Model);
            G.LoadIndenty();

            //G.PushMatrix();
            //G.Scale(0.1f, 0.1f, 0.1f);
            //G.Translate(0f, 4f, 0f);
            //EE.СurrentResources.GetResource<IDraweble>("/models/Skys.obj").Draw();
            //G.PopMatrix();

            


            //OnWindowClick.ButtonState(mL);
            for (int i = 0; i < Decorations.Count; i++)
            {
                Decorations[i].Draw();
            }
            for (int i = 0; i < _instanceFromGame.Length; i++)
            {
                _instanceFromGame[i].Draw();
                _instanceFromGame[i].Body.DebugDraw(_slowDebugDrawer);
            }

           


            EE.CurentTransleter.Process();

            //GUI TARGETING
            {          
                if ((!_instanceFromGui.Process(new Point(0, 0), widith, height, mX, mY, mL)) && mL)
                {
                    TryToSelectObject(widith, height, mX, mY);
                }

                _instanceFromGui.ReversUpdate();
         


                GL.Disable(EnableCap.LineSmooth);
                G.Instance = _shaderGui;
                G.MatrixMode(MatrixType.Projection);
                G.LoadIndenty();
                G.MultMatrix(Matrix4.CreateOrthographicOffCenter(0.0f, widith, height, 0.0f, 2f, -2.0f));
                G.MatrixMode(MatrixType.View);
                G.LoadIndenty();
                G.MatrixMode(MatrixType.Model);
                G.LoadIndenty();
                G.Clip = G.Clip2D.Indenty(widith, height);
                _instanceFromGui.Draw();
                GL.Enable(EnableCap.LineSmooth);


            }


            
         
            GraphicsContext.CurrentContext.SwapBuffers();
        }
        public void Update(float fps)
        {
     
            _instanceFromGame.Update(1f / fps);
        }


        public GameObject Selection = null;

        private void TryToSelectObject(int Window_Width, int Window_Height, int  m_b_X, int m_b_Y)
        {
            //Console.WriteLine("Try to selection Changed.");
            try
            {
                Vector3 rayS = new Vector3();
                Vector3 rayE = new Vector3();
                Projection.PushRay(m_b_X /*- (Window_Width / 2)*/, m_b_Y /*- (Window_Height / 2)*/, Window_Width, Window_Height, ref rayS, ref rayE);

                //Console.WriteLine("{0}, {1}",rayS, rayE);

                float flst = 0f;
                JVector normal;
                RigidBody body;
                this.InstanceFromGame.World.CollisionSystem.Raycast(EE.Vector(rayS), EE.Vector(rayE), (aa, ba, ca) => true/*!aa.IsStatic*/ , out body, out normal, out flst);
              
                if (body != null)
                {
                    Selection = (((ObjectivBody)body).BodyGameObject);
                    ((MainForm)this.InstanceFromGui).SelectionName.Text = Selection.Name;
            
                }
           
            } catch (Exception) { }             
        }







    }
}
