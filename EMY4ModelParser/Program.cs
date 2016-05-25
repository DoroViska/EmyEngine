using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace EmyEngine
{
    class Program
    {
        static void Main(string[] args)
        {

            foreach (string sb in args )
            {

                Model3DS s = new Model3DS();
                s.ModelFromResources(sb);
                object y = s.Draw();
                XmlSerializer ser = new XmlSerializer(typeof(FlatModel));
                Stream f = File.OpenWrite("./model.xml");       
                ser.Serialize(f,y);
                f.Close();


            }





        }
    }
}
