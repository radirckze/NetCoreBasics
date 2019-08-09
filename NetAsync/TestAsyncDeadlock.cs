using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreBasics.NetAsync {

    public class TestAsyncDeadlock {

        //A simple async taks that acually makes an async call
        public static async Task<int> GetContentsAsync(string url) {

            string retStr = "";
            HttpClient client = new HttpClient();
            retStr = await client.GetStringAsync(url); 
            //await Task.Delay(2000);

            return retStr.Length;
        }

    }

}