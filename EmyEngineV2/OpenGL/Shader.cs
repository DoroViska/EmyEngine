using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.OpenGL
{
    public sealed class Shader
    {
        
        public int Handle { get; private set; }
        public ShaderType Type { get; private set; }

        public Shader(ShaderType type)
        {
            Type = type;
            AcquireHandle();
        }

        private void AcquireHandle()
        {
            Handle = GL.CreateShader(Type);
        }

        public void Compile(string source)
        {
            EE.Log("Shader Compiled {0}", Type);
            GL.ShaderSource(Handle, source);
            GL.CompileShader(Handle);

            int compileStatus;
            GL.GetShader(Handle, ShaderParameter.CompileStatus, out compileStatus);

            // Если произошла ошибка, выведем сообщение
            if (compileStatus == 0)
                Console.WriteLine(GL.GetShaderInfoLog(Handle));
        }

        private void ReleaseHandle()
        {
            if (Handle == 0)
                return;

            GL.DeleteShader(Handle);

            Handle = 0;
        }

        ~Shader()
        {
            if (GraphicsContext.CurrentContext != null && !GraphicsContext.CurrentContext.IsDisposed)
                ReleaseHandle();
        }
    }
}
