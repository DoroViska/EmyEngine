using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.OpenGL
{
    public static class IDrawebleExtentions
    {
        public static void Draw(this IDraweble self)
        {
            self.Draw(OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles);
        }
    }
}
