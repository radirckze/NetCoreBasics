using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CSExtended.NetAsync {

    /* Testing out the case described in the article below which I cannot reproduce.
     * http://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
      * This WILL deadlonk when run in ASP (see article above and Ravi D comment)
     */

    public class TestAsyncDeadlock {

        //A simple async taks that acually makes an async call
        public static async Task<int> GetContentsAsync(string url) {

            string retStr = "";
            HttpClient client = new HttpClient();
            retStr = await client.GetStringAsync(url); 
            //await Task.Delay(2000);

            return retStr.Length;
        }

        public void TestDeadlock() {

            Task<int> task = TestAsyncDeadlock.GetContentsAsync("http://developer.microsoft.com/");
            int lengh = task.Result;
            Console.WriteLine("The string lenght is {0}", lengh);
        }

    }

}