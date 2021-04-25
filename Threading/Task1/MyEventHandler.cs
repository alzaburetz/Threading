using System;
using System.Collections.Generic;
using System.Text;

namespace Threading
{
    public class MyEventHandler
    {
        public Action<object, EventArgs> OnFinished;
        public MyEventHandler(Action action)
        {
            _action = action;
        }
        public void InvokeAction() => _action.Invoke();
        private readonly Action _action;
    }
}
