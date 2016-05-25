using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.GL4
{
    public class Sphere
    {

        public const float X = .525731112119133606f;
        public const float Z = .850650808352039932f;

        static Vector3[] vdata = {    
           new Vector3( -X, 0.0f, Z),
             new Vector3( X ,0.0f, Z),
              new Vector3(-X, 0.0f, -Z),
              new Vector3(X, 0.0f, -Z),
              new Vector3(0.0f, Z, X),
             new Vector3( 0.0f, Z, -X),
             new Vector3( 0.0f, -Z, X),
             new Vector3( 0.0f, -Z, -X),
             new Vector3( Z, X, 0.0f),
             new Vector3( -Z, X, 0.0f),
            new Vector3(  Z, -X, 0.0f),
            new Vector3(  -Z, -X, 0.0f)
        };
        static int[,] tindices = { 
            {0,4,1}, {0,9,4}, {9,5,4}, {4,5,8}, {4,8,1},    
            {8,10,1}, {8,3,10}, {5,3,8}, {5,2,3}, {2,7,3},    
            {7,10,3}, {7,6,10}, {7,11,6}, {11,0,6}, {0,1,6}, 
            {6,1,10}, {9,0,11}, {9,11,2}, {9,2,5}, {7,2,11} };

       


        public Sphere()
        {

            for (int i = 0; i < 20; i++)
            {
                vert.AppendVertex(new Vertex(vdata[tindices[i, 0]]));
                vert.AppendVertex(new Vertex(vdata[tindices[i, 1]]));
                vert.AppendVertex(new Vertex(vdata[tindices[i, 2]]));

            }
                
            vert.Save();
        }

        VertexVert vert = new VertexVert();

        public void Draw()
        {
            vert.Draw();

        }

 


    }
}
