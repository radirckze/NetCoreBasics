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

        public async void TestAsyncVoidTaskException(int spinFor)
        {
            Thread.SpinWait(spinFor);
            throw new ApplicationException("Exception thrown by TestAsyncVoidTaskException task");
        }


    }
}