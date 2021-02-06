using System;
using System.Threading;
using System.Threading.Tasks;

namespace TryAsync
{
    public class TestAsync
    {
        public async Task ExecuteAsync()
        {
            Console.WriteLine($"[{DateTime.Now}] Start with no error:");
            await Task.WhenAll(RunTaskAAsync(), RunTaskBAsync(), RunTaskCAsync(false));
            Console.WriteLine($"[{DateTime.Now}] End");
            Console.WriteLine($"[{DateTime.Now}] Start with error:");
            await Task.WhenAll(RunTaskAAsync(), RunTaskBAsync(), RunTaskCAsync(true));
            Console.WriteLine($"[{DateTime.Now}] End");
            Console.ReadLine();
        }

        private async Task<string> RunTaskAsync(string taskName, int seconds)
        {
            await Task.Delay(seconds * 1000);
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}]{taskName}:{seconds}");
            return taskName;
        }
        private async Task RunTaskAAsync()
        {
            await RunTaskAsync("A1", 5);
        }
        private async Task RunTaskBAsync()
        {
            await RunTaskAsync("B1", 1);
            await RunTaskAsync("B2", 5);
        }
        private async Task RunTaskCAsync(bool hasError)
        {
            await RunTaskAsync("C1", 2);
            try
            {
                if (hasError)
                {
                    await GetError();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"get error: {ex.Message}");
                return;
            }
            await RunTaskAsync("C2", 1);
            await RunTaskAsync("C3", 1);
        }
        private Task GetError()
        {
            throw new Exception("Get a error!");
        }


    }
}
