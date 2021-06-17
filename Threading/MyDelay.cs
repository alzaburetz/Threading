using System;
using System.Timers;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Threading
{
    public static class MyDelayClass
    {
        public static Task MyDelay(int delay, CancellationToken ct = default(CancellationToken))
        {
            var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var timer = new System.Timers.Timer(delay);
            timer.Elapsed += (sender, args) =>
            {
                tcs.SetResult(true);
                (sender as System.Timers.Timer).Dispose();
            };
            timer.Start();
            return tcs.Task;
        }
    }
}
