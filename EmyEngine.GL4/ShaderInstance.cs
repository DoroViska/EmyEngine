using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.GL4
{
    public abstract class ShaderInstance
    {
        public abstract ShaderProgram Program { set; get; }

        public void UpdateState(Color4 Defuse,Color4 Ambient, Color4 Specular,int DefuseMap, int AmbientMap, int SpecularMap)
        {


        }

    }
}
