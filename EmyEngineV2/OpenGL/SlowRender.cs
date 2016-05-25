using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using EmyEngine.GUI;
using EmyEngine.Platform;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace EmyEngine.OpenGL
{
    public class SlowRender : IGraphics
    {
        public SlowRender()
        {
            _vrt = new VertexObject();
            for (int i = 0; i < 4; i++)
                _vrt.AppendVertex(new Vertex());
            _vrt.Save();      
            _norml = new Vector3();
            _curent_material = OpenGL.Material.Defult;
        }

        private VertexObject _vrt;
        private Vector3 _norml;
        private Material _curent_material;

        public float LineWidth
        {
            set
            {              
                GL.LineWidth(value);
            }
            get
            {
                float val = 0f;
                GL.GetFloat(GetPName.LineWidth, out val);
                return val;
            }
        }

        public void DefuseMap(int color)
        {
            _curent_material.DefuseMap = color;
        }
        public void AmbientMap(int color)
        {
            _curent_material.AmbientMap = color;
        }
        public void SpecularMap(int color)
        {
            _curent_material.SpecularMap = color;
        }


        public void DefuseColor(Color4 color)
        {
            _curent_material.Defuse = color;
        }
        public void AmbientColor(Color4 color)
        {
            _curent_material.Ambient = color;
        }
        public void SpecularColor(Color4 color)
        {
            _curent_material.Specular = color;
        }

        public void Material(Material mt)
        {
            _curent_material = mt;
        }
        public void Normal(Vector3 nrm)
        {
            _norml = nrm;
        }

        public void DrawTexturedRectangle(Vector3 v, Vector2 tv, Vector3 v1, Vector2 tv1, Vector3 v2, Vector2 tv2, Vector3 v3, Vector2 tv3)
        {
            _vrt.Size = 4;
            for (int i = 0; i < _vrt.Size; i++)
                _vrt.SetNormal(i, _norml);
            _vrt.SetPosition(0, v);
            _vrt.SetPosition(1, v1);
            _vrt.SetPosition(2, v2);
            _vrt.SetPosition(3, v3);
            _vrt.SetTextureCoord(0, tv);
            _vrt.SetTextureCoord(1, tv1);
            _vrt.SetTextureCoord(2, tv2);
            _vrt.SetTextureCoord(3, tv3);
            _vrt.Save();
            _vrt.DrawType = PrimitiveType.TriangleStrip;
            _vrt.Material = _curent_material;
         
            _vrt.Draw();
        }
        public void DrawRectangle(Vector3 v, Vector3 v1, Vector3 v2, Vector3 v3)
        {
            _vrt.Size = 4;
            for (int i = 0; i < _vrt.Size; i++)
                _vrt.SetNormal(i, _norml);
            _vrt.SetPosition(0, v);
            _vrt.SetPosition(1, v1);
            _vrt.SetPosition(2, v2);
            _vrt.SetPosition(3, v3);
            _vrt.Save();
            _vrt.DrawType = PrimitiveType.TriangleStrip;
            _vrt.Material = _curent_material;
         
            _vrt.Draw();
        }

        public void DrawTexturedTriangle(Vector3 v,Vector2 tv, Vector3 v1, Vector2 tv1,Vector3 v2, Vector2 tv2)
        {
            _vrt.Size = 3;
            for (int i = 0; i < _vrt.Size; i++)
                _vrt.SetNormal(i, _norml);
            _vrt.SetPosition(0, v);
            _vrt.SetPosition(1, v1);
            _vrt.SetPosition(2, v2);
            _vrt.SetTextureCoord(0, tv);
            _vrt.SetTextureCoord(1, tv1);
            _vrt.SetTextureCoord(2, tv2);
            _vrt.Save();
            _vrt.DrawType = PrimitiveType.Triangles;
            _vrt.Material = _curent_material;
        
            _vrt.Draw();
        }
        public void DrawTriangle(Vector3 v,Vector3 v1,Vector3 v2)
        {
            _vrt.Size = 3;
            for (int i = 0; i < _vrt.Size; i++)
                _vrt.SetNormal(i,_norml);
            _vrt.SetPosition(0, v);
            _vrt.SetPosition(1, v1);
            _vrt.SetPosition(2, v2);
            _vrt.Save();
            _vrt.DrawType = PrimitiveType.Triangles;
            _vrt.Material = _curent_material;
       
            _vrt.Draw();
        }
        public void DrawLine(Vector3 v, Vector3 v1)
        {
            _vrt.Size = 2;
            for (int i = 0; i < _vrt.Size; i++)
                _vrt.SetNormal(i, _norml);
            _vrt.SetPosition(0, v);
            _vrt.SetPosition(1, v1);
            _vrt.Save();
            _vrt.DrawType = PrimitiveType.Lines;
            _vrt.Material = _curent_material;
        
            _vrt.Draw();
        }
        public void DrawPoint(Vector3 v)
        {
            _vrt.Size = 1;
            for (int i = 0; i < _vrt.Size; i++)
                _vrt.SetNormal(i, _norml);
            _vrt.SetPosition(0, v);   
            _vrt.Save();
            _vrt.DrawType = PrimitiveType.Points;
            _vrt.Material = _curent_material;
         
            _vrt.Draw();
        }

        public void Push()
        {
            G.PushMatrix();
        }

        public void Pop()
        {
            G.PopMatrix();
        }

        public void DrawSolidRectangle(Vector3 v, Vector3 v1, Vector3 v2, Vector3 v3)
        {
            _vrt.Size = 4;
            for (int i = 0; i < _vrt.Size; i++)
                _vrt.SetNormal(i, _norml);
            _vrt.SetPosition(0, v);
            _vrt.SetPosition(1, v1);
            _vrt.SetPosition(2, v2);
            _vrt.SetPosition(3, v3);
            _vrt.Save();
            _vrt.DrawType = PrimitiveType.LineLoop;
            _vrt.Material = _curent_material;
     
            _vrt.Draw();
        }

        public void DrawSolidTriangle(Vector3 v, Vector3 v1, Vector3 v2)
        {
            _vrt.Size = 3;
            for (int i = 0; i < _vrt.Size; i++)
                _vrt.SetNormal(i, _norml);
            _vrt.SetPosition(0, v);
            _vrt.SetPosition(1, v1);
            _vrt.SetPosition(2, v2);
            _vrt.Save();
            _vrt.DrawType = PrimitiveType.LineStrip;
            _vrt.Material = _curent_material;
         
            _vrt.Draw();
        }


        public void DrawRectangle(Vector3 leUp, Vector3 riDown)
        {
            float XL = riDown.X - leUp.X;
            float YL = riDown.Y - leUp.Y;

            DrawTriangle(
                new Vector3(leUp.X, leUp.Y, leUp.Z),
               new Vector3(leUp.X + XL, leUp.Y + YL, leUp.Z),
                 new Vector3(leUp.X + XL, leUp.Y, leUp.Z)
              //new Vector3(leUp.X + XL, leUp.Y, leUp.Z)
              );


            DrawTriangle(
                 new Vector3(leUp.X, leUp.Y, leUp.Z),
                 new Vector3(leUp.X, leUp.Y + YL, leUp.Z),
                 new Vector3(leUp.X + XL, leUp.Y + YL, leUp.Z)
                 //new Vector3(leUp.X + XL, leUp.Y, leUp.Z)
                 );

           
        }


        public void DrawSolidRectangle(Vector3 leUp, Vector3 riDown)
        {
            float XL = riDown.X - leUp.X;
            float YL = riDown.Y - leUp.Y;

            DrawSolidRectangle(
                new Vector3(leUp.X, leUp.Y, leUp.Z),
                new Vector3(leUp.X, leUp.Y + YL, leUp.Z),
                new Vector3(leUp.X + XL, leUp.Y + YL, leUp.Z),
                new Vector3(leUp.X + XL, leUp.Y, leUp.Z)
                );
        }


        //float XL = riDown.X - leUp.X;
        //float YL = riDown.Y - leUp.Y;

        //GL.Vertex2(leUp.X, leUp.Y);
        //            GL.Vertex2(leUp.X + XL, leUp.Y);
        //            GL.Vertex2(leUp.X + XL, leUp.Y + YL);
        //            GL.Vertex2(leUp.X, leUp.Y + YL);
        //            GL.Vertex2(leUp.X, leUp.Y);
        public void Move(Vector3 v)
        {
            G.Translate(v);
        }

        public void DrawText(string text, Point point, Color4 color, object currentFont, float fontSize)
        {
            G.PushMatrix();
            G.Translate(point.Vector3());
            this.DefuseColor(Color4.White);
            this.AmbientColor(color);
            G.Scale(fontSize,fontSize,fontSize);
            TextAlgoritm.BaseTextRender(text,(Font)currentFont);
            G.PopMatrix();
        }
    }
}
