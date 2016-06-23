using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK;
using EmyEngine.OpenGL;
using EmyEngine.ResourceManagment;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Parsers;
using System.Runtime.CompilerServices;

namespace EmyEngine.OpenGL
{

    public class Obj3DModel : IDraweble, IResource
    {

    
        public byte[] Data { set; get;  }

        public string Path { set; get; }


        public void Draw(PrimitiveType rendertype)
        {
            for (int i = 0; i < VertexArrays.Count; i++)
            {
                VertexArrays[i].Draw(rendertype);
            }
        }

        public List<VertexObject> VertexArrays { set; get; } = new List<VertexObject>();

        private static Vector3 Vector3(WtObjectParser.WtVector3 v)
        {
            return new Vector3(v.X,v.Y, v.Z);
        }

        private static Vector2 Vector2(WtObjectParser.WtVector2 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public void Parse(Stream fobj, Stream fmtl, Resources textures, string textures_handle = "/")
        {
            this.Data = IResourceExtentions.GetStreamData(fobj);

            WtObjectParser objparser = new WtObjectParser();
            objparser.ParseMtl(new StreamReader(fmtl));
            objparser.ParseObj(new StreamReader(fobj));
            objparser.Groups.Sort();
            foreach (WtObjectParser.WtGroup group in objparser.Groups)
            {
                VertexObject nobj = new VertexObject();

                nobj.Material.Defuse = new Color4(group.Material.Defuse.X, group.Material.Defuse.Y, group.Material.Defuse.Z, group.Material.DefuseAlpha);
                nobj.Material.Ambient = new Color4(group.Material.Ambient.X, group.Material.Ambient.Y, group.Material.Ambient.Z, group.Material.DefuseAlpha);
                nobj.Material.Specular = new Color4(group.Material.Specular.X, group.Material.Specular.Y, group.Material.Specular.Z, group.Material.DefuseAlpha);
                if (group.Material.MapDefuse != null)
                    nobj.Material.DefuseMap = textures.GetResource<Texture>(textures_handle + group.Material.MapDefuse).TextureObject;
                if (group.Material.MapSpecular != null)
                    nobj.Material.SpecularMap = textures.GetResource<Texture>(textures_handle + group.Material.MapSpecular).TextureObject;
                if (group.Material.MapAmbient != null)
                    nobj.Material.AmbientMap = textures.GetResource<Texture>(textures_handle + group.Material.MapAmbient).TextureObject;

                nobj.Capacity = (group.Faces.Count * 3);

                for (int i = 0;i < group.Faces.Count; i++)
                {
                  
                    Vertex vt = new Vertex();
                    vt.Position = Vector3(objparser.Vertexes[group.Faces[i].V0]);
                    vt.Normal = Vector3(objparser.Normals[group.Faces[i].VN0]);
                    vt.TextureCoords = Vector2(objparser.TextureCoords[group.Faces[i].VT0]);
                    nobj.AppendVertex(vt);
           
              
                    vt.Position = Vector3(objparser.Vertexes[group.Faces[i].V1]);
                    vt.Normal = Vector3(objparser.Normals[group.Faces[i].VN1]);
                    vt.TextureCoords = Vector2(objparser.TextureCoords[group.Faces[i].VT1]);
                    nobj.AppendVertex(vt);

                  
                    vt.Position = Vector3(objparser.Vertexes[group.Faces[i].V2]);
                    vt.Normal = Vector3(objparser.Normals[group.Faces[i].VN2]);
                    vt.TextureCoords = Vector2(objparser.TextureCoords[group.Faces[i].VT2]);
                    nobj.AppendVertex(vt);

                }
                nobj.Save();
                this.VertexArrays.Add(nobj);

            }

        }

        public void Parse(Resources _r, MemoryStream _obj, string filenameWithoutEXT)
        {
            this.Path = filenameWithoutEXT + ".obj";
            Stream _mtl = _r.GetResource(filenameWithoutEXT + ".mtl").GetStream();
            int idx = filenameWithoutEXT.LastIndexOf("/");
            string dir = filenameWithoutEXT.Remove(idx, filenameWithoutEXT.Length - idx);
            Parse(_obj, _mtl, _r, dir + "/");


        }

    }
}
