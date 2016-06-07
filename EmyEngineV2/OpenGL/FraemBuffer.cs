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
    public class FraemBuffer : Bindable
    {
        public int FraemBufferObject { private set; get; } = -1;
        public int DepthTextureObject { private set; get; } = -1;

        public FraemBuffer() : this(FramebufferAttachment.DepthAttachment, 8192, 8192) { }

        public FraemBuffer(FramebufferAttachment atahment, int w, int h)
        {

   
            DepthTextureObject = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, DepthTextureObject);
            if(atahment == FramebufferAttachment.DepthAttachment)
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent16, w, h, 0, PixelFormat.DepthComponent, PixelType.Float, (IntPtr)0);
            else
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, w, h, 0, PixelFormat.Rgb, PixelType.UnsignedByte, (IntPtr)0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)All.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)All.Nearest);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        
            FraemBufferObject = GL.GenFramebuffer();
      
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FraemBufferObject);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent, w, h);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, atahment, RenderbufferTarget.Renderbuffer, FraemBufferObject);

            GL.FramebufferTexture(FramebufferTarget.Framebuffer, atahment, DepthTextureObject, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            //GL.DrawBuffer(DrawBufferMode.None);
            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("Не удалось скольпильнуть фраембуфер: " + GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer));

       



        }
    

   


        public void Draw()
        {
            Use();
            GL.DrawBuffer(DrawBufferMode.None);

        }

        public override void Use()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FraemBufferObject);
        }

        public override void UnUsed()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
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
