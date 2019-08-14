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
                //the result of  non-async method that returns a task as shown below
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

                //Error handling 
                Task<int> tteTask = null;
                try
                {
                    tteTask = asyncBasic.TestAsyncException(2000);
                    Task.WaitAll(tteTask); 
                    //Note: WaitAll does not catch/mask any exceptions thrown within the task so we never get to the next line. 
                    //However use while(task.Status == WaitingForActivation OR Running) and we can then hit the line below.
                    Console.WriteLine("The status of the TestTaskException task is {0}", tteTask.Status);
                }
                catch (Exception ex)  
                {
                    Console.WriteLine("Caught exception thrown byTestTaskException task. Task Satus is: {0}. Exception Message is: {1}", tteTask.Status, ex.Message);
                }

            }

            #region async issues / things to remember

            //Deadloocks

            /* See https://devblogs.microsoft.com/pfxteam/await-synchronizationcontext-and-console-apps/ for well written explanation.
             * Deadlocks can occur when you mix async code with synchronous code (e.g., blocking Task.Wait, Task.Result etc.).
             * What causes the deadlock is how SynchronizationContext are used. When a Task is awaited the current sync-context, if any,  is 
             * captured so it can be used when the task is ready to be resumed. When the await completes, the application attempts to execute the
             * remaninder of the async method with the captured sync-context, more specically, posts it to the async-context, if any, to be scheduled 
             * for execution. In some application frameworks e.g., GUI applicatons, the sync-contextt is tied to a thread. For some others, e.g.,  
             * ASP.NET, the sync-context is not tied to a thread but allows only one thread to run at a time.
             * In such applications when using async with a blocking call (say Task.Result), if after the async call the executing thread reaches  
             * the blocking call, the thread/sync-context is blocked. When the async is completed and it attempts to continue with the captured  
             * sync-context it finds sync-context is blocked, waiting for Task.Result. This causes a deadlock as the async continuation is   
             * waiting for the sync-context to schedule it for exacution but the sync-context is blocked waiting for the Task.Result.
             * Console applications do not face this issue as Console applications do not have a sync-context and as such are scheduled by the
             * default task scheduler. 
             */

            // NOTE*: The following code would not deadlock in this console application but will deadlock in an ASP.NET or GUI application.
            bool runTestDeadlock = false;
            if (runTestDeadlock) 
            {
                string tmpUrl = "http://developer.microsoft.com/";
                Task<int> task = TestAsyncDeadlock.GetContentsAsync(tmpUrl);
                int lengh = task.Result;
                Console.WriteLine("The number of characters inpage<{0}> is: {1}", tmpUrl, lengh);
            }

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
                    asyncBasic.TestAsyncVoidTaskException();
                    Thread.SpinWait(spinFor * 3);
                    Console.WriteLine("After runAsyncWaitExceptionTest called and SpinWait.");
                }
                catch (Exception ex) // exceptions thrown in asyncBasic.TestAsyncVoidTaskException(...) would not get caught here.
                {
                    Console.WriteLine("runAsyncWaitExceptionTest caught an exception. ex.message is {0}", ex.Message);
                }
            }

            //best practice #2 async all the way. 


            //best practice #3 - using Configure Context. 
            //Use ConfigureAwait(true) to tell the framework to schedule the continuation on the original context (default). Using false
            //will eliminate the deadlock discussed above as it would indicate that the continuation does not need to be scheduled on the
            //originally captured context.


            #endregion best practices

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
