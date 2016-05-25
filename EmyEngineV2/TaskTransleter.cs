using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmyEngine
{
    public class TaskTransleter
    {
        public TaskTransleter()
        {
            _Tasks = new Stack<Action<object>>(1000);
            _Argss = new Stack<object>(1000);
        }

        private Stack<Action<object>> _Tasks;
        private Stack<object> _Argss;
        private object _lockator = new object();
        private object _wait = new object();
        private bool _isProcessed = false;
        public void PushTask(Action<object> tsk, object args)
        {
            lock (_lockator)
            {
                _isProcessed = true;
                _Tasks.Push(tsk);
                _Argss.Push(args);
            }
        }


        public void Wait()
        {
            while (true)         
                lock (_wait)              
                    if(!_isProcessed)
                        break;
        }

        public void Process()
        {
            Action<object> tsk = null;
            object args = null;
            lock (_lockator)
            {
                if (_Tasks.Count != 0)
                {
                    tsk = _Tasks.Pop();
                    args = _Argss.Pop();
                }
                else
                {
                    lock (_wait)
                        _isProcessed = false;
                    return;
                }                    
            }
            tsk(args);
           

        }
    }

}
