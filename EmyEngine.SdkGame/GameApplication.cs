using System;
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


namespace SdkGame
{
    public class GameApplication
    {
        private GameInstance _instanceFromGame;
        private GameUI _instanceFromGui;
        private FraemBuffer _fraemBufferMain;
        private IShaderInstance _shaderGui;
        private IShaderInstance _shaderZMain;
        private IShaderInstance _shader3DMain;
        private DebugDrawer _slowDebugDrawer;
        private MainForm _forma;
        private List<IDraweble> _decorations; 

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
            set { _instanceFromGame = value; }
            get { return _instanceFromGame; }
        }

        public GameUI InstanceFromGui
        {
            set { _instanceFromGui = value; }
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

        public List<IDraweble> Decorations
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




            EE.СurrentResources = reses;
            EE.СurrentGuiResources = reses;
            EE.СurrentFont = new Font(reses, "/Arial_50");
            EE.CurentTransleter = new TaskTransleter();
            _fraemBufferMain = new FraemBuffer();
            _decorations = new List<IDraweble>();
            _shaderGui = new GUIShader();
            _shaderZMain = new ZShader();
            _shader3DMain = new ShaderNew();
            ((ShaderNew) _shader3DMain).ShadowMapObject = _fraemBufferMain.DepthTextureObject;

            _instanceFromGame = new GameInstance();
            _instanceFromGui = new GameUI();

            _forma = new MainForm(this);
            _instanceFromGui.Items.Add(_forma);


        }


       








        public void Render(int widith, int height,int mX, int mY, bool mL)
        {

            _instanceFromGui.Process(new Point(0, 0), widith, height, mX, mY, mL);
            _instanceFromGui.ReversUpdate();

            //if (ts.IsButtonDown(MouseButton.Right))
            //    fly.UpdateState(Window, true);
            {
             
                GL.CullFace(CullFaceMode.Front);
                G.Instance = _shaderZMain;
                _fraemBufferMain.Use();

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


                

                GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            }
            {
                GL.CullFace(CullFaceMode.Back);
                G.Instance = _shader3DMain;

                GL.Viewport(0, 0, widith , height);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


                G.MatrixMode(MatrixType.Projection);
                G.LoadIndenty();
                G.MultMatrix(Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)widith / (float)height * (float)1.0f, 0.1f, 1000f));


                G.MatrixMode(MatrixType.View);
                G.LoadIndenty();
                G.MultMatrix(Matrix4.LookAt(10f, 5f, 10f, 0f, 0f, 0f, 0f, 1f, 0f));
                //fly.MultMatrix();
                G.MatrixMode(MatrixType.Model);
                G.LoadIndenty();


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
            }
            {
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


        //    public static GameObject Selection = null;

        //    private static void OnWindowClickOnPress(GameObject gameObject, bool b)
        //    {
        //        if(!b) return;

        //        MouseState m_b = Mouse.GetState();
        //        Vector3 rayS = new Vector3();
        //        Vector3 rayE = new Vector3();
        //        Projection.PushRay(m_b.X, m_b.Y, Window.Width, Window.Height, ref rayS, ref rayE);


        //        float flst = 0f;
        //        JVector normal;
        //        RigidBody body;
        //        Inst.World.CollisionSystem.Raycast(EE.Vector(rayS), EE.Vector(rayE), (aa, ba, ca) => { return !aa.IsStatic; }, out body, out normal, out flst);

        //        if (body != null)
        //            Selection = (((ObjectivBody)body).BodyGameObject);


        //    }







    }
}
