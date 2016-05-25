using EmyEngine.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmyEngine.OpenGL;
using OpenTK.Input;

namespace EmyEngine.GUI
{
  
    public class GameUI : Widget
    {

        class BasicResolver : IDrawebleContextSolver
        {
            public BasicResolver(GameUI instance)
            {
                this_t = instance;
            }
            GameUI this_t;

            public float ZCounter
            {
                set; get;
            }

            public int GetHeight()
            {
                return this_t.Height;
            }

            public int GetWidth()
            {
                return this_t.Width;
            }

            public bool IsDraweble()
            {
                return true;
            }

            public IGraphics CreateGraphics()
            {
                return G.Graphics;
            }
        }
        public IDrawebleContextSolver DrawebleSolver { private set; get; }
    
        public WidgetCollection Items { private set; get; } 

        public GameUI()
        {
            Items = new WidgetCollection(this);
            DrawebleSolver = new BasicResolver(this);
        }



        public static bool CursorCollusion(Point c1Min, Point c1Max, Point c2Min, Point c2Max)
        {
            if ((c1Max.X >= c2Min.X && c1Min.X <= c2Max.X) == false) return false;
            if ((c1Max.Y >= c2Min.Y && c1Min.Y <= c2Max.Y) == false) return false;
            return true;
        }

        public void Draw()
        {

            DrawebleSolver.ZCounter = 0f;
            for (int i = 0; i < Items.Count; i++)
            {
                Widget o = Items[i];
                if (!o.IsVisable) continue;

                o.Paint(DrawebleSolver);
            }
            this.Paint(this.DrawebleSolver);
        }
        public Widget Selected { set; get; }

     
        public void ReversUpdate()
        {
            ReversUpdate(this.Items,new Point());
        }
        public void ReversUpdate(WidgetCollection collection,Point startPoint)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                Widget o = collection[i];
                o.SetFullPosition(startPoint + o.Position);
                o.Update(0.1f);
                if (o is Panel)
                {
                    ReversUpdate(((Panel)o).Items, startPoint + o.Position);
                }
            }     

        }


        
        public void Process(Point fullpoint, int window_width,int window_height,int cur_x,int cur_y, bool cur_statel,/*byte* key_sate,*/ Widget parent = null,List<Widget> Items = null)
        {
            if (parent == null)
                parent = this;
            if (Items == null)
                Items = this.Items;
            
            parent.Width = window_width;
            parent.Height = window_height;

            fullpoint += parent.Position;


            for (int i = 0; i < Items.Count; i++)
            {
                Widget o = Items[i];
             
                o.CursorPosition = new Point(cur_x,cur_y);
                if (!o.IsVisable)
                    continue;
        
              

                if (
                    !CursorCollusion(fullpoint + o.Position, fullpoint + o.PositionMax, new Point(cur_x, cur_y), new Point(cur_x, cur_y))
                    ||
                    !CursorCollusion(fullpoint, new Point(parent.Width,parent.Height) + fullpoint, new Point(cur_x, cur_y), new Point(cur_x, cur_y))
                    )
                {
                    if (o.IsDraged == true)
                        o.OnEndMove();
                    o.IsDraged = false;

                    if (o.IsPushed == true && !cur_statel)
                    {                       
                        o.IsPushed = false;
                        o.OnEndPush();                       
                    }
                    else
                        continue;              
                }
                                                     

                if (o.IsDraged == false)
                    o.OnMove();
                o.IsDraged = true;

                if (cur_statel)
                {
                    if (o.IsPushed == false)
                    {
                        o.OnStartPush();
                        o.UI.Selected = o;
                    }
                        
                    o.IsPushed = true;
                }
                else
                {
                    if (o.IsPushed == true)
                        o.OnEndPush();
                    o.IsPushed = false;
                }



                if (o is Panel)
                {
                    this.Process(fullpoint, o.Width, o.Height,  cur_x,  cur_y, cur_statel, /*key_sate,*/o,((Panel)o).Items);


                }



            }


        }



        public override void Paint(IDrawebleContextSolver context)
        {
            IGraphics gp = context.CreateGraphics();
            gp.Push();
         

            gp.Pop();
        }

        public override void Update(float TimeStep)
        {
            
        }
    }
}
