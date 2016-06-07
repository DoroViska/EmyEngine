using System;
using Eto;
using Eto.Forms;
using Eto.Gl;
using Eto.Gl.Windows;
using OpenTK;


namespace BotArtist
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Platform cur = null;
            Toolkit.Init(new ToolkitOptions
            {
                Backend = PlatformBackend.PreferNative
            });
            bool platfomselected = true;
            for (int i = 0;i < args.Length;i++)
            {

                if (args[i] == "-p")
                {
                    switch (args[i + 1])
                    {
                        case "w":
                            platfomselected = false;
                            cur = new Eto.WinForms.Platform();
                            cur.Add<GLSurface.IHandler>(() => new Eto.Gl.Windows.WinGLSurfaceHandler());
                            break;
                        case "l":
                            cur = new Eto.GtkSharp.Platform();
                            cur.Add<GLSurface.IHandler>(() => new Eto.Gl.Gtk.GtkGlSurfaceHandler());
                            platfomselected = false;
                            break;
                        case "m":
                            throw new Exception("MacOS Not Supported");
                            platfomselected = false;
                        case "au":
                            cur = new Eto.GtkSharp.Platform();
                            cur.Add<GLSurface.IHandler>(() => new Eto.Gl.Gtk.GtkGlSurfaceHandler());
                            platfomselected = false;
                            break;
                    }
                }
            }
            if(platfomselected)
                if (Configuration.RunningOnWindows)
                {
                    cur = new Eto.WinForms.Platform();
                    cur.Add<GLSurface.IHandler>(() => new Eto.Gl.Windows.WinGLSurfaceHandler());
                }
                else if (Configuration.RunningOnLinux)
                {
                    cur = new Eto.GtkSharp.Platform();
                    cur.Add<GLSurface.IHandler>(() => new Eto.Gl.Gtk.GtkGlSurfaceHandler());
                }
                else if (Configuration.RunningOnMacOS)
                {
                    throw new Exception("MacOS Not Supported");
                }
                else if (Configuration.RunningOnUnix)
                {
                    cur = new Eto.GtkSharp.Platform();
                    cur.Add<GLSurface.IHandler>(() => new Eto.Gl.Gtk.GtkGlSurfaceHandler());
                }
            
            aplet = new Application(cur);
            

            StartForm fm = new StartForm();
    
         
            aplet.Run(fm);
           


        }


        public static Application aplet = null;
    }
}
