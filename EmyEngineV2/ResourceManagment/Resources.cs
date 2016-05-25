using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EmyEngine.OpenGL;

namespace EmyEngine.ResourceManagment
{
    public abstract class Resources : IEnumerable<IResource>
    {
        #region const/static
        public const string PathSeparator = "/";

        public static string PathFormat(params string[] r)
        {
            return string.Join(PathSeparator, r);
        }
        #endregion


        private List<IResource> _tree  = new List<IResource>();
        private static string get_ext(string name)
        {
            int ind = name.LastIndexOf('.');
            if (ind == -1)
                return name.ToLower();
            else
                return name.Remove(0, ind).ToLower();
        }

        public class MemoryFile
        {
            public MemoryFile(string fileName, byte[] data)
            {
                FileName = fileName;
                Data = data;
                IsFree = false;
            }

            private string fileName;
            private byte[] data;
            public bool IsFree { set; get; }

            public string FileName
            {
                get
                {
                    return fileName;
                }

                set
                {
                    fileName = value;
                }
            }

            public byte[] Data
            {
                get
                {
                    return data;
                }

                set
                {
                    data = value;
                }
            }
        }


        public T GetResource<T>(string path)
        {
            if (path.Contains("//"))
                path = path.Replace("//","/");
            if (path[0] != '/')
                path = "/" + path;
            foreach (IResource _r in _tree)
            {
                if ((path == _r.Path) && (_r is T))
                {                   
                    return (T)_r;
                }

            }
            throw new ResourceNotFoundException(path);
        }
        public IResource GetResource(string path)
        {
            if (path[0] != '/')
                path = "/" + path;
            foreach (IResource _r in _tree)
            {
                if (path == _r.Path)
                {
                    return _r;
                }

            }
            throw new ResourceNotFoundException(path);
        }
        public T[] GetResources<T>(string path)
        {
            if (path[0] != '/')
                path = "/" + path;
            if (path[path.Length - 1] != '/')
                path = path + "/";
            string handle = path;


            List<T> g = new List<T>();
            foreach (IResource rs in _tree)
            {
                try
                {
                    if (rs.Path.Length < handle.Length) continue;
                    if ((rs.Path.Remove(handle.Length, rs.Path.Length - handle.Length) == handle) && rs is T)
                    {
                        //Console.WriteLine("[res]: " + rs.FullName);
                        g.Add((T)rs);
                    }


                }
                catch (Exception) {  }

            }
            return g.ToArray();

        }
        public IResource[] GetResources(string path)
        {
            if (path[0] != '/')
                path = "/" + path;
            if (path[path.Length - 1] != '/')
                path = path + "/";
            string handle = path;
           

            List<IResource> g = new List<IResource>();
            foreach (IResource rs in _tree)
            {
                try
                {
                    if (rs.Path.Length < handle.Length) continue;
                    if (rs.Path.Remove(handle.Length, rs.Path.Length - handle.Length) == handle)
                    {
                        //Console.WriteLine("[res]: " + rs.FullName);
                        g.Add(rs);
                    }


                }
                catch (Exception ex) { ex.ToString(); }

            }
            return g.ToArray();

        }
        
        public void Load(List<MemoryFile> files)
        {
            foreach (MemoryFile objT in files)
            {
                string ext = get_ext(objT.FileName);
                if (ext == ".png" || ext == ".jpg" || ext == ".bmp")
                {
                    EE.Log("add texture: " + objT.FileName);
                    using (MemoryStream b = new MemoryStream(objT.Data))
                    {
                        IResource addinit = new Texture(b, objT.FileName);
                        this._tree.Add(addinit);
                    }                 
                    objT.IsFree = true;
                }
                if (ext == ".mtl")
                {
                    EE.Log("add text: " + objT.FileName);
                    this._tree.Add(new UnknownResource(objT.FileName, objT.Data));
                    objT.IsFree = true;
                }

            }
            foreach (MemoryFile objT in files)
            {
                string ext = get_ext(objT.FileName);
                if (ext == ".obj")
                {
           
                    Obj3DModel md = new Obj3DModel();
                    string fname = objT.FileName;
                    fname = fname.Remove(fname.LastIndexOf('.'), 4);

                    md.Parse(this,new MemoryStream(objT.Data),fname);

                    EE.Log("add 3dModel: " + objT.FileName);
                    this._tree.Add(md);
                    objT.IsFree = true;
                }

            }



            foreach (MemoryFile objT in files)
            {
                if (!objT.IsFree)
                {
                    EE.Log("add res: " + objT.FileName);
                    this._tree.Add(new UnknownResource(objT.FileName, objT.Data));
                    objT.IsFree = true;
                }
        
             
            }
        }




        public IEnumerator<IResource> GetEnumerator()
        {
            return _tree.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tree.GetEnumerator();
        }
    }
}
