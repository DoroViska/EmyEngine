using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
using System.IO;

using EmyEngine.Game;
using EmyEngine.OpenGL;
using EmyEngine.ResourceManagment;
using Jitter.Collision.Shapes;
using Jitter.LinearMath;


namespace EmyEngine.CompoundEditor
{
    public partial class MainForm : Form
    {
        ShaderNew BasicShader;
        ZShader SubShader;
        FraemBuffer BasicBufer;
        private  GameInstance inst;
        public MainForm()
        {
            InitializeComponent();

     

        }

        private CompoundShape.TransformedShape lower;
        private CompoundShape.TransformedShape upper;
        private CompoundShape chassis;
        private GameObjectJShape bbox;
        private void glControl1_Load(object sender, EventArgs e)
        {

            EE.СurrentResources = new ResourcesDir();
            //C:\Users\super_power\Google Диск\EmyEngine\EmyEngine.SdkGame\bin\Debug\bin
            GL.ClearColor(1f / 255 * 0xFF, 1f / 255 * 0xFF, 1f / 255 * 0xFF, 1f);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);

            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);


            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Enable(EnableCap.CullFace);


            EE.СurrentResources = new ResourcesDir();
            ((ResourcesDir)EE.СurrentResources).Open("C:/Users/super_power/Google Диск/EmyEngine/EmyEngine.SdkGame/bin/Debug/bin");


            BasicBufer = new FraemBuffer();
            BasicShader = new ShaderNew();
            BasicShader.ShadowMapObject = BasicBufer.DepthTextureObject;
            SubShader = new ZShader();

            {
                inst = new GameInstance();
                GameObjectJShape bbox = new GameObjectJShape(new BoxShape(300, 1, 300));
                bbox.Position = new JVector(0f, 0f, 0f);
                bbox.Body.IsStatic = true;
                inst.AddObject(bbox);
            }  
            {



               

                Update_t();

            }

            Renderer.Enabled = true;
        }

        private void Renderer_Tick(object sender, EventArgs e)
        {
            inst.Update(0.01f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.CullFace(CullFaceMode.Back);
            G.Instance = BasicShader;

            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            G.MatrixMode(MatrixType.Projection);
            G.LoadIndenty();
            G.MultMatrix(Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)glControl1.Width / (float)glControl1.Height * (float)1.0f, 0.1f, 1000f));


            G.MatrixMode(MatrixType.View);
            G.LoadIndenty();
            G.MultMatrix(Matrix4.LookAt(10f, 10f, 10f, 0f, 0f, 0f, 0f, 1f, 0f));

     

            G.MatrixMode(MatrixType.Model);
            G.LoadIndenty();

            for (int i = 0; i < inst.Length; i++)
            {

                inst[i].Draw();
            }
            G.PushMatrix();

            G.Translate(position.X, position.Y, position.Z);
            G.Translate(car.X, car.Y, car.Z);
            G.Scale(0.383f, 0.383f, 0.383f);
            G.Rotate((float)Math.PI,0f,1f,0f);
            EE.СurrentResources.GetResource<IDraweble>("models/PickUpBody.obj").Draw();
            G.PopMatrix();

            glControl1.SwapBuffers();
        }






        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                CarObject vs = new CarObject();
                vs.Position = new JVector(0f, 4f, 0f);

                inst.AddObject(vs);
            }

            catch (Exception ex)
            {
                EE.Log(ex.ToString());
            }
        }

        public void Update_t()
        {
           if(bbox != null)
                inst.RemoveObject(bbox);


            lower = new CompoundShape.TransformedShape(
                    new BoxShape(1.430f, 0.6f, 4.040f), JMatrix.Identity, JVector.Zero);

            upper = new CompoundShape.TransformedShape(
                new BoxShape(1.430f, 0.5f, 1.040f), JMatrix.Identity, addinal);


            CompoundShape.TransformedShape[] subShapes = { lower, upper };

            chassis = new CompoundShape(subShapes);


            bbox = new GameObjectJShape(chassis);
            bbox.Position = position;
            bbox.Body.IsStatic = true;
            inst.AddObject(bbox);

        }

        private JVector car = new JVector(0f, 0f, 0f);
        JVector addinal = new JVector();
        JVector position = new JVector(0f, 5f, 0f);
        private void button2_Click(object sender, EventArgs e)
        {
            addinal = addinal + new JVector(0f,0.1f,0f);
            Update_t();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            addinal = addinal - new JVector(0f, 0.1f, 0f);
            Update_t();
        }


        private void button8_Click(object sender, EventArgs e)
        {
            addinal = addinal + new JVector(0f, 0f, 0.1f);
            Update_t();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            addinal = addinal - new JVector(0f, 0f, 0.1f);
            Update_t();
        }





        private void button4_Click(object sender, EventArgs e)
        {
            position += new JVector(0.5f,0f,0f);
            bbox.Position = position;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            position -= new JVector(0.5f, 0f, 0f);
            bbox.Position = position;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            position += new JVector(0f, 0f, 0.5f);
            bbox.Position = position;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            position -= new JVector(0f, 0f, 0.5f);
            bbox.Position = position;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            car += new JVector(0f, 0.1f, 0f);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            car -= new JVector(0f, 0.1f, 0f);
        }

        private float scale = 1f;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            scale = ((float)trackBar1.Value) / 1000f;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            throw new Exception();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            car += new JVector(0f, 0f, 0.1f);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            car -= new JVector(0f, 0f, 0.1f);
        }
    }
}
