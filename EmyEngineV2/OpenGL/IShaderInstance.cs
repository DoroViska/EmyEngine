using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.OpenGL
{
    public interface IShaderInstance
    {
        ShaderProgram Program { set; get; }
        void UpdateState(Material Material);

    }





}
