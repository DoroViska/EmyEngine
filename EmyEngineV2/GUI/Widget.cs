using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmyEngine.OpenGL;

namespace EmyEngine.GUI
{

    public delegate void UIEventArgs(object sender,EventArgs e);

    public abstract class  Widget
    {
   

        public string Name { set; get; } = nameof(Widget);

        #region PRIVATE LAYOUT

        #endregion
        public Aligment Anchor { set; get; } = Aligment.Left | Aligment.Top;
        public virtual void Resize(int rW, int rH)
        {

            if (this.Anchor == Aligment.Center)
            {
                this.Position = new Point((this.Parent.Width / 2) - (this.Width / 2), (this.Parent.Height / 2) - (this.Height / 2));           
                return;
            }


            bool rithout = false;
            if (((uint)(Anchor & Aligment.Left) == (uint)Aligment.Left) && ((uint)(Anchor & Aligment.Right) == (uint)Aligment.Right))
            {

                this.Width += rW;
                rithout = true;
            }
            if (((uint)(Anchor & Aligment.Top) == (uint)Aligment.Top) && ((uint)(Anchor & Aligment.Down) == (uint)Aligment.Down))
            {

                this.Height += rH;
                rithout = true;
            }
            if (rithout == true)
                return;

            if ((uint)(Anchor & Aligment.Right) == (uint)Aligment.Right)
            {
                X = (this.Position.X + rW);

            }
            if ((uint)(Anchor & Aligment.Down) == (uint)Aligment.Down)
            {
                Y = (this.Position.Y + rH);

            }
        }

        #region STATIC LAYOUT
        public static GameUI ThisToUI(Widget c)
        {
            if (c is GameUI)
                return (GameUI)c;
            return ThisToUI(c.Parent);
        }
        #endregion

        public GameUI UI {
            get { return ThisToUI(this); }
        }

        public bool IsVisable { set; get; } = true;

        public bool IsMouseDown { set; get; } = false;
        public bool IsMouseMove{ set; get; } = false;

        public event UIEventArgs Click;
        public virtual void OnClik() { if (Click != null) Click(this,EventArgs.Empty); }
        public event UIEventArgs MouseMove;
        public virtual void OnMouseMove() { if (MouseMove != null) MouseMove(this, EventArgs.Empty); }
        public event UIEventArgs MouseLeave;
        public virtual void OnMouseLeave() { if (MouseLeave != null) MouseLeave(this, EventArgs.Empty); }
        public event UIEventArgs MouseDown;
        public virtual void OnMouseDown() { if (MouseDown != null) MouseDown(this, EventArgs.Empty); }
        public event UIEventArgs MouseUp;
        public virtual void OnMouseUp() { OnClik(); if (MouseUp != null) MouseUp(this, EventArgs.Empty); }



        public Widget Parent { set; get; } = null;
        public int OldWidth { set; get; } = 0;
        public int OldHeight { set; get; } = 0;
        public int Width { set; get; } = 100;
        public int Height { set; get; } = 100;
        public int X { set; get; } = 0;
        public int Y { set; get; } = 0;

        private Point _displayPosition;
        public void SetFullPosition(Point t)
        {
            _displayPosition = t;
        }
        public Point DisplayPosition
        {
            get { return _displayPosition; }
        }
        public Point DisplayPositionMax
        {
            get
            {
                return new Point(this._displayPosition.X + Width, this._displayPosition.Y + Height);
            }
        }

        public Point Position
        {
            set { this.X = value.X;  this.Y = value.Y; }
            get { return new Point(this.X,this.Y); }
        }
        public Point PositionMax
        {
            get
            {
                return new Point(this.Position.X + Width, this.Position.Y + Height);
            }
        }

        public Point CursorPosition { set; get; }

        public abstract void Paint(IDrawebleContextSolver context);
        public virtual void Update(float TimeStep) {

            if (OldWidth == 0)
                OldWidth = Parent.Width;
            if (OldHeight == 0)
                OldHeight = Parent.Height;

            if (OldWidth != Parent.Width)
            {
                
    
                this.Resize(Parent.Width - OldWidth, 0);
                
                OldWidth = Parent.Width;
            }
            if (OldHeight != Parent.Height)
            {
                
                   
                    this.Resize(0, Parent.Height - OldHeight);
                
                OldHeight = Parent.Height;
            }
        }


    }
}
