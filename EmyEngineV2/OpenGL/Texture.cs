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
#if SYSTEM_DRAWING
using System.Drawing.Imaging;
using System.Drawing;
#else
using EmyEngine.Platform;
#endif



namespace EmyEngine.OpenGL
{
    public class Texture : IResource
    {
        #region IResource
        public string Path { private set; get; }
        public byte[] Data
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public Stream GetStream() { throw new NotImplementedException(); }
        #endregion
    
        public Texture(Stream file_t,string filename)
        {
            if (file_t == null)
                throw new ArgumentNullException(nameof(file_t));
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException(nameof(filename));
            this.Path = filename;
            using (file_t)
            {
              
           
                TextureObject = GL.GenTexture();
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


        ~Texture()
        {
            if (GraphicsContext.CurrentContext != null && !GraphicsContext.CurrentContext.IsDisposed)
            {
                GL.DeleteTexture(TextureObject);             
            }

        }
    
    }
}
