using System;

namespace EmyEngine
{
    public interface ISwither
    {
        bool Swith { get; }

        event Action<Swither, bool> Swithed;
        event Action<Swither> SwithedToFalse;
        event Action<Swither> SwithedToTrue;

        void Press();
    }
}