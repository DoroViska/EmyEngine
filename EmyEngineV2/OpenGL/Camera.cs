using OpenTK.Input;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.OpenGL
{
    public interface ICamera
    {
        void MultMatrix();
    }

    public class FlyingCamera : ICamera
    {
        public FlyingCamera()
        {
            AngleX = 0f;
            AngleY = 0f;
            PushAngle(0, 0);
        }
        public float AngleX { set; get; }
        public float AngleY { set; get; }

        public Vector3 DirectionZero = Vector3.Zero;
        public Vector3 Direction = Vector3.Zero;
        public Vector3 HeadPosition { set; get; } = new Vector3(0f, 5f, 0f);
        public Vector3 UpVector { set; get; } = new Vector3(0f, 1f, 0f);

        public void PushAngle(float x, float y)
        {

            AngleX += x;
            // if (AngleX > 359) { AngleX = 0; }
            AngleY += y;
            //  if (AngleY > 359) { AngleY = 0; }

            Direction.X = (float)Math.Cos((AngleX));
            Direction.Y = (float)Math.Tan((AngleY));
            Direction.Z = (float)Math.Sin((AngleX));
            DirectionZero = Direction;

        }

        public int oldX { set; get; }
        public int oldY { set; get; }

        public bool MyConvision { set; get; } = true;
        public void UpdateState(GameWindow wnd, bool use_mouse)
        {


            if (MyConvision)
            {
                KeyboardState s = Keyboard.GetState();
                

                if (s.IsKeyDown(Key.W))
                {
                    HeadPosition += (new Vector3(
                       (float)Math.Cos(this.AngleX) * 1
                       ,
                       0,
                       (float)Math.Sin(this.AngleX) * 1
                       ) * 0.1f); 
                }



                if (s.IsKeyDown(Key.S))
                {
                    HeadPosition += (new Vector3(
                      (float)Math.Cos(this.AngleX) * -1
                      ,
                      0,
                      (float)Math.Sin(this.AngleX) * -1
                      ) * 0.1f);

                }


                if (s.IsKeyDown(Key.A))
                {
                    HeadPosition += (new Vector3(
                    ((float)Math.Cos(this.AngleX + (float)Math.PI / 2)) * -1
                    ,
                    0,
                     ((float)Math.Sin(this.AngleX + (float)Math.PI / 2)) * -1
                    ) * 0.1f); 
                }




                if (s.IsKeyDown(Key.D))
                {

                    HeadPosition += (new Vector3(
                        ((float)Math.Cos(this.AngleX + (float)Math.PI / 2)) * 1
                        ,
                        0,
                         ((float)Math.Sin(this.AngleX + (float)Math.PI / 2)) * 1
                        ) * 0.1f); 


                }


                if (s.IsKeyDown(Key.Space))
                {

                    HeadPosition += (new Vector3(
                     0
                        ,
                        0.4f,
                       0
                        ) * 0.1f); 


                }


                if (s.IsKeyDown(Key.LControl))
                {

                    HeadPosition += (new Vector3(
                       0
                        ,
                       -0.4f,
                       0
                        ) * 0.1f); 


                }

            }


         
            int xm = Mouse.GetCursorState().X;
            int ym = Mouse.GetCursorState().Y;
          
      
            this.PushAngle(((float)this.oldX - (float)xm) / -500, ((float)this.oldY - (float)ym) / 500);
            Mouse.SetPosition(wnd.X + (ushort)(wnd.Width / 2), wnd.Y + (ushort)(wnd.Height / 2));

            this.oldX = wnd.X + (ushort)(wnd.Width / 2);
            this.oldY = wnd.Y + (ushort)(wnd.Height / 2);
            

            Direction += HeadPosition;


            
           // Transformator.LookAt(HeadPosition.X, HeadPosition.Y, HeadPosition.Z, Direction.X, Direction.Y, Direction.Z, UpVector.X, UpVector.Y, UpVector.Z);
        }

        public void MultMatrix()
        {
            G.MultMatrix(Matrix4.LookAt(HeadPosition.X, HeadPosition.Y, HeadPosition.Z, Direction.X, Direction.Y, Direction.Z, UpVector.X, UpVector.Y, UpVector.Z));

        }



        public void ResetPosition(/*IGLView wnd, bool rester*/)
        {
            //SDL.SDL_WarpMouseInWindow((SDL_Window*)wnd.TryToGetWindowPtr(), (ushort)(wnd.Width / 2), (ushort)(wnd.Height / 2));
        }



    }
}
