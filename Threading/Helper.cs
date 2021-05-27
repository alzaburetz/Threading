using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Threading
{
    public static class Helper
    {
        static async Task<TResult> WithCancellation<TResult>(this Task<TResult> task, CancellationToken ct)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            ct.Register(_ => tcs.TrySetCanceled(), null, false);
            Task resultTask = await Task.WhenAny(task, tcs.Task);
            return resultTask;
        }
    }
}
