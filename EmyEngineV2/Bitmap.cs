using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine
{
    public class Bitmap
    {

        public Color4 this[int idx]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public Color4 this[int idx, int idy]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Bitmap(string ResourceName)
        {
            throw new NotImplementedException();
        }

        public Bitmap(Stream sFile)
        {
            throw new NotImplementedException();
        }

        public Bitmap(Color4[,] Color)
        {
            throw new NotImplementedException();
        }

        public Bitmap(int w, int h, Color4[] Color)
        {
            throw new NotImplementedException();
        }

        public void ReverseX()
        {
            throw new NotImplementedException();
        }

        public void ReverseY()
        {
            throw new NotImplementedException();
        }

        public Color4 GetPixel(int idx, int idy)
        {
            throw new NotImplementedException();
        }

        public void SetPixel(int idx, int idy, Color4 Color)
        {
            throw new NotImplementedException();
        }

        public int Width
        {
            get { throw new NotImplementedException(); }
        }

        public int Height
        {
            get { throw new NotImplementedException(); }
        }

        public Color4[,] DoubleDupe()
        {
            throw new NotImplementedException();
        }

        public Color4[] Dupe()
        {
            throw new NotImplementedException();
        }

    }
}
