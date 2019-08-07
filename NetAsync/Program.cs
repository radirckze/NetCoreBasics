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

            bool runAsyncAwait = false;
            bool runAsyncBasic = false;
            bool runTestDeadlock = true;

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

            #region async issues / things to remember

            //Deadloocks

            /* Deadlocks can occur when you mix async code with synchronous code (e.g., blocking Task.Wait, Task.Result etc.).
             * What causes the deadlock is how SynchronizationContext are used. When a Task is awaited the current sync-context is captured so 
             * it can be used when the task is ready to be resumed. When the await completes, the application attempts to execute the remaninder
             * of the async method with the captured sync-context. 
             * In some application frameworks e.g., GUI applicatons, the s-context is tied to a thread. For some other, e.g., ASP.NET, the 
             * sync-context is not tied to a thread but allows only one thread to run at a time.
             * In such applications when using async with a blocking call if after the async call the executing thread reaches the blocking call, 
             * then when the async is completed and it attempts to continue with the captured sync-context, it finds the context is associated with a
             * thread that is blocked. This causes a deadlock as the async continuation is waiting for the sync-context to be released by the thread 
             * and the thread is waiting for the Task.Result (from the continuation). 
             */

            if (runTestDeadlock) 
            {
                TestAsyncDeadlock tad = new TestAsyncDeadlock();
                tad.TestDeadlock();
            }
 
            //Error handling

            #endregion async issues

            #region some best practices ...

            //Best practice 1 - async void tasks are useful for event handlers but generally avoid it. E.g., you cannot catch exceptions thrown in task.
            bool runAsyncWaitExceptionTest = false;
            if (runAsyncWaitExceptionTest)
            {
                Console.WriteLine("runAsyncWaitExceptionTest block is starting ...");
                AppDomain.CurrentDomain.UnhandledException += GlobalExceptionHandler;
                int spinFor = 20000;
                try
                {
                    asyncBasic.TestAsyncVoidTaskException(spinFor);
                    Thread.SpinWait(spinFor * 3);
                    Console.WriteLine("After runAsyncWaitExceptionTest called and SpinWait.");
                }
                catch (Exception ex) // exceptions thrown in asyncBasic.TestAsyncVoidTaskException(...) would not get caught here.
                {
                    Console.WriteLine("runAsyncWaitExceptionTest caught an exception. ex.message is {0}", ex.Message);
                }
            }

            //best practice #3 - using Configure Context. 

            //best practice #2 async all the way. 
                //include table 


            #endregion best practices

            if (runAsyncBasic) {
                AsyncBasic.RunSimpleDelayTest(); //synchronous call
                //following is async call but need to block on call as this Main
                //method cannot be async
                AsyncBasic.RunGetContentTestAsync().Wait(); 
            }

            Console.WriteLine("CS Basic stuff completed. Press any key to terminate ...");
            Console.ReadLine();
        }

        public static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Global exception handler caught unhandled exception. Exception is: {0}", e.ExceptionObject.ToString());
            Console.WriteLine("CS Basic stuff forced to exit. Press any key to terminate ...");
            Console.ReadLine();
        }
    }
}
