using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.CompilerServices;

namespace Threading
{
    internal class MyAwaiter : INotifyCompletion, ICriticalNotifyCompletion
    {
        public void OnCompleted(Action continuation)
        {
            
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            
        }

        public void SetCompleted()
        {
            if (_isFinished)
            {
                return;
            }
            _isFinished = true;
            _onCompleted?.Invoke();
        }

        public void GetResult() { }

        public MyAwaiter GetAwaiter()
        {
            return this;
        }

        public bool IsCompleted => true;
        private Action _onCompleted;
        private bool _isFinished;
    }
}
