using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmyEngine.ResourceManagment;

namespace EmyEngine.OpenGL
{
    public sealed class ShaderProgram : IDisposable
    {
        private const int InvalidHandle = -1;

        public int Handle { get; private set; }


        public int GetAttribLocation(string name)
        {
            return GL.GetAttribLocation(this.Handle,name);
        }

        public int GetUniformLocation(string name)
        {           
            return GL.GetUniformLocation(this.Handle, name);
        }





        public ShaderProgram()
        {
            AcquireHandle();
        }

        private void AcquireHandle()
        {
            Handle = GL.CreateProgram();
        }

        public void AttachShader(Shader shader)
        {
            GL.AttachShader(Handle, shader.Handle);
        }

        public void Link()
        {
            GL.LinkProgram(Handle);

            int linkStatus;
            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out linkStatus);

            if (linkStatus == 0)
                Console.WriteLine(GL.GetProgramInfoLog(Handle));
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        private void ReleaseHandle()
        {
            if (Handle == InvalidHandle)
                return;

            GL.DeleteProgram(Handle);

            Handle = InvalidHandle;
        }

        public void Dispose()
        {
            ReleaseHandle();
            GC.SuppressFinalize(this);
        }

        ~ShaderProgram()
        {
            if (GraphicsContext.CurrentContext != null && !GraphicsContext.CurrentContext.IsDisposed)
                ReleaseHandle();
        }


        public static ShaderProgram FromSrcDir(string name)
        {
            ShaderProgram shader = new ShaderProgram();
            Shader vs = new Shader(ShaderType.VertexShader);
            vs.Compile(File.ReadAllText(name + "/shader.vs"));
            Shader fs = new Shader(ShaderType.FragmentShader);
            fs.Compile(File.ReadAllText(name + "/shader.fs"));
            shader.AttachShader(vs);
            shader.AttachShader(fs);
            shader.Link();
            return shader;
        }
        public static ShaderProgram FromResources(Resources t,string dir)
        {
            
            ShaderProgram shader = new ShaderProgram();
            Shader vs = new Shader(ShaderType.VertexShader);
            vs.Compile(Encoding.ASCII.GetString(t.GetResource(dir + "/shader.vs").Data));
            Shader fs = new Shader(ShaderType.FragmentShader);
            fs.Compile(Encoding.ASCII.GetString(t.GetResource(dir + "/shader.fs").Data));
            shader.AttachShader(vs);
            shader.AttachShader(fs);
            shader.Link();
            return shader;
        }


    }
}
