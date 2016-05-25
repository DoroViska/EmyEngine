using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BotQX.BotQ;
using EmyEngine.Game;
using Gtk;
using Mono.TextEditor;
using Mono.TextEditor.Highlighting;
using NiL.JS;
using System.Threading;
using Mono.TextEditor.Utils;
using NiL.JS.BaseLibrary;
using NiL.JS.Core;

namespace BotQX
{
    public class BotPage : VBox
    {
        public BotObject Bot
        {
            get
            {
                return _bot;
            }

            set
            {
                _bot = value;
            }
        }

        public TextEditor TextEditor
        {
            get
            {
                return _textEditor;
            }

            set
            {
                _textEditor = value;
            }
        }

        private BotObject _bot = null;
        private Mono.TextEditor.TextEditor _textEditor = null;
        private Gtk.TextView _buildBundles = null;
        private Toolbar _toolbar = null;
        private Gtk.Action _startAction = null;
        private Gtk.Action _stopAction = null;
        private Gtk.Action _pauseAction = null;
        private Thread _workThread = null;

        private void Build()
        {
            //
            //_toolbar
            _toolbar = new Toolbar();
            _toolbar.IconSize = IconSize.SmallToolbar;           
            this.PackStart(_toolbar,false,false,0);
            {
                //
                //_stopAction
                _stopAction = new Gtk.Action("Стоп", "Стоп",null,"stop");
                _stopAction.Activated += StopActionOnActivated; 
                _toolbar.Add(_stopAction.CreateToolItem());
                //
                //_startAction
                _startAction = new Gtk.Action("Запуск", "Запуск", null, "start");
                _startAction.Activated += StartActionOnActivated;
                _toolbar.Add(_startAction.CreateToolItem());
                //_pauseAction
                _pauseAction = new Gtk.Action("Пауза", "Пауза", null, "pause");
                _toolbar.Add(_pauseAction.CreateToolItem());  
            }
            //
            //_textEditor
            _textEditor = new Mono.TextEditor.TextEditor();
            _textEditor.Text = "function Main()\n{\n    Rotate();\nRotate();\nRotate();\nwhile(true){Move();Rotate();}}";
            _textEditor.Document.SyntaxMode = SyntaxMode.Read(Assembly.GetExecutingAssembly().GetManifestResourceStream("BotQX.JsSyntax.xml"));
            ScrolledWindow temp = new ScrolledWindow();
            temp.ShadowType = ShadowType.In;
            temp.Child = _textEditor;
            this.PackStart(temp, true, true, 0);
            //
            //_buildBundles
            _buildBundles = new TextView();
            _buildBundles.Editable = false;
            _buildBundles.HeightRequest = 100;  
 
            ScrolledWindow temp1 = new ScrolledWindow();
            temp1.Child = _buildBundles;

            Frame temp2 = new Frame();
            temp2.Label = "<b>Ошибки:</b>";
            temp2.Shadow = ShadowType.None;
             ((Label)temp2.LabelWidget).UseMarkup = true;
            temp2.Child = temp1;
          

            this.PackStart(temp2, false, false, 0);         
            //this
            this.ShowAll();
        }


        public void AppendTextToErrorViewer(string text,byte r,byte g,byte b)
        {
            TextTag tag = new TextTag(null);
            this._buildBundles.Buffer.TagTable.Add(tag);
            tag.ForegroundGdk = new Gdk.Color(r,g,b);
            var iter = this._buildBundles.Buffer.EndIter;
            this._buildBundles.Buffer.InsertWithTags(ref iter, text, tag);
        }

        public void BundlesError(string error)
        {
            AppendTextToErrorViewer("Syntax Error   ", 100, 100, 100);
            AppendTextToErrorViewer("[BotQ Compiller]   ", 0, 0, 0);
            AppendTextToErrorViewer(error + "\n", 255, 0, 0);
        }
        public void BundlesLog(string log)
        {
            AppendTextToErrorViewer("Syntax Error   ", 100, 100, 100);
            AppendTextToErrorViewer("[BotQ Compiller]   ", 0, 0, 0);
            AppendTextToErrorViewer(log + "\n", 0, 0, 255);
        }



        private void StartActionOnActivated(object sender, EventArgs eventArgs)
        {
            if (this._bot == null)
            {
                StartUp.MessageBox("не миссия, не задание не загружены в текушем контексте", MessageType.Error, this.TooltipWindow);
                return;                
            }



            _buildBundles.Buffer.Clear();
            _bot.MarkersClear();
            Script ns = null;
            try
            {
                ns = new Script(this._textEditor.Text);
            }
            catch (JSException ex)
            {
                BundlesError(ex.Message); 
                return;
            }
            catch (Exception ex)
            {
                BundlesError(ex.Message);
                return;
            }
           
            JSRuntime.JSPutsAlgorirm(ns.Context, this);

            //если ошибка то сюда дойти не должно

            AppendActionSensetives(true);
            _workThread = new Thread(() =>
            {              
                try
                {
                    (ns.Context.GetVariable("Main").Value as NiL.JS.BaseLibrary.Function).Invoke(null);
                }
                catch (NullReferenceException ex)
                {
                    Application.Invoke((a, t) =>
                    {
                        BundlesError("Отсутствует входная процедура __jscall _Main@0 : void");
                    });
                }
                catch (JSException ex)
                {
                    Application.Invoke((a, t) => { BundlesError(ex.Message); });

                }
                catch (ThreadAbortException )
                {
                   return;
                }
                catch (Exception ex)
                {
                    Application.Invoke((a, t) => { BundlesError(ex.Message); });
                }


                Application.Invoke((a, t) => { AppendActionSensetives(false); });
            });
            _workThread.IsBackground = true;
            _workThread.Start();

        }

        private void StopActionOnActivated(object sender, EventArgs eventArgs)
        {
         
            BundlesLog("Остановка работы потока.");
            _workThread.Abort();
            AppendActionSensetives(false);
        }


     


        private void AppendActionSensetives(bool internalt)
        {
            _startAction.Sensitive = !internalt;
            _stopAction.Sensitive = internalt;
            _pauseAction.Sensitive = internalt;
        }


        public BotPage() : base()
        {
            Build();
            AppendActionSensetives(false);
        }



    }
}
