using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreBasics.NetAsync {

    //Testing the ASP async programming paradigm
    //See README for some important notes

    public class AsyncBasic {

        // A simple async method that delays so I can control execution time. 
        public async Task<int> SimpleDelayAsync(int millis) 
        {
            Console.WriteLine(@"In SimpleDelayAsync .... The Thread id is: {0}.", Thread.CurrentThread.ManagedThreadId);
            //The await yeilds (that is it returns incomplete task to calling method).
            await Task.Delay(millis); 
            //Execution will resume from this point when the async operation is completed. Expect to see different thread id.
            Console.WriteLine("Exiting SimpleDelayAsync. The Thread id is: {0}.", Thread.CurrentThread.ManagedThreadId);
            return millis;
        }

         public Task<int> SimpleDelayTaskFromResult(int millis) 
         {
            Console.WriteLine("In SimpleDelayTask .... The Thread id is: {0}.", Thread.CurrentThread.ManagedThreadId);
            Task.Delay(millis); 
            Console.WriteLine("Exiting SimpleDelayTask. The Thread id is: {0}.", Thread.CurrentThread.ManagedThreadId);
            return Task.FromResult<int>(millis);
        }

        public Task<int> SimpleDelayTaskWithRun(int millis) {
            Console.WriteLine("In SimpleDelayTask .... The Thread id is: {0}.", Thread.CurrentThread.ManagedThreadId);
            Task<int> tempTask = Task.Run(() => 
            {
                //Since we are creating a task here expect to see it run asynchronously and on a different thread. 
                Console.WriteLine(@"In SimpleDelayTask -> tempTask. Before Delay. The Thread id is: {0}.", Thread.CurrentThread.ManagedThreadId);
                Task.Delay(millis); 
                Console.WriteLine(@"In SimpleDelayTask -> tempTask. After Delay. The Thread id is: {0}.", Thread.CurrentThread.ManagedThreadId);
                return millis; 
            });
            Console.WriteLine("Exiting SimpleDelayTask. The Thread id is: {0}.", Thread.CurrentThread.ManagedThreadId);
            return tempTask;
        }


        //A simple method to illustrate the await call for SimpleDelayAsync
        public async Task<int> TestAsyncSimpleDelay() {
        
            string prefix = "In TestAsyncSimpleDelay ";
            Console.WriteLine(prefix + "before invoking SimpleDelaysAsync");
            Task<int> task = this.SimpleDelayAsync(30);
             Console.WriteLine(prefix + "after invoking SimpleDelaysAsync");

            int sleepFor = 10;
            while (!task.IsCompleted) {
                Console.WriteLine("TestAsyncSimpleDelay is pretending to work " +
                    "while SimpleDelaysAsync delays asynchronously");
                Thread.Sleep(sleepFor);
                
            }

            int  result = await task;
            Console.WriteLine(prefix + "SimpleDelaysAsync returned. Task " +
                "delayed for: {0} milli-seconds", result);
            return result;
        }

        //A simple async taks that acually makes an async call
        public async Task<int> GetContentsAsync(string url) {

            //should do null checks and possible Uri.IsWellFormedUriString(...);

            string prefix = "In GetContentsAsync ";
            HttpClient client = new HttpClient();
            DateTime startTime = DateTime.Now;

            Console.WriteLine(prefix + "Before HttpClient.GetStringAsync call");
            //the following call should return an incomplete task while it waits ...
            await client.GetStringAsync(url); 
            //execution starts here when the async call is completed
            Console.WriteLine(prefix + "After HttpClient.GetStringAsync call");
            DateTime endTime = DateTime.Now;

            int millis = endTime.Subtract(startTime).Milliseconds;

            return millis;
        }

        //A simple method to illustrate the await call for GetContentAsync
        public async Task<int> TestAsyncGetContent() {

            string prefix = "In TestAsyncGetContent ";
            Console.WriteLine(prefix + "Before calling GetContentAsync");
            Task<int> task = this.GetContentsAsync("http://developer.microsoft.com/");
            Console.WriteLine(prefix + "After calling GetContentAsync");
            
            //the task that is returned may or may not be completed.

            int run = 0; 
            while (!task.IsCompleted && run < 3) {
                Console.WriteLine(prefix + "loop {0} (pretending to do something)", 
                    ++run);
            }

            Console.WriteLine(prefix + "The GetContentsAsync task isComplete status is {0}", 
                task.IsCompleted);

            int result = await task;

            Console.WriteLine(prefix + "After await (GetContentsAsync) task ");
            Console.WriteLine(prefix + "The GetContentsAsync task isComplete status is {0}", 
                task.IsCompleted); 
            Console.WriteLine(prefix + "GetStringAsync(url) took {0} millis", result);

            return (int)task.Result;
        }

        public async void TestAsyncVoidTaskException(int spinFor)
        {
            Thread.SpinWait(spinFor);
            throw new ApplicationException("Exception thrown by TestAsyncVoidTaskException task");
        }

        public static void RunSimpleDelayTest() {
            Console.WriteLine("\nRunning simple delay based async test ...");
            AsyncBasic asyncBasic = new AsyncBasic();
            asyncBasic.TestAsyncSimpleDelay().Wait();     

        }

        //This is *not* an async call. The .Wait() will block at this point.
        //Not ideal, but ok for a consle application. See note.  
        public static void  RunSimpleDelayTestAsync() {
            Console.WriteLine("\nRunning simple delay based async test ...");
            AsyncBasic asyncBasic = new AsyncBasic();
            asyncBasic.TestAsyncSimpleDelay().Wait();     

        }

        public static async Task RunGetContentTestAsync() {
            Console.WriteLine("\nRunning GetContentAsync based async test ...");
            AsyncBasic asyncBasic = new AsyncBasic();
            await asyncBasic.TestAsyncGetContent();
        }

    }
}