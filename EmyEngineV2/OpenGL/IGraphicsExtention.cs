using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.OpenGL
{
    public static class IGraphicsExtention
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawRectangle(this IGraphics self, int L, int T, int R, int B)
        {
            self.DrawRectangle(new Vector3((float)L, (float)T, 0f), new Vector3((float)R, (float)B, 0f));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawSolidRectangle(this IGraphics self, int L, int T, int R, int B)
        {
            self.DrawSolidRectangle(new Vector3((float)L, (float)T, 0f), new Vector3((float)R, (float)B, 0f));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawRectangle(this IGraphics self, Vector3 leUp, Vector3 riDown)
        {
            float XL = riDown.X - leUp.X;
            float YL = riDown.Y - leUp.Y;

            self.DrawTriangle(
                new Vector3(leUp.X, leUp.Y, leUp.Z),
               new Vector3(leUp.X + XL, leUp.Y + YL, leUp.Z),
                 new Vector3(leUp.X + XL, leUp.Y, leUp.Z)
              );


            self.DrawTriangle(
                 new Vector3(leUp.X, leUp.Y, leUp.Z),
                 new Vector3(leUp.X, leUp.Y + YL, leUp.Z),
                 new Vector3(leUp.X + XL, leUp.Y + YL, leUp.Z)
                 );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawSolidRectangle(this IGraphics self, Vector3 leUp, Vector3 riDown)
        {
            float XL = riDown.X - leUp.X;
            float YL = riDown.Y - leUp.Y;

            self.DrawSolidRectangle(
                new Vector3(leUp.X, leUp.Y, leUp.Z),
                new Vector3(leUp.X, leUp.Y + YL, leUp.Z),
                new Vector3(leUp.X + XL, leUp.Y + YL, leUp.Z),
                new Vector3(leUp.X + XL, leUp.Y, leUp.Z)
                );
        }

    }

}
