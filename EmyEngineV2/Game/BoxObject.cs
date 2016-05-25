using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmyEngine.OpenGL;
using Jitter.Collision.Shapes;
using Jitter.LinearMath;
using OpenTK.Graphics.OpenGL4;

namespace EmyEngine.Game
{
    public class WoodDoor : GameObject
    {

        public WoodDoor()
        {
            this.Body = new ObjectivBody(this, new BoxShape(new JVector(1f, 2f, 0.1f)));
            this.Body.IsStatic = false;
            bxt = EE.СurrentResources.GetResource<IDraweble>("/models/wooddoor.obj");
            this.Body.Mass = 40f;
        }

        private IDraweble bxt = null;
        public override void Draw()
        {
            G.PushMatrix();    
            G.SetTransform(this.Position, this.Orientation);
            G.Translate(0f, -1f, 0f);
            bxt.Draw();
            G.PopMatrix();
        }


        public override void Update()
        {

        }
    }
    public class WoodPlank : GameObject
    {

        public WoodPlank()
        {
            this.Body = new ObjectivBody(this, new BoxShape(new JVector(1f, 2f, 0.1f)));
            this.Body.IsStatic = false;
            this.Body.Mass = 60f;
            bxt = EE.СurrentResources.GetResource<IDraweble>("/models/woodplank.obj");
        }

        private IDraweble bxt = null;
        public override void Draw()
        {
            G.PushMatrix();           
            G.SetTransform(this.Position, this.Orientation);
            G.Translate(0f, -1f, 0f);
            bxt.Draw();
            G.PopMatrix();
        }


        public override void Update()
        {

        }
    }
















    public class MiniPlatformObject : GameObject
    {

        public MiniPlatformObject()
        {
            this.Body = new ObjectivBody(this, new BoxShape(new JVector(10f, 1f, 8f)));
            this.Body.IsStatic = true;
            bxt = EE.СurrentResources.GetResource<IDraweble>("/models/box.obj");
        }

        private IDraweble bxt = null;
        public override void Draw()
        {
            G.PushMatrix();
            G.SetTransform(this.Position, this.Orientation);
            G.Scale(10f, 1f, 8f);
            bxt.Draw();
            G.PopMatrix();
        }

  
        public override void Update()
        {

        }
    }
    public class PlatformObject : GameObject
    {

        public PlatformObject()
        {
            this.Body = new ObjectivBody(this, new BoxShape(new JVector(300f, 1f, 300f)));
            this.Body.IsStatic = true;
            bxt = EE.СurrentResources.GetResource<IDraweble>("/models/box.obj");
        }

        private IDraweble bxt = null;
        public override void Draw()
        {
            G.PushMatrix();
            G.SetTransform(this.Position, this.Orientation);
            G.Scale(300f, 1f, 300f);
            bxt.Draw();
            G.PopMatrix();
        }


        public override void Update()
        {

        }
    }
    public class BoxObject : GameObject
    {
       
        public BoxObject()
        {
            this.Body = new ObjectivBody(this,new BoxShape(new JVector(1f,1f,1f)));
            bxt = EE.СurrentResources.GetResource<IDraweble>("/models/box.obj");
        }

        private IDraweble bxt = null;
        public override void Draw()
        {
            G.PushMatrix();
            G.SetTransform(this.Position, this.Orientation);      
            bxt.Draw();
            G.PopMatrix();
        }

        

        public override void Update()
        {
            
        }
    }
}
