using System;

namespace Threading
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var threadPool = new MyThreadPool();

            var handle1 = threadPool.QueueUserWorkItem(() => Console.WriteLine("Hello"));
            handle1.OnFinished += (o, a) => Console.WriteLine("1 Done");
            var handle2 = threadPool.QueueUserWorkItem(() => Console.WriteLine(" "));
            handle2.OnFinished += (o, a) => Console.WriteLine("2 Done");
            var handle3 = threadPool.QueueUserWorkItem(() => Console.WriteLine("World"));
            handle3.OnFinished += (o, a) => Console.WriteLine("3 Done");
        }
    }
}
