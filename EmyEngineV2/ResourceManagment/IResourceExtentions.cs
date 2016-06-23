using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.ResourceManagment
{
    public static class IResourceExtentions
    {
        public static string GetName(this IResource self)
        {
           
                int ind = self.Path.LastIndexOf('/');
                if (ind == -1)
                    return self.Path;
                else
                    return self.Path.Remove(0, ind + 1);           
        }

        public static string GetExtention(this IResource self)
        {

            int ind = self.Path.LastIndexOf('.');
            if (ind == -1)
                return self.Path;
            else
                return self.Path.Remove(0, ind + 1);
        }


        public static Stream GetStream(this IResource self)
        {
            return new MemoryStream(self.Data);       
        }


        public static byte[] GetStreamData(Stream st)
        {
            if (st == null)
                throw new ArgumentNullException(nameof(st));
            if (!st.CanSeek)
                throw new Exception(nameof(st) + "::CanSeek returned false");
            if (!st.CanRead)
                throw new Exception(nameof(st) + "::CanRead returned false");

            st.Seek(0, SeekOrigin.Begin);
            byte[] fret = new byte[st.Length];
            st.Read(fret, 0, fret.Length);
            st.Seek(0, SeekOrigin.Begin);
            return fret;
        }



    }
}
