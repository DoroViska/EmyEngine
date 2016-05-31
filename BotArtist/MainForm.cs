using System;
using Eto.Forms;
using Eto.Drawing;
using Eto.Gl;
using EtoApp1.Desktop;

namespace EtoApp1
{


    public class MainForm : Form
    {
        GLSurface sutface;
        TabControl tabControl;
        public MainForm()
        {
            Title = "My Eto Form";
            ClientSize = new Size(400, 350);


            sutface = new GLSurface();
            tabControl = new TabControl();
            tabControl.Pages.Add(new TabPage { Text = "asdasdasdas" ,/*Image = Bitmap.FromResource("EtoApp1.Desktop.proj.png"), */ Content = sutface });
            tabControl.Pages.Add(new TabPage { Text = "asdasdasdas", /*Image = Bitmap.FromResource("EtoApp1.Desktop.proj.png"),*/ Content = new RichTextArea {  } });
            Content = tabControl;
            

            sutface.GLDrawNow += Sutface_GLDrawNow;
            UITimer tm = new UITimer();
            tm.Interval = 1f / 30;
            tm.Start();    
            tm.Elapsed += Tm_Elapsed;


                // create a few commands that can be used for the menu and toolbar
                var clickMe = new Command { /*Image = Bitmap.FromResource("EtoApp1.Desktop.proj.png"), */MenuText = "Click Me!", ToolBarText = "Click Me!" };
            clickMe.Executed += (sender, e) => MessageBox.Show(this, "I was clicked!");
           
            var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();

            var aboutCommand = new Command { MenuText = "About..." };
            aboutCommand.Executed += (sender, e) => { MessageBox.Show(this, "About my app..."); };

            var ccCommand = new Command { MenuText = "asfasf" };
            ccCommand.Executed += (sender, e) => {
                Form cc = new Form();
                cc.Owner = (this);
                cc.Show();
            };
            // [ROOT NAMESPACE] . [PATH ('/' -> '.')] . [FILE NAME]
            //create menu
            Menu = new MenuBar
            {
                Items =
                {

                    new ButtonMenuItem { /*Image = Bitmap.FromResource("EtoApp1.Desktop.proj.png"),*/ Text = "&File", Items = { quitCommand, aboutCommand , ccCommand,   new ButtonMenuItem {
                        Text = "&File",
                         Items = { quitCommand, aboutCommand , ccCommand }
                    } }},
                    new ButtonMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
                    new ButtonMenuItem { Text = "&View", Items = { /* commands/items */ } },

                },
           
            };


           
            ToolBar = new ToolBar { Items = { clickMe, quitCommand, aboutCommand, ccCommand } };
        }

        Random rnd = new Random();
        private void Tm_Elapsed(object sender, EventArgs e)
        {
            if (!sutface.IsInitialized)
                return;
            OpenTK.Graphics.OpenGL4.GL.ClearColor((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble(), 1.0f);
            OpenTK.Graphics.OpenGL4.GL.Clear(OpenTK.Graphics.OpenGL4.ClearBufferMask.ColorBufferBit);
         
            sutface.SwapBuffers();
        }

        private void Sutface_GLDrawNow(object sender, EventArgs e)
        {
          
        }
    }
}