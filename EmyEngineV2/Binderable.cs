using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmyEngine
{


    public abstract class Bindable : IBinderable
    {


        public Binder Bind()
        {
            return new Binder(this);
        }



        public abstract void Use();

        public abstract void UnUsed();


    }


}
