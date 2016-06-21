using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmyEngine
{
    public class Swither : ISwither
    {
        public event Action<Swither> SwithedToTrue;
        public event Action<Swither> SwithedToFalse;
        public event Action<Swither,bool> Swithed;

        public Swither(bool Swith = true)
        {
            this.Swith = Swith;
        }
        public bool Swith { private set; get; }

        public void Press()
        {
            this.Swith = !this.Swith;
            if (Swithed != null) Swithed(this, this.Swith);
            if (this.Swith)
                if (SwithedToTrue != null) SwithedToTrue(this);
            else
                if (SwithedToFalse != null) SwithedToFalse(this);
        }
    }
}
