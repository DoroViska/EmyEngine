using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jitter.LinearMath;
using EmyEngine;
using EmyEngine.Imaging;


using Jitter;
using Jitter.Collision;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;

using OpenTK.Graphics.OpenGL4;
using EmyEngine.OpenGL;

namespace EmyEngine.Game
{
    public class NotSafeble : Attribute { }
    [NotSafeble]
    public class GameObjectJShape : GameObject
    {



        static IDraweble spt;
        static IDraweble bxt;
        static IDraweble cyl;
        static bool ModelsIneted = false;


        public GameObjectJShape(Shape shape)
        {
            sh = shape;
            if (!ModelsIneted)
            {
              
                spt = EE.СurrentResources.GetResource<IDraweble>("models/sphere.obj");
                bxt = EE.СurrentResources.GetResource<IDraweble>("models/box.obj");
                cyl = EE.СurrentResources.GetResource<IDraweble>("models/cylinder.obj");
                ModelsIneted = true;
            }
            this.Body = new ObjectivBody(this,sh);
            this.Body.Mass = 70;
        
        }

        Shape sh;
        public override void Update()
        {
            
        }

       

     



        public override void Draw()
        {
            if (!(this.Body.Tag is bool))
            {
                RenderShape(this.sh, this.Body.Position, this.Orientation);
            }
        }
        public static void RenderShape(Shape sh, JVector pos,JMatrix orient)
        {
            if (sh is BoxShape)
            {
                G.PushMatrix();
                G.SetTransform(pos, orient);
                G.Scale(((BoxShape)sh).Size.X, ((BoxShape)sh).Size.Y, ((BoxShape)sh).Size.Z);
                bxt.Draw();
                G.PopMatrix();
            }
            if (sh is SphereShape)
            {
                G.PushMatrix();
                G.SetTransform(pos, orient);
                G.Scale(((SphereShape)sh).Radius, ((SphereShape)sh).Radius, ((SphereShape)sh).Radius);
                spt.Draw();
                G.PopMatrix();

            }
            if (sh is CylinderShape)
            {
                G.PushMatrix();
                G.SetTransform(pos, orient);
                G.Scale(
                     ((CylinderShape)sh).Radius,
                   ((CylinderShape)sh).Height,
                   ((CylinderShape)sh).Radius
                    );
                cyl.Draw();
                G.PopMatrix();

            }

            if (sh is CompoundShape)
            {

                foreach (CompoundShape.TransformedShape ccc in ((CompoundShape)sh).Shapes)
                {
                    G.PushMatrix();
                    G.SetTransform(pos,orient);

                 //   Console.WriteLine();
                    RenderShape(ccc.Shape, ccc.Position, ccc.Orientation);

                    G.PopMatrix();
                }



            }

        }

       
    }
}
