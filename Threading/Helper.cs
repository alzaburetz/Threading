using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Threading
{
    public static class Helper
    {
        public static Task<TResult> WithCancellation<TResult>(this Task<TResult> task, CancellationToken ct)
        {
            var tcs = new TaskCompletionSource<TResult>();
            ct.Register(() =>
            {
                tcs.SetResult(default(TResult));
            });
            return Task.WhenAny(tcs.Task, task).Unwrap();
        }

        public static Task<TResult[]> WhenAllOrError<TResult>(params Task<TResult>[] tasks)
        {
            return WhenAllOrErrorImplementation(tasks);
        }

        static Task<TResult[]> WhenAllOrErrorImplementation<TResult>(Task<TResult>[] tasks, CancellationToken token = default(CancellationToken))
        {
            var tcs = new TaskCompletionSource<TResult[]>();
            var result = new TResult[tasks.Length];

            var newTasks = tasks
                .Select((task, index) => task.ContinueWith((t) =>
                {
                    if (t.IsFaulted)
                    {
                        tcs.TrySetResult(result);
                        return default;
                    }
                    result[index] = t.Result;
                    return t.Result;
                }));

            return Task.WhenAny(Task.WhenAll(newTasks), tcs.Task).Unwrap();
        }
    }
}
