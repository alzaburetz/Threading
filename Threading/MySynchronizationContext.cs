using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using System.Threading;

namespace Threading
{
    public class MySynchronizationContext
    {
        private Thread _thread;
        private Queue<KeyValuePair<WaitCallback, Object>> _actions;

        public MySynchronizationContext()
        {
            _actions = new Queue<KeyValuePair<WaitCallback, Object>>();
            _thread = new Thread(Work);
            _thread.Start();
        }

        private void Work()
        {
            while (true)
            {
                if (_actions.TryDequeue(out var action))
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                    action.Key(action.Value);
                }
            }
        }

        public virtual void Send(SendOrPostCallback d, Object state)
        {
            d(state);
        }

        public virtual void Post(SendOrPostCallback d, Object state)
        {
            _actions.Enqueue(new KeyValuePair<WaitCallback, object>(new WaitCallback(d), state));
        }
    }
}
