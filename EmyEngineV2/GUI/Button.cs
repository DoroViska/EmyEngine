using EmyEngine.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmyEngine.OpenGL;
using EmyEngine.Platform;
using OpenTK.Graphics;

namespace EmyEngine.GUI
{

    public class Button : Widget
    {
        public string Text { set; get; }

        public float FontSize { set; get; } = 0.3f;

        public bool Padded { set; get; } = false;

        public Button()
        {
        }

        public void AutoSize()
        {
            this.Height = (int)(75f * FontSize);
            this.Width = (int)(((float)TextAlgoritm.BaseTextRenderOffsetX(Text, EE.СurrentFont)) * FontSize + 5f);
        }

        Point addd = new Point(3, 0);


        public override void Paint(IDrawebleContextSolver context)
        {
            IGraphics gp = context.CreateGraphics();
            if (IsMouseDown == true)
            {
                gp.Material(Material.Gui);
                gp.Push();
                gp.Translate(new Point(0,1).Vector3());
                gp.LineWidth = 1f;
                gp.DefuseColor(Color4.Aqua);
                gp.DrawRectangle(this.Position.Vector3(), this.PositionMax.Vector3());
                gp.DefuseColor(new Color4(0xDC, 0xDC, 0xDC, 0xFF));
                gp.DrawSolidRectangle(this.Position.Vector3(), this.PositionMax.Vector3());
                gp.DefuseColor(Color4.White);
                gp.DrawLine((this.Position + new Point(1, 1)).Vector3(), (this.Position + new Point(1 + this.Width - 2, 1)).Vector3());
                gp.DrawText(this.Text, this.Position + addd, new Color4(0x66, 0x66, 0x66,0xFF), EE.СurrentFont, FontSize);
                gp.LineWidth = 1f;
                gp.Pop();

                return;
            }


            if (!this.IsMouseMove)
            {
                gp.Material(Material.Gui);
                gp.Push();
                gp.Translate(new Point(0, 1).Vector3());
                gp.LineWidth = 1f;
                gp.DefuseColor(new Color4(0xF6, 0xF6, 0xF6, 0xFF));

                if (!Padded)
                {
                    gp.DrawRectangle(this.Position.Vector3(), this.PositionMax.Vector3());

                    gp.DefuseColor(new Color4(0xDC, 0xDC, 0xDC, 0xFF));
                    gp.DrawSolidRectangle(this.Position.Vector3(), this.PositionMax.Vector3());

                    gp.DefuseColor(Color4.White);
                    gp.DrawLine((this.Position + new Point(1, 1)).Vector3(), (this.Position + new Point(1 + this.Width - 2, 1)).Vector3());
                }

              
                gp.DrawText(this.Text, this.Position + addd, new Color4(0x66, 0x66, 0x66, 0xFF), EE.СurrentFont, FontSize);
                gp.LineWidth = 1f;
                gp.Pop();
                //gp.Push();
                //gp.LineWidth = 1f;
                //gp.DrawRectangle(this.Position, this.PositionMax, Color.Bytes(0xF6, 0xF6, 0xF6, 0xFF));

                //gp.DrawSolidRectangle(this.Position, this.PositionMax, Color.Bytes(0xDC, 0xDC, 0xDC, 0xFF));
                //gp.DrawLine(this.Position + new Point(1,1), this.Position + new Point(1 + this.Width - 2, 1),Color.White);
                //gp.DrawText(this.Text, this.Position + addd, Color.Bytes(0x66, 0x66, 0x66), GameEngine.CurrentFont, FontSize);
                //gp.LineWidth = 1f;
                //gp.Pop();
            }
            else
            {
                gp.Material(Material.Gui);
                gp.Push();
                gp.Translate(new Point(0, 1).Vector3());
                gp.LineWidth = 1f;
                gp.DefuseColor(Color4.White);
                gp.DrawRectangle(this.Position.Vector3(), this.PositionMax.Vector3());
                gp.DefuseColor(new Color4(0xDC, 0xDC, 0xDC, 0xFF));
                gp.DrawSolidRectangle(this.Position.Vector3(), this.PositionMax.Vector3());
                gp.DefuseColor(Color4.White);
                gp.DrawLine((this.Position + new Point(1, 1)).Vector3(), (this.Position + new Point(1 + this.Width - 2, 1)).Vector3());
                gp.DrawText(this.Text, this.Position + addd, new Color4(0x66, 0x66, 0x66, 0xFF), EE.СurrentFont, FontSize);
                gp.LineWidth = 1f;
                gp.Pop();
                //gp.Push();
                //gp.LineWidth = 1f;
                //gp.DrawRectangle(this.Position, this.PositionMax, Color.White);

                //gp.DrawSolidRectangle(this.Position, this.PositionMax, Color.Bytes(0xDC, 0xDC, 0xDC,0xFF));

                //gp.DrawText(this.Text, this.Position + addd, Color.Bytes(0x66, 0x66, 0x66, 0xFF), GameEngine.CurrentFont, FontSize);
                //gp.LineWidth = 1f;
                //gp.Pop();
            }
         
        }

        public override void Update(float TimeStep)
        {
            base.Update(TimeStep);
        }
    }
}
