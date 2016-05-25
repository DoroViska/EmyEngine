using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Gtk;

namespace BotQX
{
    public class TabPageHeader : Gtk.HBox
    {
        private Gtk.Image _image = null;
        private Gtk.Label _lable = null;
        private Gtk.Button _closeButton = null;

        public Image Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
            }
        }

        public Label Lable
        {
            get
            {
                return _lable;
            }

            set
            {
                _lable = value;
            }
        }

        private void Build()
        {         
            //
            //_image
            _image = new Image();
            this.PackStart(_image,true, true, 0);
            //
            //_lable
            _lable = new Label();
            _lable.UseMarkup = true;
            this.PackStart(_lable, false, false, 2);
            //
            //_closeButton
            _closeButton = new Button();
            _closeButton.Image = new Gtk.Image(new Gdk.Pixbuf(Assembly.GetExecutingAssembly(),"BotQX.Images.close-icon.png"));
            _closeButton.Relief = ReliefStyle.None;
            this.PackStart(_closeButton, false, false, 2);
            //this
            this.ShowAll();
        }


        public TabPageHeader(string str, Gdk.Pixbuf buffers,bool buttonVisable) : this()
        {
          
            _lable.LabelProp = str;
            _image.Pixbuf = buffers;
            _closeButton.Visible = buttonVisable;

        }
        public TabPageHeader() : base()
        {
            Build();
     
        }


    }
}
