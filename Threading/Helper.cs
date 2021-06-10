using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Threading
{
    public static class Helper
    {
        public static async Task<TResult> WithCancellation<TResult>(this Task<TResult> task, CancellationToken ct)
        {
            var tcs = new TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously);
            ct.Register(() => tcs.TrySetCanceled(), useSynchronizationContext: false);
            var cancellableTask = await Task.WhenAny(task, tcs.Task);

            if (cancellableTask == tcs.Task)
            {
                task.ContinueWith((result) => task.Exception.Flatten(),
                    TaskContinuationOptions.OnlyOnFaulted |
                    TaskContinuationOptions.ExecuteSynchronously);
            }
            return cancellableTask.Result;
        }
    }
}
