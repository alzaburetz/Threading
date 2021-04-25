using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Threading
{
    public class ParallelDelivery
    {
        public void Deliver()
        {
            var deliver1 = Task.Factory.StartNew(() =>
            {
                Task.Delay(1000);
                Console.WriteLine("I delivered 1000 units");
            }, TaskCreationOptions.DenyChildAttach);

            var deliver2 = Task.Factory.StartNew(() =>
            {
                Task.Delay(1500);
                Console.WriteLine("I delivered 1500 to couriers");
            }, TaskCreationOptions.RunContinuationsAsynchronously);

            deliver2 = deliver2.ContinueWith(obj =>
            {
                var b1 = Task.Factory.StartNew(() =>
                {
                    Task.Delay(500);
                    Console.WriteLine("Deliver B1 delivered 500 units");
                });
                var b2 = Task.Factory.StartNew(() =>
                {

                    Task.Delay(600);
                    Console.WriteLine("Deliver B2 delivered 600 units");
                });

                Task.WaitAll(b2, b1);
                Console.WriteLine("My delivers completetd tasks");
            });

            Task.WaitAll(deliver1, deliver2);
            Console.WriteLine("Work is done");
        }
    }
}
