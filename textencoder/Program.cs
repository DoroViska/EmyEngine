using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace textencoder
{
    class Program
    {

        public static void remove_spaces(ref string data )
        {
            if (data.Contains(" "))
                data = data.Replace(" ", "");
        }


        static void Main(string[] args)
        {
            string datra = File.ReadAllText("./sfs.txt");
            remove_spaces(ref datra);
            string[] bytes = datra.Split(',');
            using (Stream f = new FileStream("./out.txt",FileMode.OpenOrCreate,FileAccess.ReadWrite))
            {
                foreach (string sdfsdf in bytes)
                {
                    f.WriteByte(byte.Parse(sdfsdf));
                }
            }
        }
    }
}
