using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace EmyEngine.OpenGL
{
    public interface IDrawable
    {
        void Draw(PrimitiveType rendertype);
    }
}
