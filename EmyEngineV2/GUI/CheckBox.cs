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
    public class CheckBox : Widget
    {
        public CheckBox()
        {
            this.Width = 20;
            this.Height = 20;
            this.Click += CheckBox_Click;


        }
        public event UIEventArgs ValueChanged;
        private void CheckBox_Click(object sender, EventArgs e)
        {
            Checked = !Checked;
            if (ValueChanged != null)
                ValueChanged(this,EventArgs.Empty);
        }

        public bool Checked { set; get; } = false;

        public string Text { set; get; }

        public float FontSize { set; get; } = 0.3f;
        public override void Paint(IDrawebleContextSolver context)
        {

            IGraphics gp = context.CreateGraphics();


            gp.Push();
            gp.Translate(new Point(0, 1).Vector3());
            gp.Material(Material.Gui);
            gp.DefuseColor(Color4.White);
            gp.DrawRectangle(this.Position.Vector3(), this.PositionMax.Vector3());
            gp.DefuseColor(new Color4(0xDC, 0xDC, 0xDC, 0xFF));
            gp.DrawSolidRectangle(this.Position.Vector3(), this.PositionMax.Vector3());

            gp.LineWidth = 3f;
            if (Checked == true)
            {
                gp.DefuseColor(Color4.Black);
                gp.DrawLine((this.Position + new Point(5, 5)).Vector3(), (this.PositionMax + new Point(-5, -5)).Vector3());
                gp.DrawLine((this.Position + new Point(this.Width - 5, 5)).Vector3(), (this.PositionMax - new Point(this.Width - 5, 5)).Vector3());
            }

            gp.DrawText(Text, this.Position + new Point(32,0),new Color4(0x66, 0x66, 0x66, 0xFF), EE.СurrentFont, FontSize);



            gp.LineWidth = 1f;
            gp.Pop();
        }
    }
}
