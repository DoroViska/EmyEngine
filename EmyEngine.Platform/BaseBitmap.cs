using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EmyEngine.Platform.SDL2;
using EmyEngine;
using System.Runtime.InteropServices;

#pragma warning disable
namespace EmyEngine.Platform
{
    public enum PixelColorsCounts : int
    {
        Color1 = 1,
        Color2 = 2,
        Color3 = 3,
        Color4 = 4
    }
    public enum ColorDelim : int
    {
        Float,
        Byte,
        Int32
    }
  
    public unsafe class BaseBitmap 
    {

    
        public PixelColorsCounts OnePixel { private set; get; }
        public int Width { private set; get;}
        public int Height { private set; get; }

        /// <summary>
        /// It's basic rgba color array
        /// </summary>
        public Color[,] ColorMap { private set; get; }
       
        public void SetPixel(int x, int y, Color color4)
        {
            if (x > this.Width || x < 0)
                throw new ArgumentOutOfRangeException("Пиксель X не может быть '> Width' и '< 0'");
            if (y > this.Height || y < 0)
                throw new ArgumentOutOfRangeException("Пиксель Y не может быть '> Height' и '< 0'");
            fixed (Color* Pixels = &ColorMap[0, 0])
            {
                Pixels[this.Width * (y - 1) + (x - 1)] = color4;
            }
        }

        public Color GetPixel(int x, int y)
        {
            if (x > this.Width || x < 0)
                throw new ArgumentOutOfRangeException("Пиксель X не может быть '> Width' и '< 0'");
            if (y > this.Height || y < 0)
                throw new ArgumentOutOfRangeException("Пиксель Y не может быть '> Height' и '< 0'");
            fixed (Color* Pixels = &ColorMap[0, 0])
            {
                Color c = Pixels[this.Width * (y - 1) + (x - 1)];
                return c;
            }
           
        }




        


        public void RotateY()
        {
            //  Console.WriteLine(GetPixel(0,3) + " :::: " + ColorMap[0,3]);

            fixed (Color* Pixels = &ColorMap[0, 0])
            {
                Color[] buffer = new Color[this.Width * this.Height];

                //memscopy
                for (int i = 1; i < this.Width * this.Height; i++)
                {
                    buffer[i] = Pixels[i];
                }

                for (int y = 1; y != this.Height; y++)
                {
                    for (int x = 1; x != this.Width; x++)
                    {
                        SetPixel(x, this.Height - y, buffer[GetArryNum(x, y)]);

                    }

                }
       
            }

        }




        public void RotateX()
        {
            fixed (Color* Pixels = &ColorMap[0, 0])
            {
                Color[] buffer = new Color[this.Width * this.Height];

                //memscopy
                for (int i = 1; i < this.Width * this.Height; i++)
                {
                    buffer[i] = Pixels[i];
                }

                for (int y = 1; y != this.Height; y++)
                {
                    for (int x = 1; x != this.Width; x++)
                    {
                        SetPixel(this.Width - x, y, buffer[GetArryNum(x, y)]);

                    }

                }
            }
        }

        public int GetArryNum(int x, int y)
        {
            if (x > this.Width || x < 0)
                throw new ArgumentOutOfRangeException("Пиксель X не может быть '> Width' и '< 0'");
            if (y > this.Height || y < 0)
                throw new ArgumentOutOfRangeException("Пиксель Y не может быть '> Height' и '< 0'");
            return this.Width* (y - 1) + (x - 1);
        }


        public void Replace(Color oldC, Color newC)
        {
            fixed (Color* Pixels = &ColorMap[0,0])
            {
                for (int i = 0; i < this.Width * this.Height; i++)
                {
                    if (Pixels[i] == oldC)
                    {

                        Pixels[i] = newC;
                    }

                }
            }
            
        }


     


        public ColorDelim ColorDelim { get { return ColorDelim.Byte; } }


        public BaseBitmap(string filename)
        {
            if (filename == null || filename == string.Empty)
                throw new ArgumentNullException(nameof(filename));
            using (Stream c = File.OpenRead(filename))
            {
                LoadFromStream(c);
            }                      
        }
        public BaseBitmap(Stream file_rw_stream)
        {          
            LoadFromStream(file_rw_stream);
        }
        public BaseBitmap(byte[] file_rw)
        {
            using (Stream c = new MemoryStream(file_rw))
            {
                LoadFromStream(c);
            }
             
        }


        private void LoadFromStream(Stream strea)
        {
            if (strea == null)
                throw new ArgumentNullException(nameof(strea));
            BitmapEncoderInternal ns = new BitmapEncoderInternal();
            int w;
            int h;
            int pxd;
            Color[,] rezm = ns.LoadInternal(&w, &h, &pxd, strea);
            LoadThis(PixelColorsCounts.Color4, ColorDelim.Byte, w, h, rezm);
        }

        [Obsolete("Опасный метод")]
        internal void LoadThis(PixelColorsCounts ct, ColorDelim pixtype, int w, int h, Color[,] allocatedPixels)
        {
            if (allocatedPixels == null)
                throw new ArgumentNullException(nameof(allocatedPixels));
            if (w == 0)
                throw new ArgumentOutOfRangeException(nameof(w));
            if (h == 0)
                throw new ArgumentOutOfRangeException(nameof(h));
            if (pixtype != ColorDelim.Byte)
            {
                throw new NotSupportedException("кроме байта в пикселе больше ничё не спортится!");
            }


            this.OnePixel = ct;
            this.Width = w;
            this.Height = h;
            this.ColorMap = allocatedPixels;
        }
        [Obsolete("Опасный метод")]
        internal void LoadThis(PixelColorsCounts ct, ColorDelim pixtype, Color[,] allocatedPixels)
        {
            if (allocatedPixels == null)
                throw new ArgumentNullException(nameof(allocatedPixels));
            if (allocatedPixels.GetLength(0) == 0)
                throw new ArgumentOutOfRangeException(nameof(allocatedPixels));
            if (allocatedPixels.GetLength(1) == 0)
                throw new ArgumentOutOfRangeException(nameof(allocatedPixels));
            if (pixtype != ColorDelim.Byte)
            {
                throw new NotSupportedException("кроме байта в пикселе больше ничё не спортится!");
            }


            this.OnePixel = ct;
            this.Width = allocatedPixels.GetLength(0);
            this.Height = allocatedPixels.GetLength(1);
            this.ColorMap = allocatedPixels;
        }

    }
}
