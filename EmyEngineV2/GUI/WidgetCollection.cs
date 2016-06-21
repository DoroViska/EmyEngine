using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmyEngine.GUI
{
    public class WidgetCollection : List<Widget>
    {
        public WidgetCollection(Widget UI)
        {
            this.UI = UI;
        }

        public Widget UI { private set; get; }

        public new void Add(Widget e)
        {
            base.Add(e);
            e.Parent = UI;

        }

        public new void Remove(Widget e)
        {
            base.Remove(e);
            e.Parent = null;

        }
    }
}
