using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    public class TaskLooper
    {
        public int Max;
        public Action A;
        public Task Task => _tcs.Task;
        public void Run(int delay = 1000)
        {
            if (_currentCount == Max)
            {
                _tcs.SetResult(null);
                return;
            }

            _currentCount++;
            var task = Task.Delay(delay);
            task = task.ContinueWith((res) => A(), TaskContinuationOptions.ExecuteSynchronously);
            task = task.ContinueWith((res) => Run(), TaskContinuationOptions.ExecuteSynchronously);
        }
        private readonly TaskCompletionSource<object> _tcs = new TaskCompletionSource<object>();
        private int _currentCount;
    }
}
