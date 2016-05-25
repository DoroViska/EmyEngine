using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.GL4
{
    public interface IRenderer
    {
        
        void Save();
        void Draw();
        void AppendVertex(Vertex vertex);

    }
}
