using System;
using System.Collections.Generic;
using System.Text;

namespace Threading
{
    public class MyEventHandler
    {
        public event EventHandler OnFinished;
        public virtual void OnFinishedCallback(EventArgs args)
        {
            EventHandler handler = OnFinished;
            if (handler != null)
            {
                handler(this, args);
            }
        }
    }
}
