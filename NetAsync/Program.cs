using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreBasics.NetAsync
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine("Running CS Basic stuff ...");
           AsyncBasic asyncBasic = new AsyncBasic();

            bool runAsyncAwait = true;
            bool runAsyncBasic = false;
            bool runTestDeadlock = false;

            if (runAsyncAwait)
            {
                Console.WriteLine(@"In runAsyncAwait. Before calling SimpleDelayAsync. The Thread id is: {0}.",
                    Thread.CurrentThread.ManagedThreadId);
                Task<int> sdaTask = asyncBasic.SimpleDelayAsync(300);
                Console.WriteLine(@"In runAsyncAwait. After calling SimpleDelayAsync. The task status is {0}. The Thread id is: {1}", 
                    sdaTask.Status, Thread.CurrentThread.ManagedThreadId);
                Task.WaitAll(sdaTask);
                Console.WriteLine(@"In runAsyncAwait. After WaitAll. The task status is {0}. The Thread id is: {1}", 
                    sdaTask.Status, Thread.CurrentThread.ManagedThreadId);
                
                //Key note: Its the type that is awitable, not the method returning the type. In other words you can
                //await the result *because it returns a task* and not because it is async. As such you can also await
                //thge result of  non-async method that returns a task as shown below
                Console.WriteLine();

                Console.WriteLine(@"In runAsyncAwait. Before calling SimpleDelayTaskFromResult. The Thread id is: {0}.", 
                    Thread.CurrentThread.ManagedThreadId);
                Task<int> sdtTaskFR = asyncBasic.SimpleDelayTaskFromResult(300);
                Console.WriteLine(@"In runAsyncAwait. After calling SimpleDelayTaskFromResult. The task status is {0}. The Thread id is: {1}", 
                    sdtTaskFR.Status, Thread.CurrentThread.ManagedThreadId);
                Task.WaitAll(sdtTaskFR);
                Console.WriteLine(@"In runAsyncAwait. After WaitAll. The task status is {0}. The Thread id is: {1}", 
                    sdtTaskFR.Status, Thread.CurrentThread.ManagedThreadId);

                //Note: async keyword is not required for the method to run asynchronously as seen in results from follolwing example. 
                 Console.WriteLine();

                Console.WriteLine(@"In runAsyncAwait. Before calling SimpleDelayTaskWithRun. The Thread id is: {0}.", 
                    Thread.CurrentThread.ManagedThreadId);
                Task<int> sdtTaskWR = asyncBasic.SimpleDelayTaskWithRun(300);
                Console.WriteLine(@"In runAsyncAwait. After calling SimpleDelayTaskWithRun. The task status is {0}. The Thread id is: {1}", 
                    sdtTaskWR.Status, Thread.CurrentThread.ManagedThreadId);
                Task.WaitAll(sdtTaskWR);
                Console.WriteLine(@"In runAsyncAwait. After WaitAll. The task status is {0}. The Thread id is: {1}", 
                    sdtTaskWR.Status, Thread.CurrentThread.ManagedThreadId);
            }

            if (runAsyncBasic) {
                AsyncBasic.RunSimpleDelayTest(); //synchronous call
                //following is async call but need to block on call as this Main
                //method cannot be async
                AsyncBasic.RunGetContentTestAsync().Wait(); 
            }

            if (runTestDeadlock) {
                TestAsyncDeadlock tad = new TestAsyncDeadlock();
                tad.TestDeadlock();
            }
        }
    }
}
