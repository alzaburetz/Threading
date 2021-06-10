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
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            while (!task.IsCompleted)
            {
                await Task.Delay(30);
                ct.ThrowIfCancellationRequested();
            }

            return await task;
        }

        public static Task<TResult[]> WhenAllOrError<TResult>(params Task<TResult>[] tasks)
        {
            return WhenAllOrErrorImplementation(tasks);
        }

        static Task<TResult[]> WhenAllOrErrorImplementation<TResult>(Task<TResult>[] tasks, CancellationToken token = default(CancellationToken))
        {
            var tcs = new TaskCompletionSource<TResult[]>();
            var result = new TResult[tasks.Length];
            var pending = result.Length;
            for (var i = 0; i < tasks.Length; i++)
            {
                var ti = i;
                tasks[i].ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        throw new Exception(); //???????
                    }
                    result[ti] = t.Result;
                    if (0 == Interlocked.Decrement(ref pending))
                        tcs.SetResult(result);
                }, CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default);
            }

            return tcs.Task;
        }
    }
}
