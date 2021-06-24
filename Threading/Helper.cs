using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
            var result = new List<TResult>(tasks.Length);

            for (var i = 0; i < tasks.Length - 1; i++)
            {
                tasks[i] = tasks[i].ContinueWith((t) => 
                {
                    if (t.IsFaulted)
                    {
                        tcs.SetResult(result.ToArray());
                        return default;
                    }
                    result.Add(t.Result);
                    return t.Result;
                }, token);
            }

            return Task.WhenAny(Task.WhenAll(tasks), tcs.Task).Unwrap();
        }
    }
}
