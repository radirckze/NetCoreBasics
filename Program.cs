using NetCoreBasics.Concurrency;
using NetCoreBasics.NetAsync;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running CS Entended stuff ...");

            bool runSynchedCache = false;
            bool runAsyncBasic = false;
            bool runTestDeadlock = false;

            if (runSynchedCache) {
                SynchedCache.TestSynchedCache();
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
