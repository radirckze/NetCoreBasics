using CSExtended.CodeChallenge;
using CSExtended.Concurrency;
using CSExtended.NetAsync;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSExtended
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running CS Entended stuff ...");

            bool runSynchedCache = false;
            bool runAsyncBasic = false;
            bool runTestDeadlock = false;
            bool runFiveProblems = false;
            

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

           if (runFiveProblems) {
               FiveProblems fiveProblems = new FiveProblems();
               List<int> values = new List<int>() {5, 9, 50, 1, 2, 56};
               int result = fiveProblems.SumNumbersInListFor(values); 
               Console.WriteLine("Sum of 5, 9, 50, 1, 2, 56 using for {0}", result);
               result = fiveProblems.SumNumbersInListWhile(values); 
               Console.WriteLine("Sum of 5, 9, 50, 1, 2, 56 using while {0}", result);
               result = fiveProblems.SumNumbersInListRecursive(values, 0); 
               Console.WriteLine("Sum of 5, 9, 50, 1, 2, 56 using recursive {0}", result);

               List<char> charList1 = new List<char>() {'a', 'b', 'c'};
               List<char> charList2 = new List<char>() {'1', '2', '3'};
               List<char> combinedList = fiveProblems.CombineTwoLists(charList1, charList2);
               Console.WriteLine("The combined list for 1,2 3, and a,b,c is: ");
               foreach(char ch in combinedList) {
                   Console.Write(ch +", ");
               }
               Console.WriteLine();

               result = fiveProblems.SumFibNumbers(10);
               Console.WriteLine("The sum of the first 10 fibonacci #'s is {0}", result);

               string resultStr = fiveProblems.ArrangeToFromLargestNumber(values);
               Console.WriteLine("the resuling string for 5, 9, 50, 1, 2, 56 is {0}", resultStr);
           }

            // run Max SumRectangle
            //MaxSumRectangle msr = new MaxSumRectangle();
            //msr.RunTestCases();

            //run Job Sequence
            //JobSequencing jobSequencing = new JobSequencing();
            //jobSequencing.TestJobSequencing();

            //Count Palindrome substrings
            // PalindromeSubstrings paliSubstrings = new PalindromeSubstrings();
            //paliSubstrings.TestPaliSubstring();

            //Traveling salesman problem
            //TravelingSalesman travelingSalesman = new TravelingSalesman();
            //travelingSalesman.TestTravelingSalesman();

            //Find median in stream
            //MedianInStream medianInStream = new MedianInStream();
            //medianInStream.TestMedianInStream();

            //The N-Queen problem
            //NQueen nQueen= new NQueen();
            //nQueen.TestNQueen();

            // WordBreak2 problem
            //WordBreak2 wordBreak2 = new WordBreak2();
            //wordBreak2.TestWordBreak2();

            //LinkedList loop test
            LinkedListLoop linkedListLoop = new LinkedListLoop();
            linkedListLoop.TestLinkedListLoop();


        }
    }
}
