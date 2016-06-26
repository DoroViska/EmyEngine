using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jitter;
using Jitter.Collision;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;
using Jitter.LinearMath;
namespace EmyEngine.Game
{
    public class ObjectivBody : RigidBody
    {
        public GameObject BodyGameObject { private set; get; }

        public ObjectivBody(GameObject obj,Shape shape) : base(shape)
        {
            //this.AllowDeactivation = false;
            this.BodyGameObject = obj;
        
        }
        public ObjectivBody(GameObject obj, Shape shape,Material mat) : base(shape, mat)
        {
            //this.AllowDeactivation = false;
            this.BodyGameObject = obj;
          
        }

        public ObjectivBody(GameObject obj, Shape shape, Material mat,bool isPar) : base(shape, mat, isPar)
        {
            //this.AllowDeactivation = false;
            this.BodyGameObject = obj;
        }


        ///
        ///x1
        ///x2
        ///6t


    }
}
