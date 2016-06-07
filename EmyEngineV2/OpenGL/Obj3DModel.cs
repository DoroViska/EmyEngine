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

        #region IResource

        public byte[] Data
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Stream GetStream()
        {
            throw new NotImplementedException();
        }

        public string Path { set; get; }

        #endregion


        #region IDraweble

        public void Draw()
        {
            for (int i = 0; i < _vertexArrays.Count; i++)
            {
                _vertexArrays[i].Draw();
            }
        }
        public void Draw(PrimitiveType rendertype)
        {
            for (int i = 0; i < _vertexArrays.Count; i++)
            {
                _vertexArrays[i].Draw(rendertype);
            }
        }
        #endregion

        public List<VertexObject> _vertexArrays = new List<VertexObject>();

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Vector3(WtObjectParser.WtVector3 v)
        {
            return new Vector3(v.X,v.Y, v.Z);
        }
        public static Vector2 Vector2(WtObjectParser.WtVector2 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public void Parse(Stream fobj, Stream fmtl, Resources textures, string textures_handle = "/")
        {
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
                this._vertexArrays.Add(nobj);

            }

        }

   

        //private class vertexobj_and_name_pair
        //{
        //    public vertexobj_and_name_pair()
        //    {
        //    }

        //    public string Name;
        //    public VertexObject ArrayObj;
        //}

        //private void push_mtl(string name, ref VertexObject CurrentVertexObject)
        //{
        //    foreach (vertexobj_and_name_pair a in _vertexArrays)
        //    {
        //        if (a.Name == name)
        //        {
        //            CurrentVertexObject = a.ArrayObj;
        //            return;
        //        }

        //    }
        //    throw new EmyEngineFindException(name);
        //}

        //private static void remove_spaces(ref string txt)
        //{
        //    txt = txt.Replace("  ", " ");
        //    txt = txt.Replace("\t", "");
        //    if (txt.Contains("  "))
        //        remove_spaces(ref txt);
        //}
        ////private static void remove_minuses(ref string[] txt)
        ////{
        ////    for (int i = 0;i<txt.Length;i++)
        ////    {
        ////        txt[i] = txt[i].Replace("-", "");

        ////    }

        ////}

        //private List<vertexobj_and_name_pair> _vertexArrays = null;
        //private List<Vector3> _vertexes = null;
        //private List<Vector2> _textureCoords = null;
        //private List<Vector3> _normals = null;




        public void Parse(Resources _r, MemoryStream _obj, string filenameWithoutEXT)
        {
            this.Path = filenameWithoutEXT + ".obj";
            Stream _mtl = _r.GetResource(filenameWithoutEXT + ".mtl").GetStream();
            int idx = filenameWithoutEXT.LastIndexOf("/");
            string dir = filenameWithoutEXT.Remove(idx, filenameWithoutEXT.Length - idx);
            Parse(_obj, _mtl, _r, dir + "/");


        }





        ////public enum ReadedWordType 
        ////{
        ////    Number,
        ////    V,
        ////    Vn,
        ////    Vt,
        ////    F,
        ////    None
        ////}

        ////private bool IsNum(char ch)
        ////{
        ////    switch (ch)
        ////    {
        ////        case '0':
        ////            return true;
        ////        case '1':
        ////            return true;
        ////        case '2':
        ////            return true;
        ////        case '3':
        ////            return true;
        ////        case '4':
        ////            return true;
        ////        case '5':
        ////            return true;
        ////        case '6':
        ////            return true;
        ////        case '7':
        ////            return true;
        ////        case '8':
        ////            return true;
        ////        case '9':
        ////            return true;
        ////        case '.':
        ////            return true;
        ////        default:
        ////            return false;
        ////    }
        ////}

        ////private StringBuilder _stringBuilder = new StringBuilder(1000);
        ////public ReadedWordType GetNextWord(string buff,int wordIndex, out int mabyrezult0d, out int mabyrezult1d, out int mabyrezult2d, out float mabyrezultf)
        ////{
        ////    ReadedWordType retType = ReadedWordType.None;
        ////    mabyrezultf = 0f;
        ////    mabyrezult0d = -1;
        ////    mabyrezult1d = -1;
        ////    mabyrezult2d = -1;

        ////    int wordindexed = 1;
        ////    if (wordIndex == wordindexed)
        ////    {
        ////        bool isCurrentBlock = false;
        ////        for (int i = 0; i < buff.Length; i++)
        ////        {
        ////            if (buff[i] == ' ')
        ////            {
        ////            }



        ////        }



        ////    }




        ////    if (wordIndex == 0)
        ////    {
        ////        bool isSession = false;
        ////        for (int i = 0; i < buff.Length; i++)
        ////        {
        ////            if (buff[i] == ' ')
        ////                break;
        ////            if (buff[i] == 'f')
        ////            {
        ////                retType = ReadedWordType.F;
        ////                break;
        ////            }
        ////            if (buff[i] == 'v')
        ////            {
        ////                if (buff[i + 1] == 'n')
        ////                {
        ////                    retType = ReadedWordType.Vn;
        ////                    break;
        ////                }
        ////                if (buff[i + 1] == 't')
        ////                {
        ////                    retType = ReadedWordType.Vt;
        ////                    break;
        ////                }
        ////                if (buff[i + 1] == ' ')
        ////                {
        ////                    retType = ReadedWordType.V;
        ////                    break;
        ////                }
        ////            }      
        ////        }
        ////        return retType;
        ////    }


        ////    return retType;
        ////}


        ////public void Parse(Stream fobj, Stream fmtl, Resources textures, string textures_handle = "/")
        ////{


        ////    _vertexArrays = new List<vertexobj_and_name_pair>();
        ////    _vertexes = new List<Vector3>();
        ////    _textureCoords = new List<Vector2>();
        ////    _normals = new List<Vector3>();




        ////}



















        //public void Parse(Stream fobj,Stream fmtl,Resources textures,string textures_handle = "/")
        //{


        //    _vertexArrays = new List<vertexobj_and_name_pair>(10000);
        //    _vertexes = new List<Vector3>(10000);
        //    _textureCoords = new List<Vector2>(10000);
        //    _normals = new List<Vector3>(10000);

        //    VertexObject CurrentVertexObject = null;
        //    vertexobj_and_name_pair cur = null;
        //    using (StreamReader reder = new StreamReader(fmtl))
        //    {

        //        while (reder.Peek() >= 0)
        //        {
        //            string line = reder.ReadLine().Replace('.', ',');
        //            remove_spaces(ref line);
        //            line = line.Trim(' ');
        //            string[] splited = line.Split(' ');
        //            if (splited[0] == "newmtl")
        //            {
        //                if (cur != null)
        //                    _vertexArrays.Add(cur);
        //                cur = new vertexobj_and_name_pair();
        //                cur.ArrayObj = new VertexObject();
        //                cur.Name = splited[1];
        //            }
        //            if (splited[0] == "Ka")
        //            {
        //                cur.ArrayObj.Material.Ambient.R = float.Parse(splited[1]);
        //                cur.ArrayObj.Material.Ambient.G = float.Parse(splited[2]);
        //                cur.ArrayObj.Material.Ambient.B = float.Parse(splited[3]);
        //            }
        //            if (splited[0] == "Kd")
        //            {
        //                cur.ArrayObj.Material.Defuse.R = float.Parse(splited[1]);
        //                cur.ArrayObj.Material.Defuse.G = float.Parse(splited[2]);
        //                cur.ArrayObj.Material.Defuse.B = float.Parse(splited[3]);
        //            }
        //            if (splited[0] == "Ks")
        //            {
        //                cur.ArrayObj.Material.Specular.R = float.Parse(splited[1]);
        //                cur.ArrayObj.Material.Specular.G = float.Parse(splited[2]);
        //                cur.ArrayObj.Material.Specular.B = float.Parse(splited[3]);
        //            }
        //            if (splited[0] == "d")
        //            {
        //                cur.ArrayObj.Material.Defuse.A = float.Parse(splited[1]);
        //                cur.ArrayObj.Material.Ambient.A = float.Parse(splited[1]);
        //                cur.ArrayObj.Material.Specular.A = float.Parse(splited[1]);
        //            }
        //            //Чё за хрень (Tr)? ваще не понятно на* он нужен?
        //            //if (splited[0] == "Tr")
        //            //{
        //            //    cur.ArrayObj.Material.Defuse.A = float.Parse(splited[1]);
        //            //    cur.ArrayObj.Material.Ambient.A = float.Parse(splited[1]);
        //            //    cur.ArrayObj.Material.Specular.A = float.Parse(splited[1]);

        //            //}
        //            if (splited[0] == "map_Kd")
        //            {
        //                cur.ArrayObj.Material.DefuseMap = textures.GetResource<Texture>(textures_handle + splited[1].Replace(',','.')).TextureObject;
        //                //cur.ArrayObj.Material.Enables = cur.ArrayObj.Material.Enables | MapActivity.Defuse;
        //            }
        //            if (splited[0] == "map_Ks")
        //            {
        //                cur.ArrayObj.Material.SpecularMap = textures.GetResource<Texture>(textures_handle + splited[1].Replace(',', '.')).TextureObject;
        //                //cur.ArrayObj.Material.Enables = cur.ArrayObj.Material.Enables | MapActivity.Specular;
        //            }
        //            if (splited[0] == "map_Ka")
        //            {
        //                cur.ArrayObj.Material.AmbientMap = textures.GetResource<Texture>(textures_handle + splited[1].Replace(',', '.')).TextureObject;
        //                //cur.ArrayObj.Material.Enables = cur.ArrayObj.Material.Enables | MapActivity.Ambient;
        //            }

        //        }

        //        if (cur != null)
        //            _vertexArrays.Add(cur);
        //    }
        //    using (StreamReader reder = new StreamReader(fobj))
        //    {

        //        while (reder.Peek() >= 0)
        //        {

        //            string line = reder.ReadLine().Replace('.', ',');
        //            remove_spaces(ref line);
        //            line = line.Trim(' ');
        //            string[] splited = line.Split(' ');
        //            if (splited[0] == "v")
        //            {
        //                _vertexes.Add(new Vector3(float.Parse(splited[1]), float.Parse(splited[2]), float.Parse(splited[3])));
        //            }
        //            if (splited[0] == "vn")
        //            {
        //                _normals.Add(new Vector3(float.Parse(splited[1]), float.Parse(splited[2]), float.Parse(splited[3])));
        //            }
        //            if (splited[0] == "vt")
        //            {
        //                _textureCoords.Add(new Vector2(float.Parse(splited[1]), float.Parse(splited[2])));
        //            }
        //        //}


        //        //while (reder.Peek() >= 0)
        //        //{
        //        //    string line = reder.ReadLine().Replace('.',',');
        //        //    RemoveSpaces(ref line);
        //        //    string[] splited = line.Split(' ');
        //            if (splited[0] == "usemtl")
        //            {
        //                if(CurrentVertexObject != null)
        //                    CurrentVertexObject.Save();
        //                this.push_mtl(splited[1],ref CurrentVertexObject);
        //            }


        //            if (splited[0] == "f")
        //            {

        //                string[] A = splited[1].Split('/');
        //                {
        //                    Vertex v = new Vertex();
        //                    v.Position = _vertexes[ int.Parse(A[0]) - 1];
        //                    if (A[1] != string.Empty && A[1] != null)
        //                        v.TextureCoords = _textureCoords[int.Parse(A[1]) - 1];
        //                    if (A[2] != string.Empty && A[2] != null)
        //                        v.Normal = _normals[int.Parse(A[2]) - 1];
        //                    CurrentVertexObject.AppendVertex(v);
        //                }
        //                string[] B = splited[2].Split('/');
        //                {                            
        //                    Vertex v = new Vertex();
        //                    v.Position = _vertexes[int.Parse(B[0]) - 1];
        //                    if (B[1] != string.Empty && B[1] != null)
        //                        v.TextureCoords = _textureCoords[int.Parse(B[1]) - 1];
        //                    if (B[2] != string.Empty && B[2] != null)
        //                        v.Normal = _normals[int.Parse(B[2]) - 1];
        //                    CurrentVertexObject.AppendVertex(v);
        //                }
        //                string[] C = splited[3].Split('/');
        //                {
        //                    Vertex v = new Vertex();
        //                    v.Position = _vertexes[int.Parse(C[0]) - 1];
        //                    if (C[1] != string.Empty && C[1] != null)
        //                        v.TextureCoords = _textureCoords[int.Parse(C[1]) - 1];
        //                    if (C[2] != string.Empty && C[2] != null)
        //                        v.Normal = _normals[int.Parse(C[2]) - 1];
        //                    CurrentVertexObject.AppendVertex(v);
        //                }                                   
        //                if (splited.Length == 5)
        //                {
        //                    CurrentVertexObject.DrawType = OpenTK.Graphics.OpenGL4.PrimitiveType.TriangleStrip;
        //                    string[] E = splited[4].Split('/');
        //                    {
        //                        Vertex v = new Vertex();

        //                        v.Position = _vertexes[int.Parse(E[0]) - 1];
        //                        if (E[1] != string.Empty && E[1] != null)
        //                            v.TextureCoords = _textureCoords[int.Parse(E[1]) - 1];
        //                        if (E[2] != string.Empty && E[2] != null)
        //                            v.Normal = _normals[int.Parse(E[2]) - 1];
        //                        CurrentVertexObject.AppendVertex(v);
        //                    }




        //                }


        //            }

        //        }
        //        if (CurrentVertexObject != null)
        //            CurrentVertexObject.Save();

        //    }

        //}


    }
}
