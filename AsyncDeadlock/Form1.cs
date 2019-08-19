using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncDeadlock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_AsyncDeadlock_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Calling async method and blocking on result (should deadlock after this message)", "Async Blocking Clicked!");
            string tmpUrl = "http://developer.microsoft.com/";
            Task<int> task = GetContentsAsync(tmpUrl);
            int length = task.Result;
            MessageBox.Show(String.Format("The number of bytes on {0} is {1}", tmpUrl, length), "Async Blocking");
        }

        private void Btn_ConfigureWait_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Calling async method with ConfigureAwait (should not deadlock)", "Async ConfigureAwait Clicked!");
            string tmpUrl = "http://developer.microsoft.com/";
            Task<int> task = GetContentsAsyncConfigureAwait(tmpUrl);
            int length = task.Result;
            MessageBox.Show(String.Format("The number of bytes on {0} is {1}", tmpUrl, length), "Async ConfigureAwait");

        }

        private async void Btn_async_atw_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Calling async all-the-way (should not deadlock)", "Async all-the-way Clicked!");
            string tmpUrl = "http://developer.microsoft.com/";
            int length = await GetContentsAsync(tmpUrl);
            MessageBox.Show(String.Format("The number of bytes on {0} is {1}", tmpUrl, length), "Async all-the-way");

        }

        #region aux methods

        public async Task<int> GetContentsAsync(string url)
        {
            string retStr = "";
            HttpClient client = new HttpClient();
            retStr = await client.GetStringAsync(url);
            return retStr.Length;
        }

        public async Task<int> GetContentsAsyncConfigureAwait(string url)
        {
            string retStr = "";
            HttpClient client = new HttpClient();
            retStr = await client.GetStringAsync(url).ConfigureAwait(false);
            return retStr.Length;
        }

        #endregion

    }
}
