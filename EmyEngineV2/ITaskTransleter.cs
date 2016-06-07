using System;

namespace EmyEngine
{
    public interface ITaskTransleter
    {
        void Process();
        void PushTask(Action<object> tsk, object args);
        void Wait();
    }
}