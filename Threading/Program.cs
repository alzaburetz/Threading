using System;
using System.Threading;

namespace Threading
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var looper = new TaskLooper()
            {
                A = () => Console.WriteLine($"Hello from {Thread.CurrentThread.ManagedThreadId}"),
                Max = 5
            };

            looper.Run();
            looper.Task.Wait();
        }
    }
}
