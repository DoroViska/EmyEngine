using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmyEngine.GUI
{
    public class DragDropPanel : Panel
    {
        public DragDropPanel()
        {
            this.MouseDown += MainForm_StartPush;
        }
        private Point FormStartPosition = new Point();
        private Point CursorStartPosition = new Point();
       

        private void MainForm_StartPush(object sender, EventArgs e)
        {
            CursorStartPosition = this.CursorPosition;
            FormStartPosition = this.Position;
        }

        public override void Update(float TimeStep)
        {
            if (this.IsMouseDown)
            {
                Point t = this.CursorPosition - CursorStartPosition;
                this.Position = FormStartPosition + t;
                return;
            }

            base.Update(TimeStep);
        }
    }
}
