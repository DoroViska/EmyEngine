using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;
using Jitter.LinearMath;
using Jitter;
using EmyEngine.ResourceManagment;
namespace EmyEngine.Game
{

    /// <summary>
    /// Enumeration for the four car wheels.
    /// </summary>
    public enum WheelPosition
    {
        /// <summary>Front left wheel.</summary>
        FrontLeft,
        /// <summary>Front right wheel.</summary>
        FrontRight,
        /// <summary>Back left wheel.</summary>
        BackLeft,
        /// <summary>Back right wheel.</summary>
        BackRight,

        Back1Left,
        /// <summary>Back right wheel.</summary>
        Back1Right,

        Back2Left,
        /// <summary>Back right wheel.</summary>
        Back2Right
    }

    /// <summary>
    /// Creates the Jitter default car with 4 wheels. To create a custom car
    /// use the Wheel class and add it to a body.
    /// </summary>
    public class DefaultCar : ObjectivBody
    {

        // the default car has 4 wheels
        private Wheel[] wheels = null;
   

        private float destSteering = 0.0f;   
        private float destAccelerate = 0.0f;
        private float steering = 0.0f;
        private float accelerate = 0.0f;
    
        /// <summary>
        /// The maximum steering angle in degrees
        /// for both front wheels
        /// </summary>
        public float SteerAngle { get; set; }

        /// <summary>
        /// The maximum torque which is applied to the
        /// car when accelerating.
        /// </summary>
        public float DriveTorque { get; set; }

        /// <summary>
        /// Lower/Higher the acceleration of the car.
        /// </summary>
        public float AccelerationRate { get; set; }

        /// <summary>
        /// Lower/Higher the steering rate of the car.
        /// </summary>
        public float SteerRate { get; set; }

        // don't damp perfect, allow some bounciness.
        private const float dampingFrac = 0.5f;
        private const float springFrac = 0.1f;

        //World.WorldStep postStep;




        /// <summary>
        /// Initializes a new instance of the DefaultCar class.
        /// </summary>
        /// <param name="world">The world the car should be in.</param>
        /// <param name="shape">The shape of the car. Recommend is a box shape.</param>      
        public DefaultCar(GameObject obj, Shape shape) : base(obj, shape)
        {
         
            //postStep = new World.WorldStep(world_PostStep);
        
            //world.Events.PostStep += postStep;

            // set some default values
            this.AccelerationRate = 5.0f;
            this.SteerAngle = 20.0f;
            this.DriveTorque = 50.0f;
            this.SteerRate = 5.0f;
            this.IsActive = true;

            this.Material.StaticFriction = 0.15f;
            this.Material.KineticFriction = 0.15f;
            this.Material.Restitution = 0.1f;


            // create default wheels
            //wheels[(int)WheelPosition.FrontLeft] = new Wheel( this, new JVector(0, -0.3f, -0.34f) + JVector.Left + 1.8f * JVector.Forward + 0.8f * JVector.Down, 0.5f);
            //wheels[(int)WheelPosition.FrontRight] = new Wheel( this, new JVector(0, -0.3f, -0.34f) + JVector.Right + 1.8f * JVector.Forward + 0.8f * JVector.Down, 0.5f);

            //wheels[(int)WheelPosition.BackLeft] = new Wheel( this, new JVector(0, -0.3f, -1.1f) + JVector.Left + 1.8f * JVector.Backward + 0.8f * JVector.Down, 0.5f);
            //wheels[(int)WheelPosition.BackRight] = new Wheel( this, new JVector(0, -0.3f, -1.1f) + JVector.Right + 1.8f * JVector.Backward + 0.8f * JVector.Down, 0.5f);

            //AdjustWheelValues();
        }

        /// <summary>
        /// This recalculates the inertia, damping and spring of all wheels based
        /// on the car mass, the wheel radius and the gravity. Should be called
        /// after manipulating wheel data.
        /// </summary>
        public void AdjustWheelValues(Wheel[] wheels)
        {
            this.wheels = wheels;
            float mass = this.Mass / 4;

            foreach (Wheel w in wheels)
            {
                //w.Inertia = 0.5f * (w.Radius * w.Radius) * mass;
                //w.Spring = mass * new JVector(0f,-10f,0f).Length() / (w.WheelTravel * springFrac) * 0.8f;           
               // w.Damping = 233.3f;

                w.Inertia = 0.5f * (w.Radius * w.Radius) * mass;
                w.Spring = mass * new JVector(0f, -10f, 0f).Length() / (w.WheelTravel * springFrac) * 0.3f;
                w.Damping = 1.0f * (float)System.Math.Sqrt(w.Spring * this.Mass) * 0.25f * dampingFrac;

                //w.Damping = this.Mass * 1.5f;
            }
        }

        /// <summary>
        /// Access the wheels. Wheel index follows <see cref="WheelPosition"/>
        /// </summary>
        public Wheel[] Wheels { get { return wheels; } }

        /// <summary>
        /// Set input values for the car.
        /// </summary>
        /// <param name="accelerate">A value between -1 and 1 (other values get clamped). Adjust
        /// the maximum speed of the car by setting <see cref="DriveTorque"/>. The maximum acceleration is adjusted
        /// by setting <see cref="AccelerationRate"/>.</param>
        /// <param name="steer">A value between -1 and 1 (other values get clamped). Adjust
        /// the maximum steer angle by setting <see cref="SteerAngle"/>. The speed of steering
        /// change is adjusted by <see cref="SteerRate"/>.</param>
        public void SetInput(float accelerate, float steer)
        {
            destAccelerate = accelerate;
            destSteering = steer;
        }

        public override void PreStep(float timestep)
        {
            foreach (Wheel w in wheels) w.PreStep(this.BodyGameObject.Instance.World ,timestep);
        }

        public override void PostStep(float timestep)
        {
            float deltaAccelerate = timestep * AccelerationRate;
            float deltaSteering = timestep * SteerRate;

            float dAccelerate = destAccelerate - accelerate;
            dAccelerate = JMath.Clamp(dAccelerate, -deltaAccelerate, deltaAccelerate);

            accelerate += dAccelerate;

            float dSteering = destSteering - steering;
            dSteering = JMath.Clamp(dSteering, -deltaSteering, deltaSteering);

            steering += dSteering;

            float maxTorque = DriveTorque * 0.5f;

            foreach (Wheel w in wheels)
            {
                w.AddTorque(maxTorque * accelerate);
            }

            float alpha = SteerAngle * steering;

            wheels[(int)WheelPosition.FrontLeft].SteerAngle = alpha;
            wheels[(int)WheelPosition.FrontRight].SteerAngle = alpha;


            foreach (Wheel w in wheels) w.PostStep(timestep);
        }



    }
}
