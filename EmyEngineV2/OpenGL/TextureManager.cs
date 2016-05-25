using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.OpenGL
{
    public class TextureManager : IEnumerable
    {
        private List<Texture> Textures = null;
        public TextureManager()
        {
            Textures = new List<Texture>();

        }
        public void Add(Texture textyres)
        {
            Textures.Add(textyres);
        }
        public void Remove(Texture textyres)
        {
            Textures.Remove(textyres);
        }
        public Texture Get(string name)
        {
            foreach (Texture a in Textures)           
                if (a.Path == name)               
                    return a;     
            throw new EmyEngineFindException(name);
        }

        public IEnumerator GetEnumerator()
        {
            return Textures.GetEnumerator();
        }
    }
}
