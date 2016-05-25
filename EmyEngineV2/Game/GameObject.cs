using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using EmyEngine;
using EmyEngine.Imaging;


using Jitter;
using Jitter.Collision;
using Jitter.Dynamics;
using Jitter.Collision.Shapes; 
using Jitter.LinearMath;
namespace EmyEngine.Game
{






    public abstract class GameObject
    {
    
        #region IDContenter        
        public static uint NextID = 0;
        private static object LookID = new object();
        public static uint TakeID()
        {
            lock(LookID)
            {
                NextID++;
                return NextID;
            }      
        }

        public uint ID { get; private set; } = TakeID();
        #endregion

        #region overrides
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is GameObject))
                return false;
            if (((GameObject)obj).ID == this.ID)
                return true;
            else
                return false;         
        }
        public override int GetHashCode()
        {
            return (int)this.ID;
        }
        public override string ToString()
        {
            return string.Format("GameObject: name = {0}, id = {1}, position = {2}", this.Name,this.ID,this.Body?.Position);
        }
        #endregion

        public string Name { set; get; }
        public GameObject()
        {
            this.Name = this.GetType().Name;
        }


        public abstract void Draw();
        //public abstract void TopologyDraw();
        public abstract void Update();

        public virtual void AddedToInstance(GameInstance instance)
        {

        }
        public virtual void RemovedFromInstance(GameInstance instance)
        {

        }


        
        public ObjectivBody Body { set; get;}
        public float HP { set; get; }

       
        public GameInstance Instance { set; get; }



        public JMatrix Orientation {
            set { Body.Orientation = value; }
            get { return Body.Orientation;  }
        }
        public JVector Position {
            set { Body.Position = value; }
            get { return Body.Position; }
        }
        public float Mass
        {
            set { Body.Mass = value; }
            get { return Body.Mass; }
        }
        public Material Material
        {
            set { Body.Material = value; }
            get { return Body.Material; }
        }


    }
}
