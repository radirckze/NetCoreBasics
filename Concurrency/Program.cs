using System;

namespace NetCoreBasics.Concurrency
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running SynchedCache ...");

            SynchedCache.TestSynchedCache();

            //Note: SynchedCache.TestSynchedCache() is setup to exit on its own.

            // Console.WriteLine("SynchedCache completed. Enter any key to exit: ");
            // Console.ReadLine();
        }
    }
}
