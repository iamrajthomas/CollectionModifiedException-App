using System;
using System.Diagnostics;

namespace CollectionModifiedException_App
{
    class Program
    {
        public const int LoopLimit = 10000;
        static void Main(string[] args)
        {
            Console.WriteLine("START OF APPLICATION");
            var LoopCount = 0;

            // Approach - 1
            // Invoke Startup Invoker for Once
            //Startup.Invoker();

            //=====================================================================================================================================================

            // Approach - 2
            // Invoke Startup Invoker for n number of times using a loop
            var stopWatchForLoop = Stopwatch.StartNew();
            for (int counter = 1; counter <= LoopLimit; counter++)
            {
                Console.WriteLine("Startup.Invoker() : {0}", counter);
                LoopCount++;
                Startup.Invoker();
            }
            stopWatchForLoop.Stop();
            Console.WriteLine("StopWatch => ElapsedMilliseconds: {0}, ElapsedTicks: {1} ", stopWatchForLoop.ElapsedMilliseconds, stopWatchForLoop.ElapsedTicks);

            //=====================================================================================================================================================

            // Approach - 3
            // Invoke Startup Invoker for n number of times using a Tasks.Parallel.For loop
            //var stopWatchForParallel = Stopwatch.StartNew();
            //System.Threading.Tasks.Parallel.For(0, LoopLimit, parallelcounter =>
            //{
            //    Console.WriteLine("Startup.Invoker() : {0}", parallelcounter);
            //    LoopCount++;
            //    Startup.Invoker();
            //});
            //stopWatchForParallel.Stop();
            //Console.WriteLine("StopWatch => ElapsedMilliseconds: {0}, ElapsedTicks: {1} ", stopWatchForParallel.ElapsedMilliseconds, stopWatchForParallel.ElapsedTicks);

            //=====================================================================================================================================================

            Console.WriteLine("Loop Total Count: {0}", LoopCount);
            Console.WriteLine("END OF APPLICATION");
            Console.ReadKey();
        }
    }



}
