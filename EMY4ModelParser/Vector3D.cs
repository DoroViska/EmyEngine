using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace EmyEngine
{
  



    //    public static void Perspective(double fovy, double aspect, double zNear = 0.001, double zFar = 1000)
    //    {
          
    //        double sine, cotangent, deltaZ;
    //        double radians = fovy / 2 * Math.PI / 180;

    //        deltaZ = zFar - zNear;
    //        sine = Math.Sin(radians);
    //        if ((deltaZ == 0) || (sine == 0) || (aspect == 0))
    //        {
    //            return;
    //        }
    //        cotangent = Math.Cos(radians) / sine;
          
    //        MakeIdentityd(mBuffer);
    //        mBuffer[0, 0] = cotangent / aspect;
    //        mBuffer[1, 1] = cotangent;
    //        mBuffer[2, 2] = -(zFar + zNear) / deltaZ;
    //        mBuffer[2, 3] = -1;
    //        mBuffer[3, 2] = -2 * zNear * zFar / deltaZ;
    //        mBuffer[3, 3] = 0;
    //        GL.MultMatrix(Convert4r4To16r(mBuffer));

    //    }
    //    static double[,] mBuffer = new double[4, 4];

    //    public static void Perspective(double fovy, int W,int H, double zNear = 0.001, double zFar = 1000)
    //    {

    //        double aspect = W * 1.0 / H;
      
    //        double sine, cotangent, deltaZ;
    //        double radians = fovy / 2 * Math.PI / 180;

    //        deltaZ = zFar - zNear;
    //        sine = Math.Sin(radians);
    //        if ((deltaZ == 0) || (sine == 0) || (aspect == 0))
    //        {
    //            return;
    //        }
    //        cotangent = Math.Cos(radians) / sine;

    //        MakeIdentityd(mBuffer);
    //        mBuffer[0, 0] = cotangent / aspect;
    //        mBuffer[1, 1] = cotangent;
    //        mBuffer[2, 2] = -(zFar + zNear) / deltaZ;
    //        mBuffer[2, 3] = -1;
    //        mBuffer[3, 2] = -2 * zNear * zFar / deltaZ;
    //        mBuffer[3, 3] = 0;
    //        GL.MultMatrix(Convert4r4To16r(mBuffer));

    //    }

   
    //    public static void LookAt(Position3D eye, Position3D center, Vector3D upv)
    //    {
         

    //        Position3D forward;
    //        Position3D side = Position3D.Zero();
    //        Vector3D up;

    //        double[,] m = new double[4, 4];

    //        forward = center - eye;
    //        up = upv;

    //        forward.Normalize();
    //        side.Cross(forward, up.ToPosition3D());
    //        side.Normalize();
    //        up.Cross(side.ToVector3D(), forward.ToVector3D());

    //        MakeIdentityd(m);
    //        m[0, 0] = side.x;
    //        m[1, 0] = side.y;
    //        m[2, 0] = side.z;

    //        m[0, 1] = up.x;
    //        m[1, 1] = up.y;
    //        m[2, 1] = up.z;

    //        m[0, 2] = -forward.x;
    //        m[1, 2] = -forward.y;
    //        m[2, 2] = -forward.z;
    //        GL.MultMatrix(Convert4r4To16r(m));
    //        GL.Translate(-eye.x, -eye.y, -eye.z);
    //    }


    //    static float[] rezBuffer = new float[16];
    //    static float[] Convert4r4To16r(double[,] arty)
    //    {

    //        rezBuffer[0] = (float)arty[0, 0];
    //        rezBuffer[1] = (float)arty[0, 1];
    //        rezBuffer[2] = (float)arty[0, 2];
    //        rezBuffer[3] = (float)arty[0, 3];

    //        rezBuffer[4] = (float)arty[1, 0];
    //        rezBuffer[5] = (float)arty[1, 1];
    //        rezBuffer[6] = (float)arty[1, 2];
    //        rezBuffer[7] = (float)arty[1, 3];

    //        rezBuffer[8] = (float)arty[2, 0];
    //        rezBuffer[9] = (float)arty[2, 1];
    //        rezBuffer[10] = (float)arty[2, 2];
    //        rezBuffer[11] = (float)arty[2, 3];

    //        rezBuffer[12] = (float)arty[3, 0];
    //        rezBuffer[13] = (float)arty[3, 1];
    //        rezBuffer[14] = (float)arty[3, 2];
    //        rezBuffer[15] = (float)arty[3, 3];
    //        return rezBuffer;
    //    }


    //    static void MakeIdentityd(double[,] m)
    //    {
    //        m[0, 0] = 1;
    //        m[0, 1] = 0;
    //        m[0, 2] = 0;
    //        m[0, 3] = 0;

    //        m[1, 0] = 0;
    //        m[1, 1] = 1;
    //        m[1, 2] = 0;
    //        m[1, 3] = 0;

    //        m[2, 0] = 0;
    //        m[2, 1] = 0;
    //        m[2, 2] = 1;
    //        m[2, 3] = 0;

    //        m[3, 0] = 0;
    //        m[3, 1] = 0;
    //        m[3, 2] = 0;
    //        m[3, 3] = 1;
    //    }
    //}
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Vector3D
    {
        public float X, Y, Z;

        public override int GetHashCode()
        {
            return (int)X ^ (int)Y ^ (int)Z;
        }
        public override bool Equals(object obj)
        {
            return((Vector3D)obj == this);
        }

        public static bool operator ==(Vector3D a, Vector3D b)
        {
            if (a.X != b.X) return false;
            if (a.Y != b.Y) return false;
            if (a.Z != b.Z) return false;
            return true;
        }
        public static bool operator !=(Vector3D a, Vector3D b)
        {
            if (a.X == b.X) return false;
            if (a.Y == b.Y) return false;
            if (a.Z == b.Z) return false;
            return true;
        }


        public static Vector3D Float(float x,float y,float z)
        {
            Vector3D n;
            n.X = x;
            n.Y = y;
            n.Z = z;
            return n;
        }
        public static Vector3D Float(float x, float y)
        {
            Vector3D n;
            n.X = x;
            n.Y = y;
            n.Z = 0;
            return n;
        }

        public static Vector3D Zero()
        {
            Vector3D z;
            z.X = 0;
            z.Y = 0;
            z.Z = 0;
            return z;
        }

        public Vector3D Normalize()
        {
            Vector3D vc = this;
            float r;

            r = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            if (r == 0.0) return vc;
            vc.X /= r;
            vc.Y /= r;
            vc.Z /= r;
            return vc;
        }

        public Vector3D Cross(Vector3D v1, Vector3D v2)
        {
            Vector3D vc;
            vc.X = v1.Y * v2.Z - v1.Z * v2.Y;
            vc.Y = v1.Z * v2.X - v1.X * v2.Z;
            vc.Z = v1.X * v2.Y - v1.Y * v2.X;
            return vc;
        }

        public static Vector3D operator  +(Vector3D a,Vector3D b)
        {
            Vector3D vk;
            vk.X = a.X + b.X;
            vk.Y = a.Y + b.Y;
            vk.Z = a.Z + b.Z;
            return vk;
        }
        public static Vector3D operator -(Vector3D a, Vector3D b)
        {
            Vector3D vk;
            vk.X = a.X - b.X;
            vk.Y = a.Y - b.Y;
            vk.Z = a.Z - b.Z;
            return vk;
        }
        public static Vector3D operator *(Vector3D a, Vector3D b)
        {
            Vector3D vk;
            vk.X = a.X * b.X;
            vk.Y = a.Y * b.Y;
            vk.Z = a.Z * b.Z;
            return vk;
        }
        public static Vector3D operator /(Vector3D a, Vector3D b)
        {
            Vector3D vk;
            vk.X = a.X / b.X;
            vk.Y = a.Y / b.Y;
            vk.Z = a.Z / b.Z;
            return vk;
        }
    }
   

}
