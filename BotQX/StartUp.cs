using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gtk;

namespace BotQX
{
    public static class StartUp
    {


        public static void MessageBox(string Msg,MessageType type, Gtk.Window parent)
        {
            MessageDialog md = new MessageDialog(parent, DialogFlags.Modal, type, ButtonsType.Ok, Msg);
            md.WindowPosition = WindowPosition.Center;            
            md.Run();
            md.Destroy();
        }
        public static void MessageBox(string Msg, MessageType type)
        {
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, type, ButtonsType.Ok, Msg);
            md.WindowPosition = WindowPosition.Center;
            md.Run();
            md.Destroy();
        }


        static void Main(string[] args)
        {
            Application.Init();


            Gtk.IconFactory w1 = new Gtk.IconFactory();
            w1.Add("proj", new Gtk.IconSet(Gdk.Pixbuf.LoadFromResource("BotQX.Images.proj.png")));
            w1.Add("help", new Gtk.IconSet(Gdk.Pixbuf.LoadFromResource("BotQX.Images.help.png")));
            w1.Add("About", new Gtk.IconSet(Gdk.Pixbuf.LoadFromResource("BotQX.Images.About-icon.png")));
            w1.Add("editor", new Gtk.IconSet(Gdk.Pixbuf.LoadFromResource("BotQX.Images.editor_pencil_pen_edit_write_-512.png")));
            w1.Add("sett", new Gtk.IconSet(Gdk.Pixbuf.LoadFromResource("BotQX.Images.gear.png")));
            w1.Add("page", new Gtk.IconSet(Gdk.Pixbuf.LoadFromResource("BotQX.Images.pages-1-xxl.png")));
            w1.Add("display", new Gtk.IconSet(Gdk.Pixbuf.LoadFromResource("BotQX.Images.display_9515.png")));
            w1.Add("iconsrer", new Gtk.IconSet(Gdk.Pixbuf.LoadFromResource("BotQX.Images.secimg.png")));
            w1.Add("start", new Gtk.IconSet(Gdk.Pixbuf.LoadFromResource("BotQX.Images.start.png")));
            w1.Add("stop", new Gtk.IconSet(Gdk.Pixbuf.LoadFromResource("BotQX.Images.stop.png")));
            w1.Add("pause", new Gtk.IconSet(Gdk.Pixbuf.LoadFromResource("BotQX.Images.pause.png")));
            w1.AddDefault();

            GLib.ExceptionManager.UnhandledException += ExceptionManager_UnhandledException;
            MainWindow s = new MainWindow();
            s.ShowAll();

            Application.Run();
          

        }

        private static void ExceptionManager_UnhandledException(GLib.UnhandledExceptionArgs args)
        {
            throw (Exception)args.ExceptionObject;
        }
    }
}
