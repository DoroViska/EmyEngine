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
  
        public string Path { private set; get; }
      
        public byte[] Data  { set; get; }


    }
}
