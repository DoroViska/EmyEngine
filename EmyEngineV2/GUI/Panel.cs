using EmyEngine.Imaging;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmyEngine.OpenGL;
using EmyEngine.Platform;
using OpenTK;
using OpenTK.Graphics;

namespace EmyEngine.GUI
{
    public class Panel : Widget
    {
        public WidgetCollection Items { private set; get; }
        public bool IsDraweble { set; get; } = true;

        public float MaxY()
        {
            float rez = 0f;
            for(int i = 0;i < this.Items.Count;i++)
                if (this.Items[i].Position.Y > rez)
                {
                    rez = this.Items[i].Position.Y;
                }
            return rez;
        }
        public float MaxX()
        {
            float rez = 0f;
            for (int i = 0; i < this.Items.Count; i++)
                if (this.Items[i].Position.X > rez)
                {
                    rez = this.Items[i].Position.X;
                }
            return rez;
        }


        public Panel()
        {
            Items = new WidgetCollection(this);

        }
    
        public override void Paint(IDrawebleContextSolver context)
        {
            IGraphics gp = context.CreateGraphics();
           
            gp.Push();
            if (IsDraweble)
            {
                gp.Material(Material.Gui);
                gp.DefuseColor(new Color4(0xF6, 0xF6, 0xF6, 0xFF));
                gp.DrawRectangle(this.Position.Vector3(), this.PositionMax.Vector3());
                gp.DefuseColor(Color4.Black);
                gp.DrawSolidRectangle(this.Position.Vector3(), this.PositionMax.Vector3());
            
            }
            gp.Translate(this.Position.Vector3());

            gp.PushClip();
            gp.MultClip(this.DisplayPosition.Vector2() + new Vector2(1, 1), this.DisplayPositionMax.Vector2() - new Vector2(1, 1));
            for (int i = (Items.Count - 1); i >= 0; i--)
            {
                Widget o = Items[i];
                if (!o.IsVisable)
                    continue;            
                o.Paint(context);
            }
           
            gp.PopClip();
            gp.Pop();


        }

        public override void Update(float TimeStep)
        {
            base.Update(TimeStep);
        }
    }
}
