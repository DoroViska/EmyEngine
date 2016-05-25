using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmyEngine;
using EmyEngine.Game;
using EmyEngine.OpenGL;
using Jitter.Collision.Shapes;
using Jitter.LinearMath;


namespace BotQX.BotQ
{
    public class WoodBlockObject : GameObject
    {
        private IDraweble _model = null;
        public WoodBlockObject()
        {
            _model = EE.СurrentResources.GetResource<IDraweble>("/models/wood.obj");
            this.Body = new ObjectivBody(this,new BoxShape(new JVector(1f,1f,1f)));
            this.Body.IsStatic = true;
            this.Body.Mass = 4;
            this.Material.StaticFriction = 0.15f;
            this.Material.KineticFriction = 0.15f;
            this.Material.Restitution = 0;


        }

        public override void Draw()
        {
            G.PushMatrix();
            G.SetTransform(this.Position,this.Orientation);
            _model.Draw();
            G.PopMatrix();
        }

        public override void Update()
        {
        }
    }

    public class MetalBlockObject : GameObject
    {
        private IDraweble _model = null;
        public MetalBlockObject()
        {
            _model = EE.СurrentResources.GetResource<IDraweble>("/models/metal.obj");
            this.Body = new ObjectivBody(this, new BoxShape(new JVector(1f, 1f, 1f)));
            this.Body.IsStatic = true;
            this.Body.Mass = 4;
            this.Material.StaticFriction = 0.15f;
            this.Material.KineticFriction = 0.15f;
            this.Material.Restitution = 0;


        }

        public override void Draw()
        {
            G.PushMatrix();
            G.SetTransform(this.Position, this.Orientation);
            _model.Draw();
            G.PopMatrix();
        }

        public override void Update()
        {
        }
    }



}
