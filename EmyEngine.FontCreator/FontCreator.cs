

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;

namespace EmyEngine.FontCreator
{
    public class FontCreator
    {




        public static void CreateTexChar(char буква)
        {
            Bitmap bmp = new Bitmap(100, 100);
            using (Graphics gp = Graphics.FromImage(bmp))
            {
                gp.TextRenderingHint = TextRenderingHint.AntiAlias;
                
                Font drawFont = new Font("Arial", 50);
               
             
                SolidBrush drawBrush = new SolidBrush(Color.White);

                // Create point for upper-left corner of drawing.
                PointF drawPoint = new PointF(0f, 0f);
                gp.DrawString(буква.ToString(), drawFont, drawBrush, drawPoint);
                gp.Save();
            }
           



            int MinX = bmp.Width - 1;


            int MaxX = 0;
           

            for (int y = 0; y < bmp.Height; y++)          
                for (int x = 0; x < bmp.Width; x++)
                    if (bmp.GetPixel(x, y) != Color.FromArgb(0,0,0,0))
                    {
                        if (x < MinX)
                            MinX = x;

                        if (x > MaxX)
                            MaxX = x;
                    }


            Console.WriteLine("{0},{1},{2},{3}", MinX, 0, MaxX, bmp.Height);

             Bitmap bt = Reflate(MinX, 0, MaxX, bmp.Height - 1, bmp);
             if (bt != null)
                bt.Save("./Arial_50/" + Convert.ToInt32(буква) + ".png", ImageFormat.Png);


        }

        public static Bitmap Reflate(int x1, int y1, int x2, int y2, Bitmap img_toReflate)
        {        
            try
            {
                int width = x2 - x1 + 1;
                int height = y2 - y1 + 1;
                Console.WriteLine($"{width},{height}");
                var result = new Bitmap(width + 1, height);

                for (int i = x1; i <= x2; i++)
                    for (int j = y1; j <= y2; j++)
                        result.SetPixel(i - x1, j - y1, img_toReflate.GetPixel(i, j));
                return result;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString());
              
            }


            return null;
        }

        public static void CreateAlphabet()
        {

            for (int x = 0; x < 255; x++)
            {

                CreateTexChar((char)x);
            }

            for (int x = 1040; x < 1104; x++)
            {

                CreateTexChar((char)x);
            }

        }




    }

}
