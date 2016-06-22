using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmyEngine
{
    /// <summary>
    /// Таймер компенсирующий потеренное время тем, что вызывает процедуру
    /// столько раз, сколько было потеренно
    /// </summary>
    public class TimeCompensator
    {
        Stopwatch _watch = new Stopwatch();
        double _elapsedGlobl;


        public event EventHandler OnTick;
        public double UpdateRate { set; get; }

        public void Process()
        {
            double fs = _watch.ElapsedMilliseconds - _elapsedGlobl;

            for (int i = 0; i < (int)(fs / UpdateRate); i++)
            {
                if (i == 0)
                    _elapsedGlobl = _watch.ElapsedMilliseconds;
                if (OnTick != null)
                    OnTick(this, EventArgs.Empty);
            }
        }

        public void Run(double ms)
        {
            if (ms < 0)
                throw new ArgumentOutOfRangeException(nameof(ms));
            UpdateRate = ms;
            _elapsedGlobl = _watch.ElapsedMilliseconds;
            _watch.Start();
        }

        public void Reset()
        {
            _watch.Restart();
            _elapsedGlobl = _watch.ElapsedMilliseconds;
        }
    }
}
