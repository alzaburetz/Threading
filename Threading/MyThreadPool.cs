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
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var handler = new MyEventHandler();

            _actions.Add(handler, action);

            lock(lockObject)
            {
                action();
            }
            return handler;
        }
        public MyThreadPool()
        {
            _actions = new Dictionary<MyEventHandler, Action>();
        }
        private Dictionary<MyEventHandler, Action> _actions;
        private object lockObject { get; } = new object();
    }
}
