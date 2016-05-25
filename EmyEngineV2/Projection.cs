using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmyEngine.OpenGL;
using OpenTK;

namespace EmyEngine
{
    public class Projection
    {


        public static void PushRay(int mouseX,int mouseY,int w,int h,ref Vector3 nearPoint, ref Vector3 farPoint)
        {
            Width = w;
            Height = h;
            nearPoint = new Vector3(mouseX, mouseY, 0);
            farPoint = new Vector3(mouseX, mouseY, 1);
            nearPoint = Unproject(nearPoint, G.Projection, G.View, Matrix4.Identity);
            farPoint = Unproject(farPoint, G.Projection, G.View, Matrix4.Identity);

        }



        public static float Width = 0;
        public static float Height = 0;

        public static float X = 0;
        public static float Y = 0;

        public static float MaxDepth = 1f;
        public static float MinDepth = 0;


        public static Vector3 Project(Vector3 source, Matrix4 projection, Matrix4 view, Matrix4 world)
        {
            Matrix4 matrix = Matrix4.Mult(Matrix4.Mult(world, view), projection);
            Vector3 vector = Vector3.Transform(source, matrix);
            float a = (((source.X * matrix.M14) + (source.Y * matrix.M24)) + (source.Z * matrix.M34)) + matrix.M44;
            if (!WithinEpsilon(a, 1f))
            {
                vector = (Vector3)(vector / a);
            }
            vector.X = (((vector.X + 1f) * 0.5f) * Width) + X;
            vector.Y = (((-vector.Y + 1f) * 0.5f) * Height) + Y;
            vector.Z = (vector.Z * (MaxDepth - MinDepth)) + MinDepth;
            return vector;
            
        }




        public static Vector3 Unproject(Vector3 source, Matrix4 projection, Matrix4 view, Matrix4 world)
        {
            Matrix4 matrix = Matrix4.Invert(Matrix4.Mult(Matrix4.Mult(world, view), projection));
            source.X = (((source.X - X) / ((float)Width)) * 2f) - 1f;
            source.Y = -((((source.Y - Y) / ((float)Height)) * 2f) - 1f);
            source.Z = (source.Z - MinDepth) / (MaxDepth - MinDepth);
            Vector3 vector = Vector3.Transform(source, matrix);
            float a = (((source.X * matrix.M14) + (source.Y * matrix.M24)) + (source.Z * matrix.M34)) + matrix.M44;
            if (!WithinEpsilon(a, 1f))
            {
                vector = (Vector3)(vector / a);
            }
            return vector;
        }

        private static bool WithinEpsilon(float a, float b)
        {
            float num = a - b;
            return ((-1.401298E-45f <= num) && (num <= float.Epsilon));
        }
    }
}
