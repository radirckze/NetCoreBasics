using System;

namespace NetCoreBasics.NetAsync
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine("Running CS Basic stuff ...");

            bool runAsyncBasic = true;
            bool runTestDeadlock = false;

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
