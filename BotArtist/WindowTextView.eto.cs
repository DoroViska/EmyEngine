using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;

namespace BotArtist
{
    partial class WindowTextView : Form
    {
        void InitializeComponent()
        {
            Title = "My Form";
            Resizable = false;
            Maximizable = false;
            DynamicLayout l = new DynamicLayout();
            DynamicLayout l1 = new DynamicLayout();
            DynamicLayout l2 = new DynamicLayout();
            Content = l;
            l.BeginVertical();
            l.Add(l1, true, true);
            l.Add(l2, true, true);

            l1.BeginHorizontal();
            l1.AddCentered(new Button { Text = "Вперед" });
            l1.AddCentered(new Button { Text = "Если" });
            l1.AddCentered(new Button { Text = "Пока" });
            l1.AddCentered(new Button { Text = "Назад" });
            l1.AddCentered(new Button { Text = "Поворт" });
            l1.AddCentered(new Button { Text = "Делай" });
            l1.AddCentered(new Button { Text = "Конец" });
            l1.AddCentered(new Panel {});


            l2.BeginHorizontal();
            l2.AddCentered(new Button { Text = "Вперед" });
            l2.AddCentered(new Button { Text = "Если" });
            l2.AddCentered(new Button { Text = "Пока" });
            l2.AddCentered(new Button { Text = "Назад" });
            l2.AddCentered(new Button { Text = "Поворт" });
            l2.AddCentered(new Button { Text = "Делай" });
            l2.AddCentered(new Button { Text = "Конец" });
            l2.AddCentered(new Panel { });


        }
    }
}
