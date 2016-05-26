using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsers
{
    public class WtObjectParser
    {
        public class WtMaterial
        {
            public string Name;
            public WtVector3 Ambient;
            public WtVector3 Defuse;
            public WtVector3 Specular;
            public float DefuseAlpha;
            public string MapDefuse = null;
            public string MapSpecular = null;
            public string MapAmbient = null;
        }
        public struct WtFace
        {
            public int V0, V1, V2;
            public int VN0, VN1, VN2;
            public int VT0, VT1, VT2;
        }
        public struct WtVector2
        {
            public float X;
            public float Y;
        }
        public struct WtVector3
        {
            public float X;
            public float Y;
            public float Z;
        }

        public class WtGroup : IComparable
        {
            public List<WtFace> Faces { set; get; } = new List<WtFace>(1000);
            public WtMaterial Material;

            public int CompareTo(object obj)
            {
                if (((WtGroup)obj).Material.DefuseAlpha > this.Material.DefuseAlpha)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        public List<WtVector3> Vertexes { get; } = new List<WtVector3>(1000);
        public List<WtVector3> Normals { get; } = new List<WtVector3>(1000);
        public List<WtVector2> TextureCoords { get; } = new List<WtVector2>(1000);
        public List<WtGroup> Groups { get; } = new List<WtGroup>(10);
        public List<WtMaterial> Materials { get; } = new List<WtMaterial>(10);

        private static string Vt = "vt";
        private static string Vn = "vn";
        private static string V = "v";
        private static string F = "f";
        private static string Usemtl = "usemtl";
        private static string Newmtl = "newmtl";
        private static string D = "d";
        private static string MapKa = "map_Ka";
        private static string MapKd = "map_Kd";
        private static string MapKs = "map_Ks";
        private static string Ka = "Ka";
        private static string Kd = "Kd";
        private static string Ks = "Ks";

    
        private WtMaterial _getMaterialFromName(string name)
        {
            for (int i = 0;i < Materials.Count; i++)
            {
                if (Materials[i].Name == name)
                    return Materials[i];
            }
            throw new Exception($"Material {name} not found");
        }



        public void ParseMtl(StreamReader data)
        {
            string parse = string.Empty;
            WtMaterial CurrentMaterial = null;
            while ((parse = data.ReadLine()) != null)
            {

                int castword = WordsParser.WordIndex(ref parse, 0);

                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref Newmtl, 0, 6))
                {

                    WtMaterial gp = new WtMaterial();
                    CurrentMaterial = gp;

                    Materials.Add(CurrentMaterial);
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.Name = WordsParser.WordStringValue(ref parse, castword);
                    continue;
                }
                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref MapKa, 0, 6))
                {
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.MapAmbient = WordsParser.WordStringValue(ref parse, castword);
                    continue;
                }
                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref MapKs, 0, 6))
                {
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.MapSpecular = WordsParser.WordStringValue(ref parse, castword);
                    continue;
                }
                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref MapKd, 0, 6))
                {
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.MapDefuse = WordsParser.WordStringValue(ref parse, castword);
                    continue;
                }


                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref D, 0, 1))
                {
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.DefuseAlpha = WordsParser.WordFloatValue(ref parse, castword);              
                    continue;
                }


                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref Kd, 0, 2))
                {         
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.Defuse.X = WordsParser.WordFloatValue(ref parse, castword);
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.Defuse.Y = WordsParser.WordFloatValue(ref parse, castword);
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.Defuse.Z = WordsParser.WordFloatValue(ref parse, castword);
                    continue;
                }
                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref Ka, 0, 2))
                {
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.Ambient.X = WordsParser.WordFloatValue(ref parse, castword);
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.Ambient.Y = WordsParser.WordFloatValue(ref parse, castword);
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.Ambient.Z = WordsParser.WordFloatValue(ref parse, castword);
                    continue;
                }
                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref Ks, 0, 2))
                {
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.Specular.X = WordsParser.WordFloatValue(ref parse, castword);
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.Specular.Y = WordsParser.WordFloatValue(ref parse, castword);
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentMaterial.Specular.Z = WordsParser.WordFloatValue(ref parse, castword);
                    continue;
                }

            }
        }

        public void ParseObj(StreamReader data)
        {
            string parse = string.Empty;
            WtGroup CurrentGroup = null;
            while ((parse = data.ReadLine()) != null)
            {
            
                int castword = WordsParser.WordIndex(ref parse, 0);

                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref Usemtl, 0, 6))
                {

                    WtGroup gp = new WtGroup();
                    CurrentGroup = gp;

                    Groups.Add(CurrentGroup);
                    castword = WordsParser.WordNext(ref parse, castword);
                    CurrentGroup.Material = _getMaterialFromName( WordsParser.WordStringValue(ref parse, castword));
                    continue;
                }

                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref F, 0, 1))
                {

                    WtFace v;
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.V0 = WordsParser.WordIntValue(ref parse, castword) - 1;
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.VT0 = WordsParser.WordIntValue(ref parse, castword) - 1;
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.VN0 = WordsParser.WordIntValue(ref parse, castword) - 1;
                   


                    castword = WordsParser.WordNext(ref parse, castword);
                    v.V1 = WordsParser.WordIntValue(ref parse, castword) - 1;
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.VT1 = WordsParser.WordIntValue(ref parse, castword) - 1;
                    castword = WordsParser.WordNext(ref parse , castword);
                    v.VN1 = WordsParser.WordIntValue(ref parse, castword) - 1;
            
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.V2 = WordsParser.WordIntValue(ref parse, castword) - 1;
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.VT2 = WordsParser.WordIntValue(ref parse, castword) - 1;
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.VN2 = WordsParser.WordIntValue(ref parse, castword) - 1;
                 

                    //Console.WriteLine("f {0}/{1}/{2} {3}/{4}/{5} {6}/{7}/{8}", v.V0, v.VN0, v.VT0, v.V1, v.VN1,v.VT1, v.V2, v.VN2, v.VT2);
                    CurrentGroup.Faces.Add(v);

                    continue;
                }


                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref Vn, 0, 2))
                {

                    WtVector3 v;
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.X = WordsParser.WordFloatValue(ref parse, castword);
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.Y = WordsParser.WordFloatValue(ref parse, castword);
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.Z = WordsParser.WordFloatValue(ref parse, castword);
                    //Console.WriteLine("VN {0}, {1}, {2}", v.X, v.Y, v.Z);
                    Normals.Add(v);

                    continue;
                }

                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref Vt, 0, 2))
                {

                    WtVector2 v;
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.X = WordsParser.WordFloatValue(ref parse, castword);
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.Y = WordsParser.WordFloatValue(ref parse, castword);
                    //Console.WriteLine("VT {0}, {1}", v.X, v.Y);
                    TextureCoords.Add(v);

                    continue;
                }


                if (WordsParser.WordCmp(ref parse, castword, WordsParser.WordLen(ref parse, castword), ref V, 0, 1))
                {

                    WtVector3 v;
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.X = WordsParser.WordFloatValue(ref parse, castword);
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.Y = WordsParser.WordFloatValue(ref parse, castword);
                    castword = WordsParser.WordNext(ref parse, castword);
                    v.Z = WordsParser.WordFloatValue(ref parse, castword);
                    //Console.WriteLine("V {0}, {1}, {2}",v.X,v.Y, v.Z);
                    Vertexes.Add(v);

                    continue;
                }
              
               
            }

        }




    }
}
