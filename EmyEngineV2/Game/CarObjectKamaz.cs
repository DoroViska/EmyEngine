﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Jitter.Collision.Shapes;
using Jitter;
using Jitter.LinearMath;

using EmyEngine;

using EmyEngine.Imaging;
using EmyEngine.OpenGL;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL4;

using EmyEngine.ResourceManagment;
#endregion

namespace EmyEngine.Game
{
    public class CarObjectKamaz : GameObject
    {
        
        IDrawable sc = null;
        IDrawable wc = null;
        float whRadius = 0.450f;
        Wheel[] wheels;
        public CarObjectKamaz()
        {

            //sc = EE.СurrentResources.GetResource<IDraweble>("models/PAZ.obj");
            sc = EE.СurrentResources.GetResource<IDrawable>("models/Kamaz.obj");         
            wc = EE.СurrentResources.GetResource<IDrawable>("models/PickUpWheel.obj");

            //sc = EE.СurrentResources.GetResource<IDraweble>("models/PAZ.obj");
            CompoundShape.TransformedShape lower = new CompoundShape.TransformedShape(
             new BoxShape(2.050f, 2.0f, 7.2f), JMatrix.Identity, JVector.Zero);

            //CompoundShape.TransformedShape upper = new CompoundShape.TransformedShape(
            //    new BoxShape(1.430f, 0.5f, 1.040f), JMatrix.Identity, new JVector(0, 0.5f, -0.2f));

            CompoundShape.TransformedShape[] subShapes = { lower /*, upper */};

            CompoundShape chassis = new CompoundShape(subShapes);
         
            Body = new DefaultCar(this, chassis);
            Body.Mass = 7000f;
           // Body.Material.Restitution = 2.0f;
           /// Body.Material.KineticFriction = 0.1f;
            Body.Material.StaticFriction = 2f;

            wheels = new Wheel[6];
            wheels[(int)WheelPosition.FrontLeft] = new Wheel(Body, new JVector(0, -1.00f, 0.6f)  + (JVector.Left * 0.7f)  + 2.80f * JVector.Forward, whRadius);
            wheels[(int)WheelPosition.FrontRight] = new Wheel(Body, new JVector(0, -1.00f, 0.6f) + (JVector.Right * 0.7f) + 2.80f * JVector.Forward  , whRadius);

            wheels[(int)WheelPosition.BackLeft] = new Wheel(Body, new JVector(0, -1.00f, -0.7f) + (JVector.Left * 0.7f) + 2.54f * JVector.Backward  , whRadius);
            wheels[(int)WheelPosition.BackRight] = new Wheel(Body, new JVector(0, -1.00f, -0.7f) + (JVector.Right * 0.7f) + 2.54f * JVector.Backward  , whRadius);

            wheels[(int)WheelPosition.Back1Left] = new Wheel(Body, new JVector(0, -1.00f, -0.7f) + (JVector.Left * 0.7f) + 1.35f * JVector.Backward, whRadius);
            wheels[(int)WheelPosition.Back1Right] = new Wheel(Body, new JVector(0, -1.00f, -0.7f) + (JVector.Right * 0.7f) + 1.35f * JVector.Backward, whRadius);

            //угол поворта
            ((DefaultCar)Body).SteerAngle = 40;
            //крутящий момент
            ((DefaultCar)Body).DriveTorque = 30;

            //ускарение
            ((DefaultCar)Body).AccelerationRate = 500f;
            //ускорение поврота
            ((DefaultCar)Body).SteerRate = 18f;
            ((DefaultCar)Body).AdjustWheelValues(wheels);
            
           // carBody.Tag = BodyTag.DontDrawMe;
            Body.AllowDeactivation = false;

            // place the car two units above the ground.
            //Body.Position = new JVector(0, 2, 0);

         //   world.AddBody(Body);
        }

        public override void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            float steer, accelerate;
            if (keyState.IsKeyDown(Key.Up))
            {
                //foreach (Wheel s in wheels)
                //{
                //    s.Locked = false;
                //}
                accelerate = 50.0f;
            }          
            else if (keyState.IsKeyDown(Key.Down))
            {
                accelerate = -50.0f;
                //foreach (Wheel s in wheels)
                //{
                //    s.Locked = false;
                //}
            }           
            else
            {
                accelerate = 0.0f;
                //foreach (Wheel s in wheels)
                //{
                //    s.Locked = true;
                //}
            }
              


            if (keyState.IsKeyDown(Key.Left))
                steer = 1;
            else if (keyState.IsKeyDown(Key.Right))
                steer = -1;
            else
                steer = 0.0f;

           
            ((DefaultCar)Body).SetInput(accelerate, steer);

         
        }

        public override void Draw()
        {
            G.PushMatrix();        
            G.SetTransform(Body.Position, Body.Orientation);
            G.Translate(0, -0.3f, -0.2f);         
            G.Scale(0.032f, 0.032f, 0.032f);
            sc.Draw();
            G.PopMatrix();    
                
            for (int i = 0; i < ((DefaultCar)Body).Wheels.Length; i++)
            {
                G.PushMatrix();
                Wheel wheel = ((DefaultCar)Body).Wheels[i];   
                G.SetTransform(wheel.GetWorldPosition(), Body.Orientation);              
                G.SetTransform(JVector.Zero, JMatrix.CreateRotationY(wheel.SteerAngle * (float)Math.PI / 180));
                G.SetTransform(JVector.Zero, JMatrix.CreateRotationX(-wheel.WheelRotation * (float)Math.PI / 180));
                G.Scale(whRadius * 1.3f, whRadius * 1.3f, whRadius * 1.3f);
                wc.Draw();              
                G.PopMatrix();
            }
            
        }

       
    }
}
