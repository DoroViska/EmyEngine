using EmyEngine.Imaging;
using EmyEngine.ResourceManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmyEngine.Platform;
using EmyEngine.OpenGL;

namespace EmyEngine.GUI
{
    /// <summary>
    /// Обычный класс UTF16 шрифта
    /// </summary>
    public class Font
    {
        public Font(Resources charRess,string charsPath)
        {
            //file.font
            CharArray  = new RenderChar[ushort.MaxValue];
            
            foreach (Texture paten in charRess.GetResources<Texture>(charsPath))
            {
                string fname = paten.Path;
                fname = fname.Remove(fname.LastIndexOf('.'), 4);
                fname = fname.Remove(0, fname.LastIndexOf('/') +1 );

                RenderChar nchr = new RenderChar(paten.TextureObject, ushort.Parse(fname),paten.Widith,paten.Height);
                CharArray[nchr.Code]=nchr;
            }
            
        }

        public RenderChar[] CharArray { private set; get; } 

    }
}
