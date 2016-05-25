using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

#if SYSTEM_DRAWING
// SYSTEM_DRAWING
#else 
using EmyEngine.Platform.SDL2;
#endif

namespace EmyEngine.Platform
{
    public unsafe class BitmapEncoderInternal
    {
        public Color[,] LoadInternal(int* w,int* h,int* pixeldepth,Stream file)
        {
            if(file == null)
                throw new ArgumentNullException();
            if (w == null || h == null || pixeldepth == null)
                throw  new ArgumentNullException();



#if SYSTEM_DRAWING


            using (Bitmap bmp = new Bitmap(file))
            {
                BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Color[,] rz_mp = new Color[bmp.Width, bmp.Height];
                *pixeldepth = 4;
                *w = bmp.Width;
                *h = bmp.Height;
                int scan0size = bmp.Width * bmp.Height;
                for (int display = 0; display < (scan0size); display++)
                {
                    UniColor4 cur = ((UniColor4*)bmp_data.Scan0.ToPointer())[display];
                    fixed (Color* p = &rz_mp[0, 0])
                    {
                        Color* pix = p + display;
                        pix->A = cur.A;
                        pix->R = cur.B;
                        pix->G = cur.G;
                        pix->B = cur.R;
                    }
                }

                bmp.UnlockBits(bmp_data);
                return rz_mp;
            }
             

#else 
            byte[] rw = new byte[file.Length];
            file.Read(rw, 0, rw.Length);
            SDL_RWops* pops = null;
            SDL_Surface* sf = null;
            fixed (byte* rws = &rw[0])
                pops = SDL.SDL_RWFromMem(rws, rw.Length);
            if (pops == null)
            {
                throw new FunctionReturnedNullException("SDL_RWFromMem");
            }
        
            sf = SDL.IMG_Load_RW(pops, 1);
            if (sf == null)
            {
                SDL.SDL_FreeRW(pops);
                throw new FunctionReturnedNullException("IMG_Load_RW");
            }
            try
            {
            
                Color[,] rz_mp = new Color[sf->w, sf->h];
                *pixeldepth = sf->format->BytesPerPixel;
                *w = sf->w;
                *h = sf->h;
                int scan0size = sf->h * sf->w;
                if (*pixeldepth == 1)
                    for (int display = 0; display < (scan0size); display++)
                    {
                        UniColor1 cur = ((UniColor1*)sf->pixels)[display];
                        fixed (Color* p = &rz_mp[0, 0])
                        {
                            Color* pix = p + display;
                            pix->A = 255;
                            pix->R = cur.C;
                            pix->G = cur.C;
                            pix->B = cur.C;
                        }
                    }
                if (*pixeldepth == 3)
                    for (int display = 0; display < (scan0size); display++)
                    {
                        UniColor3 cur = ((UniColor3*)sf->pixels)[display];
                        fixed (Color* p = &rz_mp[0, 0])
                        {
                            Color* pix = p + display;
                            pix->A = 255;
                            pix->R = cur.R;
                            pix->G = cur.G;
                            pix->B = cur.B;
                        }
                    }
                if (*pixeldepth == 4)
                    for (int display = 0; display < (scan0size); display++)
                    {
                        UniColor4 cur = ((UniColor4*)sf->pixels)[display];
                        fixed (Color* p = &rz_mp[0, 0])
                        {
                            Color* pix = p + display;
                            pix->A = cur.A;
                            pix->R = cur.R;
                            pix->G = cur.G;
                            pix->B = cur.B;
                        }
                    }
                SDL.SDL_FreeSurface(sf);
                SDL.SDL_FreeRW(pops);
                return rz_mp;
            }
            catch (Exception r)
            {
                SDL.SDL_FreeSurface(sf);
                SDL.SDL_FreeRW(pops);
                throw r;
            }
   
#endif
        }

    }
}
