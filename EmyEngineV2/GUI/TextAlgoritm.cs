using OpenTK.Graphics.OpenGL4;
using EmyEngine.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace EmyEngine.GUI
{
    public class TextAlgoritm
    {



        public static int BaseTextRenderOffsetX(string text1, Font font)
        {
            int renderOffsetX = 0;
            int renderOffsetY = 0;

            for (int i = 0; i < text1.Length; i++)
            {

                if (text1[i] == '\n')
                {
                    renderOffsetX = 0;
                    renderOffsetY += 75;
                    continue;
                }

                {
                    RenderChar rnd = font.CharArray[(ushort)text1[i]];            
                    renderOffsetX += rnd.Widith + 5;

                }

            }
            return renderOffsetX;

        }
        public static void BaseTextRender(string text1, Font font, IGraphics gp)
        {
            if (string.IsNullOrEmpty(text1))
                return;


            int renderOffsetX = 0;
            int renderOffsetY = 0;

            for (int i = 0; i < text1.Length; i++)
            {

                if (text1[i] == '\n')
                {
                    renderOffsetX = 0;
                    renderOffsetY += 75;
                    continue;
                }

                {
                    RenderChar rnd = font.CharArray[(ushort)text1[i]];
                    gp.DefuseMap(rnd.TextureObject);
                    gp.DrawTexturedRectangle(
                        new Vector3((float)(renderOffsetX + 0), (float)(renderOffsetY + 0), 0f),
                        new Vector2(-1.0f, 0.0f),
                        new Vector3((float)(renderOffsetX + 0), (float)(renderOffsetY + rnd.Height), 0f),
                        new Vector2(-1.0f, -1.0f),
                        new Vector3((float)(renderOffsetX + rnd.Widith), (float)(renderOffsetY + 0), 0f),
                        new Vector2(0.0f, 0.0f),
                        new Vector3((float)(renderOffsetX + rnd.Widith), (float)(renderOffsetY + rnd.Height), 0f),
                        new Vector2(0.0f, -1.0f)                               
                        );
            
                    renderOffsetX += rnd.Widith + 5;

                }

            }
        }






    }
}
