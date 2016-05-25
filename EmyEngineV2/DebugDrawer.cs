using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.Collision.Shapes;
using Jitter.LinearMath;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics;

using EmyEngine;
using EmyEngine.OpenGL;
using Material = EmyEngine.OpenGL.Material;


namespace EmyEngine
{

   

    public class DDraweble : IDebugDrawable
    {
        public void DebugDraw(IDebugDrawer drawer)
        {
            throw new __std_badOpCode_fromatException();
        }
    }

    public class DebugDrawer : IDebugDrawer
    {
        

        public void DrawLine(JVector start, JVector end)
        {
            G.Graphics.Material(Material.Defult);
            G.Graphics.DefuseColor(Color4.Red);
            G.Graphics.DrawLine(EE.Vector(start),EE.Vector(end));
        }

        public void DrawPoint(JVector pos)
        {
            G.Graphics.Material(Material.Defult);
            G.Graphics.DefuseColor(Color4.Red);
            G.Graphics.DrawPoint(EE.Vector(pos));
        }

        public void DrawTriangle(JVector pos1, JVector pos2, JVector pos3)
        {
            this.DrawLine(pos1, pos2);
            this.DrawLine(pos2, pos3);
            this.DrawLine(pos3, pos1);
        }
    }
}
