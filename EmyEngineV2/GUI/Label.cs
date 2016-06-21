using EmyEngine.OpenGL;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine.GUI
{
    public class Label : Widget
    {
        public Label()
        {

        }


        private string _text;
        public string Text
        {
           set {
                _text = value;
                AutoSize();
            }
           get {
                return _text;
            }
        }

        public float FontSize { set; get; } = 0.3f;

        public void AutoSize()
        {
            this.Height = (int)(75f * FontSize);
            this.Width = (int)(((float)TextAlgoritm.BaseTextRenderOffsetX(Text, EE.СurrentFont)) * FontSize + 5f);

        }

        public override void Paint(IDrawebleContextSolver context)
        {

            IGraphics gp = context.CreateGraphics();


            gp.Push();
            gp.Move(new Point(0, 1).Vector3());
            gp.Material(Material.Gui);
            gp.DefuseColor(Color4.White);
            
            gp.DefuseColor(new Color4(0xDC, 0xDC, 0xDC, 0xFF));


            gp.LineWidth = 3f;
     
            gp.DrawText(Text, this.Position , new Color4(0x66, 0x66, 0x66, 0xFF), EE.СurrentFont, FontSize);

            gp.LineWidth = 1f;
            gp.Pop();
        }
    }
}
