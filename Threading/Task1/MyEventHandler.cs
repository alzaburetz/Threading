using System;
using System.Threading;

namespace Threading
{
    public class MyEventHandler
    {
        public Action<object, EventArgs> OnFinished;
        public MyEventHandler(Action action)
        {
            _action = action;
        }
        public MyEventHandler(Action action, CancellationToken token)
        {
            _action = action;
            _token = token;
        }
        public void InvokeAction() => _action.Invoke();
        public bool HasCancellactionToken() => _token == CancellationToken.None;
        public CancellationToken Token => _token;
        private readonly Action _action;
        private readonly CancellationToken _token;
    }
}
