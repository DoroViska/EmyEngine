using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using BotQX.BotQ;
using Gdk;
using Gtk;

using EmyEngine;
using EmyEngine.Game;
using Jitter.LinearMath;
using OpenTK.Input;
using SdkGame;
using FrameEventArgs = OpenTK.FrameEventArgs;

namespace BotQX
{
    public class MainWindow : Gtk.Window
    {
        private Gtk.Toolbar _upMenu = null;
        private Gtk.VBox _mainBox = null;
        private Gtk.Action _projectAction = null;
        private Gtk.Action _projectEx = null;
        private Gtk.Action _projectHelp = null;
        private Gtk.Action _projectEditor = null;
        private Gtk.Action _projectSettings = null;
        private Gtk.Action _projectAbout = null;
        private Gtk.Notebook _tabpage = null;
        private Gtk.VBox _screenPage = null;    
        private OpenGlWidget _mainGl = null;
        private EventController _loops = null;
        //private BotPage _examplPage = null;
        private Mission _mission = null;
        //private Gtk.TextView _logPage = null;

        private void Build()
        {
            this.Icon = new Pixbuf(Assembly.GetExecutingAssembly(), "BotQX.Images.proj.png");
            this.WidthRequest = 800;
            this.HeightRequest = 600;
            this.DeleteEvent += OnDeleteEvent;
            this.Title = "BotQX *[project0]";
            this.WindowPosition = Gtk.WindowPosition.Center;

            _mainBox = new Gtk.VBox();
            this.Child = (_mainBox);
            {
                //_upMenu
                _upMenu = new Gtk.Toolbar();
                _upMenu.IconSize = Gtk.IconSize.LargeToolbar;
                _mainBox.PackStart(_upMenu, false, false, 0);
                {
                    bool useIcons = true;
                    //
                    //_projectAction                
                    _projectAction = new Gtk.Action("Проект", "Проект",null, useIcons ? "proj" : "");
                    _upMenu.Add(_projectAction.CreateToolItem());
                    //
                    //_projectEx                
                    _projectEx = new Gtk.Action("Задание", "Задание", null, useIcons ? "page" : "");
                    _upMenu.Add(_projectEx.CreateToolItem());
                    //
                    //_projectHelp                
                    _projectHelp = new Gtk.Action("Помошь", "Помошь", null, useIcons ? "help" : "");
                    _upMenu.Add(_projectHelp.CreateToolItem());
                    //
                    //_projectEditor                
                    _projectEditor = new Gtk.Action("Редактор", "Редактор", null, useIcons ? "editor" : "");
                    _upMenu.Add(_projectEditor.CreateToolItem());             
                    //
                    //_projectSettings                
                    _projectSettings = new Gtk.Action("Настройки", "Настройки", null, useIcons ? "sett" : "");
                    _upMenu.Add(_projectSettings.CreateToolItem());
                    //
                    //_projectAbout                
                    _projectAbout = new Gtk.Action("О программе", "О программе", null, useIcons ? "About" : "");
                    _upMenu.Add(_projectAbout.CreateToolItem());
                }

                //
                //_tabpage
                _tabpage = new Notebook();
                Gtk.Frame temp = new Gtk.Frame("<b>Активность</b>");
                ((Gtk.Label) temp.LabelWidget).UseMarkup = true;
                temp.Shadow = ShadowType.None;               
                temp.Child = _tabpage;
                _mainBox.PackStart(temp, true, true, 2);
                {
                    //
                    //_screenPage
                    _screenPage = new VBox();
                    _tabpage.AppendPage(_screenPage,new TabPageHeader("", new Pixbuf(Assembly.GetExecutingAssembly(), "BotQX.Images.display_9515.png"),false));
                    {
                        //
                        //_mainGl
                        _mainGl = new OpenGlWidget();
                        _loops = new EventController(100,30);
                        _mainGl.Initialized += (a, t) =>
                        {                           
                            GLib.Idle.Add(() =>
                            {
                                _loops.Update(null);
                                return true;
                            });
                            _loops.LoadFraems += LoopsOnLoadFraems;
                            _loops.RenderFraems += LoopsOnRenderFraems;
                            _loops.UpdateFraems += LoopsOnUpdateFraems;  

                        };                       
                        _screenPage.PackStart(_mainGl,true,true,1);
                    }
                    ////
                    ////_customPage
                    //_customPage = new VBox();
                    //_tabpage.AppendPage(_customPage, new TabPageHeader("[Bot №1] Ждёт", new Pixbuf(Assembly.GetExecutingAssembly(), "BotQX.Images.Industry-Robot-icon.png"), true));
                    //{
                    //    //
                    //    //_examplPage
                    //    _examplPage = new BotPage();
                    //    _customPage.Add(_examplPage);
                    //}
                    //
                    //_logPage
                    //_logPage = new TextView();
                    //ScrolledWindow temp0 = new ScrolledWindow();
                    //temp0.Child = _logPage;
                    //_tabpage.AppendPage(temp0, new TabPageHeader("Логи", new Pixbuf(Assembly.GetExecutingAssembly(), "BotQX.Images.Industry-Robot-icon.png"), false));
                }




            }
      



        }


        private GameApplication _appl = null;


        private void LoopsOnLoadFraems(object sender, FrameEventArgs frameEventArgs)
        {       
            _appl = new GameApplication();
            _appl.Initialize();
            MapLoader.AddType(typeof(WoodBlockObject));
            MapLoader.AddType(typeof(MetalBlockObject));
            MapLoader.AddType(typeof(BotObject));

            this._mission = new Mission();
            this._mission.Bots.Add(new BotPageFromMission());

            using (Stream map = File.OpenRead("./new.bqm/map.eem"))        
            using (Stream mission = File.OpenRead("./new.bqm/mission.eem"))
            {
                LoadMission(map,mission);
            }

        }




        public void ResetMission()
        {

            

        }




        public void SaveMission(Stream map, Stream mission)
        {
            MapLoader.Save(map,_appl.InstanceFromGame);

            XmlSerializer serial_ata = new XmlSerializer(typeof(Mission));
            serial_ata.Serialize(mission, this._mission);

        }
        public void LoadMission(Stream map,Stream mission)
        {
            MapLoader.Load(map, this._appl.InstanceFromGame);
            XmlSerializer serial_ata = new XmlSerializer(typeof(Mission));      
            this._mission = (Mission)serial_ata.Deserialize(mission);



            int i = 0;
            foreach (BotPageFromMission newbot in this._mission.Bots)
            {
                BotPage pageNew = new BotPage();
                _tabpage.AppendPage(pageNew, new TabPageHeader("[Bot №" + (++i) + "]", new Pixbuf(Assembly.GetExecutingAssembly(), "BotQX.Images.Industry-Robot-icon.png"), true));
                pageNew.Bot = new BotObject();
                pageNew.Bot.Position = newbot.StartPosition;
                this._appl.InstanceFromGame.AddObject(pageNew.Bot);
            }






















        }









        private void LoopsOnRenderFraems(object sender, FrameEventArgs frameEventArgs)
        {
            int x, y;
            _mainGl.GetPointer(out x, out y);
            _appl.Render(_mainGl.Width, _mainGl.Height, x, y, Mouse.GetState().IsButtonDown(MouseButton.Left) && this.IsActive);
     
        }
        private void LoopsOnUpdateFraems(object sender, FrameEventArgs frameEventArgs)
        {
            _appl.Update(100);
        }

        
        


        public MainWindow() : base(Gtk.WindowType.Toplevel)
        {
            Build();
        }

 










        private void OnDeleteEvent(object o, Gtk.DeleteEventArgs args)
        {
            Gtk.Application.Quit();
            args.RetVal = true;
        }

      
        
    }
}
