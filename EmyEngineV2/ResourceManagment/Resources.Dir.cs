using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace EmyEngine.ResourceManagment
{
    public class ResourcesDir : Resources 
    {
        private List<MemoryFile> t;

  
        public void Open(string dir)
        {
            t = new List<MemoryFile>();
            PushDir(dir,"/");
            this.Load(t);  
        }
        public void ReadAll()
        {

            DirectoryInfo dr = new DirectoryInfo("./");
            foreach (DirectoryInfo nfpo in dr.GetDirectories())
            {
                try {
                    if (nfpo.Name.IndexOf("res") == 0)
                    {
                        PushDir(nfpo.FullName, "");
                      
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

               
            }
        }



        private void PushDir(string dir_path, string handle )
        {
            DirectoryInfo _dir_path = new DirectoryInfo(dir_path);
            if (!_dir_path.Exists) throw new Exception("Не найден игровой ресурс: " + dir_path);
            foreach (FileInfo f in _dir_path.GetFiles())
            {
                using (Stream c = f.OpenRead())
                {
                    byte[] rw = new byte[f.Length];
                    c.Read(rw,0,rw.Length);
                    t.Add(new MemoryFile(handle + f.Name, rw));
                }
            }

            foreach (DirectoryInfo d in _dir_path.GetDirectories())
            {

                this.PushDir(d.FullName,handle + d.Name + "/");
            }

        }



    }
}
