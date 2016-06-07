using System;
using System.Collections.Generic;
using Eto;
using Eto.Forms;
using Eto.Drawing;
using Eto.Serialization.Xaml;
using System.Reflection;

namespace BotArtist
{
    public class StartForm : Form
    {
        public StartForm()
        {
            
            XamlReader.Load(this);

            this.Closed += StartForm_Closed;

            this.Content = Bitmap.FromResource("BotArtist.Images.BotArtist.png");
            this.ClientSize = Bitmap.FromResource("BotArtist.Images.BotArtist.png").Size;
            this.Resizable = false;
            this.Maximizable = false;
            this.Minimizable = false;
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            this.Menu = new MenuBar
            {
                Items =
                {
                    new ButtonMenuItem {
                        Text = "&Фаил",
                        Items = {
                            new ButtonMenuItem { Command = new Command((a , y)=> {
                               WindowTextView s = new WindowTextView();
                                s.Owner = this;
                                s.Show();

                            }), Text = "&Новая песочница" },
                            new ButtonMenuItem { Text = "&Открыть песочницу" },
                            new SeparatorMenuItem(),
                            new ButtonMenuItem { Text = "&Начать прохождение" },
                            new ButtonMenuItem { Text = "&Продолжить прохождение" },
                            new ButtonMenuItem { Text = "&Правка" },
                            new SeparatorMenuItem(),
                            new ButtonMenuItem { Command = new Command((a , y)=> {
                                Program.aplet.Quit();

                            }), Text = "&Выход" },
                        }


                    },
                    new ButtonMenuItem { Text = "&Правка", Items = { /* commands/items */ } },
                    new ButtonMenuItem { Text = "&Справка", Items = { /* commands/items */ } },
                },
             
            }; //WindowTextView
            this.Title = "BotArtist v" + version.ToString();
        }

        private void StartForm_Closed(object sender, EventArgs e)
        {
            
        }
    }
}
