using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics;
using OpenTK;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using EmyEngine.Game;
using EmyEngine.GUI;
using EmyEngine.OpenGL;
using EmyEngine.ResourceManagment;
using Jitter.Collision.Shapes;
using Jitter.Dynamics;
using Jitter.Dynamics.Constraints;
using Jitter.Dynamics.Joints;
using Jitter.LinearMath;
using OpenTK.Input;
using SdkGame;
using OpenTK.Graphics.OpenGL4;

namespace EmyEngine
{
    public class GameClass
    {



        static void Main(string[] args)
        {



            GameWindow test = new GameWindow();
            GameApplication bleat = null;


            GameObjectJShape s = null;
            GameObjectJShape s1 = null;
          
            test.Load += (sender, eventArgs) =>
            {
                bleat = new GameApplication();
                bleat.Initialize();
               
           
                s = new GameObjectJShape(new BoxShape(new JVector(1f, 1f, 1f)));
                s.Position = new JVector(4f, 4f, 0f);
                s1 = new GameObjectJShape(new BoxShape(new JVector(1f, 1f, 1f)));
                s1.Position = new JVector(4f, 4f, 2f);

                PrismaticJoint e = new PrismaticJoint(bleat.InstanceFromGame.World,s.Body,s1.Body,0f, 0f);
               
                
                e.Activate();
                
            
                bleat.InstanceFromGame.AddObject(s);
                bleat.InstanceFromGame.AddObject(s1);
                bleat.InstanceFromGame.AddObject(new PlatformObject());


               

            };
            test.RenderFrame += (sender, eventArgs) =>
            {
                
               
                bleat.Render(test.Width, test.Height, test.Mouse.X, test.Mouse.Y, test.Mouse[MouseButton.Left]);
              
            };
            test.UpdateFrame += (sender, eventArgs) =>
            {
                bleat.Update(100);
            };
         

            SoftBody body = new SoftBody();
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    Console.ReadKey();
                    Console.WriteLine("adders");

                    string cmd = Console.ReadLine();
                    if (cmd == "newb")
                    {




                    }

                }

            });
            t.IsBackground = true;
            t.Start();

            test.Run(100, 100);


            


            // Window = new GameWindow();
            // Window.RenderFrame += WindowRenderFrame;
            // Window.UpdateFrame += WindowUpdateFrame;
            // Window.Load += WindowLoad;
            //// wnd.WindowState = WindowState.Maximized;
            // Window.KeyDown += WindowKeyDown; ;
            // Window.Run(updates_in_one_second, updates_in_one_second);
        }
    }

    //    const int updates_in_one_second = 100;

    //    public static  Random rnd = new Random();
    //    private static void WindowKeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
    //    {
    //        if (e.Key == OpenTK.Input.Key.Escape)
    //        {
    //            Window.Close();
    //        }

    //        if (e.Key == OpenTK.Input.Key.Number1)
    //        {
    //            CarObject vs = new CarObject();
    //            vs.Position = new JVector(0f, 4f, 0f);
    //            GameClass.Inst.AddObject(vs);
    //        }
      

    //    }

    //    public static ClickHandler<GameObject> OnWindowClick; 

    //    public static GameInstance Inst;
    //    public static GameWindow Window;
    //    public static MainForm GameWindow;
    //    public static GameUI BaseUI;
    //    public static DebugDrawer ddrawer;
    //    public static Font arial50;
    //    public static FlyingCamera fly;
    //    public static ShaderNew BasicShader;
    //    public static ZShader SubShader;
    //    public static GUIShader GuiShader;
    //    public static FraemBuffer BasicBufer;

    //    private static void WindowLoad(object sender, EventArgs e)
    //    {
    //        Window.Title = "EmyEngine build *[0.0.4.262123]";
    //        EE.Log("Окно загружено!");
    //        GL.Viewport(0, 0, Window.Width, Window.Height);
    //        GL.ClearColor(1f / 255 * 0xFF, 1f / 255 * 0xFF, 1f / 255 * 0xFF, 1f);

    //        GL.Enable(EnableCap.DepthTest);
    //        GL.DepthFunc(DepthFunction.Lequal);

    //        GL.Enable(EnableCap.LineSmooth);
    //        GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);


    //        GL.Enable(EnableCap.Blend);
    //        GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

    //        GL.Enable(EnableCap.CullFace);
    //        GL.Enable(EnableCap.ClipPlane0);

    //        //Загружаем все ресурсы!
    //        EE.СurrentResources = new ResourcesDir();
    //        ((ResourcesDir) EE.СurrentResources).Open("./bin");
    //        //Создаём ширифт arial50 из пакета текстур в дириктории /Arial_50
    //        arial50 = new Font(EE.СurrentResources, "/Arial_50");
    //        //сразу выстовляем его дефолтным
    //        EE.СurrentFont = arial50;



    //        //Создаём три сдандартных шейдера
    //        GuiShader = new GUIShader();
    //        BasicShader = new ShaderNew();
    //        SubShader = new ZShader();

    //        //Создаём Теневой Кадровый Буфер
    //        BasicBufer = new FraemBuffer();
    //        //Говорим несущему шейдеру где находится текстура с тенями
    //        BasicShader.ShadowMapObject = BasicBufer.DepthTextureObject;

    //        //Инициализируем Медленный рендер, без него никак!
    //        G.Graphics = new SlowRender();
    //        //Инициализируем Debug Рендер, он базируется на  SlowRender'e
    //        ddrawer = new DebugDrawer();

          
         
       
    //        //Когда инициализация ресурсов закончена, создаём игровую установку
    //        Inst = new GameInstance();   
    //        //Так-же создаём установку gui
    //        BaseUI = new GameUI();

    //        //Клик хУндлЯр фром работы клика! XDXDXD
    //        OnWindowClick = new ClickHandler<GameObject>();
    //        OnWindowClick.Press += OnWindowClickOnPress;

    //        //Ну и всё! игровой движок создан. 


    //        fly = new FlyingCamera();
    //        fly.HeadPosition = new Vector3(10f, 4f, 10f);

    //        GameWindow = new MainForm();
    //        BaseUI.Items.Add(GameWindow);

    //        {
    //            PlatformObject bbox = new PlatformObject();
    //            bbox.Position = new JVector(0f, 0f, 0f);
          
    //            Inst.AddObject(bbox);
    //        }

    //        {
    //            MiniPlatformObject bbox = new MiniPlatformObject();
    //            bbox.Orientation = JMatrix.CreateRotationX((float)Math.PI / 6);
    //            bbox.Position = new JVector(-5f, 0.6f, -4);
    //            Inst.AddObject(bbox);
    //        }



    //    }


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

     


    //    private static void WindowRenderFrame(object sender, FrameEventArgs e)
    //    {

    //        MouseState ts = Mouse.GetState();
    //        BaseUI.Process(new Point(0, 0), Window.Width, Window.Height, ts.X, ts.Y, ts.IsButtonDown(MouseButton.Left));
    //        BaseUI.ReversUpdate();
    //        if (ts.IsButtonDown(MouseButton.Right))
    //            fly.UpdateState(Window,true);
    //        {
    //            GL.CullFace(CullFaceMode.Front);
                
    //            G.Instance = SubShader;

    //            BasicBufer.Use();
    //            //GL.DrawBuffer(DrawBufferMode.None);

    //            GL.Viewport(0, 0, 8192, 8192);
    //            GL.Clear(ClearBufferMask.DepthBufferBit);


    //            G.MatrixMode(MatrixType.Projection);
    //            G.LoadIndenty();
    //            //G.MultMatrix(Matrix4.CreateOrthographicOffCenter(-20, 20, -20, 20, 0, 255));

    //            G.MultMatrix(Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 2, 1.0f, 2.0f, 500.0f));
    //            //G.MultMatrix(Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)8192 / (float)8192 * (float)1.0f, 0.1f, 1000f));


    //            G.MatrixMode(MatrixType.View);
    //            G.LoadIndenty();
    //            //G.MultMatrix(Matrix4.LookAt(P1, P2, Vector3.UnitY));
    //            G.MultMatrix(Matrix4.LookAt(new Vector3(25,10,0), new Vector3(0,0,0), Vector3.UnitY));

    //            G.MatrixMode(MatrixType.Model);
    //            G.LoadIndenty();


    //            Matrix4 bias_new = new Matrix4(
    //                new Vector4(0.5f, 0f, 0f, 0f),
    //                new Vector4(0f, 0.5f, 0f, 0f),
    //                new Vector4(0f, 0f, 0.5f, 0f),
    //                new Vector4(0.5f, 0.5f, 0.5f, 1f));

    //            G.Bias = G.GetMatrixRezult() * bias_new;

    //            RenderWorld();

    //            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    //        }
    //        {
    //            GL.CullFace(CullFaceMode.Back);
    //            G.Instance = BasicShader;

    //            GL.Viewport(0, 0, Window.Width, Window.Height);
    //            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


    //            G.MatrixMode(MatrixType.Projection);
    //            G.LoadIndenty();
    //            G.MultMatrix(Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)Window.Width / (float)Window.Height * (float)1.0f, 0.1f, 1000f));


    //            G.MatrixMode(MatrixType.View);
    //            G.LoadIndenty();
    //            G.MultMatrix(Matrix4.LookAt(10f, 4f, 10f, 0f, 0f, 0f, 0f, 1f, 0f));
    //            //fly.MultMatrix();
    //            G.MatrixMode(MatrixType.Model);
    //            G.LoadIndenty();


    //            OnWindowClick.ButtonState(ts.IsButtonDown(MouseButton.Left));
             
    //            RenderWorld();
    //            for (int i = 0; i < Inst.Length; i++)
    //            {
    //                Inst[i].Body.DebugDraw(ddrawer);
    //            }
    //        }
    //        {
    //            GL.Disable(EnableCap.LineSmooth);
    //            G.Instance = GuiShader;
    //            G.MatrixMode(MatrixType.Projection);
    //            G.LoadIndenty();
    //            G.MultMatrix(Matrix4.CreateOrthographicOffCenter(0.0f, Window.Width, Window.Height, 0.0f, 2f, -2.0f));
    //            G.MatrixMode(MatrixType.View);
    //            G.LoadIndenty();
    //            G.MatrixMode(MatrixType.Model);
    //            G.LoadIndenty();
    //            G.Clip = G.Clip2D.Indenty(Window.Width, Window.Height);
    //            BaseUI.Draw();
    //            GL.Enable(EnableCap.LineSmooth);
    //        }
          
    //        Window.SwapBuffers();
    //    }

    //    private static void WindowUpdateFrame(object sender, FrameEventArgs e)
    //    {        
    //        Inst.Update(1f / updates_in_one_second);       
    //    }
    //    static void RenderWorld()
    //    {
    //        for (int i = 0; i < Inst.Length; i++)
    //        {                
    //            Inst[i].Draw();               
    //        }
    //    }

    //}
}