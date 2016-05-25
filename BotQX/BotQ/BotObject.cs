using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmyEngine;
using EmyEngine.Game;
using EmyEngine.OpenGL;
using ICSharpCode.NRefactory.Semantics;
using Jitter.Collision;
using Jitter.Collision.Shapes;
using Jitter.Dynamics;
using Jitter.LinearMath;
using OpenTK.Input;

namespace BotQX.BotQ
{
    public class BotObject : GameObject
    {
        private IDraweble _model = null;
        private IDraweble _markerModel = null;
        private TaskTransleter _taskTransleter = null;
        private int _botSpeed;
        private bool _usePen;
        private List<BasicMarker> _markers = null;

        public void MarkersClear()
        {
            _markers.Clear();
        }

        public int BotSpeed
        {
            set
            {
                _taskTransleter.PushTask((o) =>
                {
                    this._botSpeed = Convert.ToInt32(o);
                },value);
                _taskTransleter.Wait();
            }
            get { return _botSpeed; }
        }


        public bool BotPen
        {
            set
            {
                _taskTransleter.PushTask((o) =>
                {
                    this._usePen = Convert.ToBoolean(o);
                }, value);
                _taskTransleter.Wait();
            }
            get { return this._usePen; }
        }


        public BotObject()
        {
            _botSpeed = 50;
            _taskTransleter = new TaskTransleter();
            _markers = new List<BasicMarker>();
            _model = EE.СurrentResources.GetResource<IDraweble>("/models/bot.obj");
            _markerModel = EE.СurrentResources.GetResource<IDraweble>("/models/plane0x333.obj");
   
            this.Body = new ObjectivBody(this, new BoxShape(new JVector(0.9f, 0.7f, 0.9f)));
            this.Body.IsStatic = true;
            this.Body.Mass = 4;
            this.Material.StaticFriction = 0.15f;
            this.Material.KineticFriction = 0.15f;
            this.Material.Restitution = 0;


        }


        public bool Detect()
        {
            bool isResect = false;
            _taskTransleter.PushTask((a) =>
            {
                RigidBody rezb;
                JVector rezn;
                float fr;

                isResect = this.Instance.World.CollisionSystem.Raycast(this.Position,
                    JVector.Transform(new JVector(0.6f, 0f, 0f), this.Orientation), (aa, ba, ca) =>
                    {        
                        return aa != this.Body; 
                        
                    }, out rezb,
                    out rezn, out fr) && fr < 1.0f;             
            },null);
            _taskTransleter.Wait();
            return isResect;
        }

   


        public void Move()
        {
            if(Detect())
                return;
      
            JVector rez = JVector.Transform(new JVector(1f, 0f, 0f), this.Orientation);
            if (_usePen)
                _taskTransleter.PushTask((o) =>
                {
                    this._markers.Add(new BasicMarker(this.Position + new JVector(rez.X / -3f, -0.499f, rez.Z / -3f), this._markerModel));
                }, null);        
            JVector p = new JVector(rez.X / _botSpeed, rez.Y / _botSpeed, rez.Z / _botSpeed);
            for (int i = 0; i < _botSpeed; i++)
            {
                _taskTransleter.PushTask((o) =>
                {
                    this.Position += ((JVector) o);
                }, p);
            }
            if (_usePen)
                _taskTransleter.PushTask((o) =>
                {
                    this._markers.Add(new BasicMarker(this.Position + new JVector(0.0f, -0.499f, 0.0f), this._markerModel));
                    this._markers.Add(new BasicMarker(this.Position + new JVector(rez.X / 3f, -0.499f, rez.Z / 3f), this._markerModel));
                }, null);
            _taskTransleter.Wait();
          
        }

        public void Rotate()
        {
            JMatrix rotate = JMatrix.CreateRotationY((JMath.Pi / 2) / _botSpeed);
            for (int i = 0; i < _botSpeed; i++)
            {
                _taskTransleter.PushTask((o) =>
                {
                    this.Orientation *= ((JMatrix) o);
                }, rotate);
            }

            _taskTransleter.Wait();
        }



        public override void Draw()
        {
            for(int i = 0;i < this._markers.Count;i++)
                this._markers[i].Draw();


            G.PushMatrix();
            G.SetTransform(this.Position, this.Orientation);      
            G.Rotate(JMath.Pi / 2,0f,1f,0f);   
            _model.Draw();
            G.PopMatrix();
        }




        public override void Update()
        {
            _taskTransleter.Process();
        }
    }

   
}
