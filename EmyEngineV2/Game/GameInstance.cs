using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GameInstance
    {    
        private bool _debugDraweble = false;

        private List<GameObject> _gameObjects;

        public bool UsingMultiThreding { set; get; } 

        public GameObject this[int nindex]
        {
            get { return _gameObjects[nindex]; }
        }

        public GameObject Index(int nindex)
        {
            return _gameObjects[nindex];
        }


        public int Length { get { return this._gameObjects.Count; } }

        public GameInstance()
        {
          
            _gameObjects = new List<GameObject>();
            this.WorldCollisionSystem = new CollisionSystemSAP();
            ((CollisionSystemSAP)this.WorldCollisionSystem).UseTerrainNormal = true;
            ((CollisionSystemSAP)this.WorldCollisionSystem).UseTriangleMeshNormal = true;
      
            this.UsingMultiThreding = true;
            //this.WorldCollisionSystem.CollisionDetected += WorldCollisionSystem_CollisionDetected;
            this.World = new World(this.WorldCollisionSystem);
            this.World.SetIterations(100, 100);     
            this.World.SetDampingFactors(0.95f, 0.95f);
            this.World.SetInactivityThreshold(0.005f, 0.005f, 1);
            // this.World.Gravity = new JVector(0f,-10f,0f);
        }

        //private void WorldCollisionSystem_CollisionDetected(RigidBody body1, RigidBody body2, JVector point1, JVector point2, JVector normal, float penetration)
        //{
            
        //}

        public void ProgramIterations(int iter)
        {
            this.World.SetIterations(iter, iter);
        }

        public void ProgramIterations(int iter,int imin)
        {
            this.World.SetIterations(iter, imin);
        }

        public void Update(float step)
        {
            for (int objid = 0; objid < this._gameObjects.Count; objid++)
            {
                this._gameObjects[objid].Update();

            }
            World.Step(step, UsingMultiThreding);
           
        }

        public void EnableDebugDraweble()
        {
            _debugDraweble = true;
            foreach (GameObject obj in _gameObjects)
            {
                obj.Body.EnableDebugDraw = true;
            }
        }

        public void DisableDebugDraweble()
        {
            _debugDraweble = false;
            foreach (GameObject obj in _gameObjects)
            {
                obj.Body.EnableDebugDraw = false;
            }
        }

        public World World { private set; get;}

        public CollisionSystem WorldCollisionSystem { private set; get; }

        public void AddObjectOnlyDraw(GameObject obj)
        {
            obj.Instance = this;
            _gameObjects.Add(obj);
            obj.AddedToInstance(this);
        }

        public void AddObject(GameObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            if (obj.Body == null)
                throw new ObjectBodyIsNullException();
            if (_debugDraweble)
                obj.Body.EnableDebugDraw = true;


            obj.Instance = this;         

            World.AddBody(obj.Body);   
                    
            _gameObjects.Add(obj);

            obj.AddedToInstance(this);
        }

        public void RemoveObject(GameObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            obj.Instance = null;     
             
            World.RemoveBody(obj.Body);
            

            _gameObjects.Remove(obj);
            obj.RemovedFromInstance(this);
        }

        public void ClearObjects()
        {
            _gameObjects.Clear();
            World.Clear();
            foreach (GameObject obj in _gameObjects)
            {
                obj.Instance = null;
                obj.RemovedFromInstance(this);
            }
        }

        public void UpdateInstanceObject(GameObject obj)
        {
            obj.Instance = this;        
        }


    }
}
