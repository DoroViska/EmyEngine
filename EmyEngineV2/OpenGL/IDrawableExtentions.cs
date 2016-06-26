using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.OpenGL
{
    public static class IDrawableExtentions
    {
        public static void Draw(this IDrawable self)
        {
            self.Draw(OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles);
        }
    }
}
