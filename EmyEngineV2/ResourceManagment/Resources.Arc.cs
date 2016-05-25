using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace EmyEngine.ResourceManagment
{
    public class ResourcesArc : Resources
    {
        public void Open(Stream fuil)
        {
            List<MemoryFile> fs = new List<MemoryFile>();

            using (ZipFile zip = ZipFile.Read(fuil))
            {
                ZipEntry[] entr = zip.ToArray();

                foreach (ZipEntry zp in entr)
                {
                    if (!zp.IsDirectory)
                        using (System.IO.MemoryStream df = new System.IO.MemoryStream())
                        {
                            zp.Extract(df);
                            fs.Add(new MemoryFile("/" + zp.FileName, df.ToArray()));
                        }
                }
            }

            this.Load(fs);
        }
        public void Open(string fuil)
        {
            if(!File.Exists(fuil))
                return;          
            List<MemoryFile> fs = new List<MemoryFile>();

            using (ZipFile zip = ZipFile.Read(fuil))
            {
                ZipEntry[] entr = zip.ToArray();
  
                foreach (ZipEntry zp in entr)
                {
                    if(!zp.IsDirectory)
                        using (System.IO.MemoryStream df = new System.IO.MemoryStream())
                        {
                            zp.Extract(df);
                            fs.Add(new MemoryFile("/" + zp.FileName, df.ToArray()));                   
                        }                                               
                }
            }

            this.Load(fs);
        }
    }
}
