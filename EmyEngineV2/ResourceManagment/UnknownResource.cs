using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace EmyEngine.ResourceManagment
{
    public class UnknownResource : IResource
    {
        public UnknownResource(string Path, byte[] data)
        {
            this.Data = data;
            this.Path = Path;
        }
  

        public Stream GetStream()
        {
            return new MemoryStream(Data);
        }

        public string Path { private set; get; }
        public string Name
        {
            get
            {
               int ind = Path.LastIndexOf('/');
               if(ind == -1)
                    return  Path;
               else
                    return Path.Remove(0, ind + 1);
            }
        }


        public byte[] Data  { set; get; }


    }
}
