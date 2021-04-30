using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Framework46
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BtnCount.Text = _counts.ToString();
        }
        private int _counts = 0;

        private void BtnToRunTask_Click(object sender, EventArgs e)
        {
            BtnToRunTask.Enabled = false;
            var tmpStr = BtnToRunTask.Text;
            BtnToRunTask.Text = "waitting";

            DoAllAsync()
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        foreach (var ex in t.Exception.Flatten().InnerExceptions)
                        {
                            MessageBox.Show(ex.Message);
                            AddCount();
                        }
                    }
                    BtnToRunTask.Text = tmpStr;
                    BtnToRunTask.Enabled = true;
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task DoAllAsync()
        {
            var task = Task.WhenAll(new List<Task>
                {
                    WaitAsync(1),
                    WaitAsync(2)
                });

            try
            {
                await task;
            }
            catch
            {
                throw task.Exception;
            }
        }

        private async Task WaitAsync(int secs)
        {
            await Task.Delay(1000 * secs);
            throw new Exception($"{secs}: error");
        }

        private void BtnCount_Click(object sender, EventArgs e)
        {
            AddCount();
        }
        private void AddCount()
        {
            _counts++;
            BtnCount.Text = _counts.ToString();
        }
    }
}
