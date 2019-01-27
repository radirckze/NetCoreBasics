using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSExtended.Concurrency {

    //A simple example of a cache of 2 integer values such that val2 = 2*val1. 
    //The cache supports multiple readers but a single writer. 
    //Note***: the .NET ReaderWriterLockSlim class *currently* has a write-favored
    //policy so this can lead to reader starvation.  
    public class SynchedCache {

        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

        private int[] cachedValues = new int[2] {1,2};

        public int[] Read() {
            cacheLock.EnterReadLock();
            try {
                return new int[] {cachedValues[0], cachedValues[1]};
            }
            finally {
                cacheLock.ExitReadLock();
            }
        }

        public void Update(int v1, int v2) {
            if (v2 != 2*v1) {
                throw new ArgumentException();
            }
            cacheLock.EnterWriteLock();
            try {
                cachedValues[0] = v1;
                cachedValues[1] = v2;
            }
            finally {
                cacheLock.ExitWriteLock();
            }
        }

        public static void TestSynchedCache() {

            SynchedCache synchedCache = new SynchedCache();

            //Lets run the program for 3 seconds with 3 readers reading every .5 seconds
            //and 5 writers updating it every 1/100th of a second


            for(int i=0; i<3; i++) {
                Task.Run(() => {
                    while (true) {
                        int[] values = synchedCache.Read();
                        Console.WriteLine("Task {0} read: Val1={1}, Val2={2}",
                            Environment.CurrentManagedThreadId, values[0], values[1]);
                        Thread.Sleep(500);
                    }
                });
            }

            for(int i=0; i<5; i++) {
                Task.Run(() => {
                    int j = i+1;
                    while (true) {
                        synchedCache.Update(j, j*2);
                        j++;
                        Thread.Sleep(100);
                    }
                });
            }


            Thread.Sleep(3000);
            Environment.Exit(0);
        }
 
    }
}

