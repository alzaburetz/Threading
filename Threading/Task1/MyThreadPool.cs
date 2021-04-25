using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Threading;

namespace Threading
{
    public class MyThreadPool
    {
        public MyEventHandler QueueUserWorkItem(Action action)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));
            var handler = new MyEventHandler(action);
            _actions.Enqueue(handler);
            return handler;
        }
        public MyEventHandler QueueUserWorkItem(Action action, CancellationToken token)
        {
            action = action ?? throw new ArgumentNullException(nameof(action));
            var handler = new MyEventHandler(action, token);
            _actions.Enqueue(handler);
            return handler;
        }
        public MyThreadPool()
        {
            _actions = new Queue<MyEventHandler>();
            _myThread = new Thread(() => Work());
            _myThread.Start();
        }

        private void Work()
        {
            while (true)
            {
                if (_actions.TryDequeue(out var result))
                {
                    if (result.HasCancellactionToken() && result.Token.IsCancellationRequested)
                    {
                        continue;
                    }
                    result.InvokeAction();
                    result.OnFinished?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        private Queue<MyEventHandler> _actions;
        private Thread _myThread;
    }
}
