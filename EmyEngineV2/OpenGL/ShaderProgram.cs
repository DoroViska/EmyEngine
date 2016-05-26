using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmyEngine.ResourceManagment;
using System.Threading;
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

        public void DetachShader(Shader shader)
        {
            GL.DetachShader(Handle, shader.Handle);
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




        private static long FuckFileHash(FileInfo f)
        {
            long rez = 0;
   
            using (FileStream s = f.OpenRead())
            {
                int last = 0;
                for (long i = 0; i < s.Length; i++)
                {                
                    int c = s.ReadByte();
                    if (c > last)                    
                        rez += c;
                    else
                        rez += c + 1;
                    last = c;
                }
            }
            return rez;
        }



        public static ShaderProgram FromSrcDir(string name)
        {
            ShaderProgram shader = new ShaderProgram();

            Shader vs = new Shader(ShaderType.VertexShader);
            vs.Compile(File.ReadAllText(name + "/shader.vs"));
            FileInfo _vsf = new FileInfo(name + "/shader.vs");

            Shader fs = new Shader(ShaderType.FragmentShader);
            fs.Compile(File.ReadAllText(name + "/shader.fs"));
            FileInfo _fsf = new FileInfo(name + "/shader.fs");
            
            shader.AttachShader(vs);
            shader.AttachShader(fs);
            shader.Link();
        
            bool wait = false;
            object loop_lock = new object();
            Thread th = new Thread(()=> 
            {
                ShaderProgram _shader = shader;
                Shader _vs = vs;
                Shader _fs = fs;
                FileInfo vsf = _vsf;
                FileInfo fsf = _fsf;
                long old_ls = FuckFileHash(vsf);
                long old_lf = FuckFileHash(fsf);

                lock (loop_lock) wait = true;


                while (true)
                {
                    Thread.Sleep(500);
                    vsf.Refresh();
                    fsf.Refresh();
                    long old_ls_n = FuckFileHash(vsf);
                    long old_lf_n = FuckFileHash(fsf);
                    bool opt = false;
                    EE.CurentTransleter.PushTask( (a)=> 
                    {
                        if (old_ls_n != old_ls)
                        {
                            _shader.DetachShader(_vs);
                            _vs = new Shader(ShaderType.VertexShader);
                            _vs.Compile(File.ReadAllText(vsf.FullName));
                            _shader.AttachShader(_vs);
                            opt = true;
                        }
                        if (old_lf_n != old_lf)
                        {
                            _shader.DetachShader(_fs);
                            _fs = new Shader(ShaderType.FragmentShader);
                            _fs.Compile(File.ReadAllText(fsf.FullName));
                            _shader.AttachShader(_fs);
                            opt = true;
                        }
                        if (opt)
                        {
                            _shader.Link();
                        }
                    }, null);

                    EE.CurentTransleter.Wait();
                    old_ls = old_ls_n;
                    old_lf = old_lf_n;
                 
                }
              
            });
            th.IsBackground = true;
            th.Start();

            while (true) lock (loop_lock) if (wait) break;
            

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
