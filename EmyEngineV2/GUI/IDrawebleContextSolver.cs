using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmyEngine.OpenGL;

namespace EmyEngine.GUI
{
    public interface IDrawebleContextSolver
    {
        float ZCounter { set; get; }
        int GetWidth();
        int GetHeight();     
        IGraphics CreateGraphics();
    }
}
