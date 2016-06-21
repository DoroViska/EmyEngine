using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace EmyEngine.GUI
{
    public struct Point
    {
        public Point(int XY)
        {
            this.X = XY;
            this.Y = XY;
        }

        public Point(int X,int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public int X;
        public int Y;


        public Vector3 Vector3()
        {
            return new Vector3(this.X,this.Y,0f);
        }

        public float Length
        {
            get { return (float)Math.Sqrt(X*X + Y*Y); }
        }

        public Vector2 Vector2()
        {
            return new Vector2(this.X, this.Y);
        }

        public static bool operator ==(Point A, Point B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(Point A, Point B)
        {
            return !A.Equals(B);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Point p = (Point)obj;
            return (X == p.X) && (Y == p.Y);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }
     
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X,a.Y + b.Y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public static Point operator *(Point a, Point b)
        {
            return new Point(a.X * b.X, a.Y * b.Y);
        }

        public static Point operator /(Point a, Point b)
        {
            return new Point(a.X / b.X, a.Y / b.Y);
        }



    }
}
