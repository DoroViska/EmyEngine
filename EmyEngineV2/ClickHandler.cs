using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmyEngine
{
    //XD typedef
    public class ClickHandlerObject : ClickHandler<object> {}
    public class ClickHandler<T>
    {
        public event Action<T,bool> Press;
        protected virtual void OnPress(T arg1, bool arg2)
        {
            Press?.Invoke(arg1, arg2);
        }

        public bool IsPressed { set; get; }
        public T Args { set; get; }
        public void ButtonState(bool btnp)
        {
     
            if (btnp && !IsPressed)
            {
                OnPress(Args,true);
                IsPressed = true;
            }

            if (!btnp && IsPressed)
            {
                OnPress(Args, false);
                IsPressed = false;
            }
        }
    }
}
