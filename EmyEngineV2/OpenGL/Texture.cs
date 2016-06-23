using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
using System.IO;
using EmyEngine.ResourceManagment;
using LibPtr;
#if SYSTEM_DRAWING
using System.Drawing.Imaging;
using System.Drawing;
#else
using EmyEngine.Platform;
#endif



namespace EmyEngine.OpenGL
{
    public class Texture : AutoRealaseSafeUnmanagmentClass, IResource
    {
 
        public string Path { private set; get; }

        public byte[] Data { set; get;}

        public override DisposeInformation Realase()
        {
            if (GraphicsContext.CurrentContext != null && !GraphicsContext.CurrentContext.IsDisposed)
            {
                GL.DeleteTexture(TextureObject);
                return DisposeInformation.Sucsses;
            }
            return DisposeInformation.Error;
        }


        public override void Acquire()
        {
            TextureObject = GL.GenTexture();
        }
    


        public Texture(Stream file_t,string filename)
        {
            if (file_t == null)
                throw new ArgumentNullException(nameof(file_t));
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            this.Path = filename;
            this.Data = IResourceExtentions.GetStreamData(file_t);
            using (file_t)
            {


                this.Acquire();
                if (TextureObject < 1)
                    throw new GLInstanceNotCreated();
                GL.BindTexture(TextureTarget.Texture2D, TextureObject);
  
                BaseBitmap bmp = new BaseBitmap(file_t);
                bmp.RotateY();
                Widith = bmp.Width;
                Height = bmp.Height;
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.UnsignedByte, bmp.ColorMap);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
     


        }

        public int TextureObject { private set; get; } = 0;
        public int Widith { private set; get; }
        public int Height { private set; get; }

    }
}
