using EmyEngine.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace EmyEngine.GUI
{
    public class RenderChar
    {
        public RenderChar(int textureObject, ushort code, int widith, int height)
        {
            


            TextureObject = textureObject;
            Code = code;
            Widith = widith;
            Height = height;
        }

        public int TextureObject { private set; get; }
        public ushort Code { private set; get; }
        public int Widith { set; get; }
        public int Height { set; get; }
    }
}
