using System;
using System.Threading.Tasks;
using System.Threading;

namespace Threading
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            MyAwaiter ma = new MyAwaiter();
            Console.WriteLine($"Run {Thread.CurrentThread.ManagedThreadId}");
            _ = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine($"SetCompleted {Thread.CurrentThread.ManagedThreadId}");
                ma.SetCompleted();
            });
            await ma;
            Console.WriteLine($"After await {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
