using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibPtr;

namespace EmyEngine.OpenGL
{
    public sealed class Shader : AutoRealaseSafeUnmanagmentClass
    {
        
        public int Handle { get; private set; }
        public ShaderType Type { get; private set; }

        public Shader(ShaderType type)
        {
            Type = type;
            Acquire();
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
                EE.Log(GL.GetShaderInfoLog(Handle));
        }

 
        public override DisposeInformation Realase()
        {
            if (GraphicsContext.CurrentContext == null || GraphicsContext.CurrentContext.IsDisposed)
                return DisposeInformation.Error;
            if (Handle == 0)
                return DisposeInformation.Error;

            GL.DeleteShader(Handle);
      
            Handle = 0;
            return DisposeInformation.Sucsses;
        }

        public override void Acquire()
        {
            IsDisposed = false;
            Handle = GL.CreateShader(Type);
        }
    }
}
