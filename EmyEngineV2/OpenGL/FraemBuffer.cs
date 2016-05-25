using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
namespace EmyEngine.OpenGL
{
    public class FraemBuffer
    {
        public int FraemBufferObject { private set; get; } = -1;
        public int DepthTextureObject { private set; get; } = -1;

        public FraemBuffer()
        {
           

            DepthTextureObject = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D,DepthTextureObject);
            GL.TexImage2D(TextureTarget.Texture2D,0,PixelInternalFormat.DepthComponent16, 8192, 8192, 0,PixelFormat.DepthComponent,PixelType.Float,(IntPtr)0);
            GL.TexParameter(TextureTarget.Texture2D,TextureParameterName.TextureMagFilter,(float)All.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)All.Nearest);          
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);


            FraemBufferObject = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FraemBufferObject);         
            GL.FramebufferTexture(FramebufferTarget.Framebuffer,FramebufferAttachment.DepthAttachment, DepthTextureObject,0);
            GL.DrawBuffer(DrawBufferMode.None);
            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("Не удалось скольпильнуть фраембуфер");

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Use()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer,FraemBufferObject);    
        }



        public void Draw()
        {
            Use();
            GL.DrawBuffer(DrawBufferMode.None);

        }



        ~FraemBuffer()
        {
            if (GraphicsContext.CurrentContext != null && !GraphicsContext.CurrentContext.IsDisposed && (FraemBufferObject != -1))
            {
                GL.DeleteFramebuffer(FraemBufferObject);
                GL.DeleteTexture(DepthTextureObject);
            }
                
        }









    }
}
