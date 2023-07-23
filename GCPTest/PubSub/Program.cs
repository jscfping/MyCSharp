using Google.Cloud.PubSub.V1;

namespace PubSub
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string projectId = "utopian-eye-378303";
            var Sub1 = new PullMessagesAsyncSample(projectId, "Sub1");
            var Sub2 = new PullMessagesAsyncSample(projectId, "Sub2");

            var sub1 = Sub1.ListenAsync();
            var sub2 = Sub2.ListenAsync();

            var aPublishMessagesAsyncSample = new PublishMessagesAsyncSample();
            while (true)
            {
                string? msg = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(msg)) continue;

                await aPublishMessagesAsyncSample.PublishMessagesAsync(
                projectId,
                "Topic1",
                msg.Split(" ")
            );
            }
        }


        public class PublishMessagesAsyncSample
        {
            public async Task<int> PublishMessagesAsync(string projectId, string topicId, IEnumerable<string> messageTexts)
            {
                TopicName topicName = TopicName.FromProjectTopic(projectId, topicId);
                PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

                int publishedMessageCount = 0;
                var publishTasks = messageTexts.Select(async text =>
                {
                    try
                    {
                        string message = await publisher.PublishAsync(text);

                        Interlocked.Increment(ref publishedMessageCount);

                        ShowMessage($"[Pub] ({publishedMessageCount}) > {message}: {text}");
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"An error ocurred when publishing message {text}: {exception.Message}");
                    }
                });
                await Task.WhenAll(publishTasks);
                return publishedMessageCount;
            }
        }


        public class PullMessagesAsyncSample
        {
            private readonly string _projectId;
            private readonly string _subscriptionId;

            public PullMessagesAsyncSample(string projectId, string subscriptionId)
            {
                _projectId = projectId;
                _subscriptionId = subscriptionId;
            }

            public async Task ListenAsync()
            {
                SubscriptionName subscriptionName = SubscriptionName.FromProjectSubscription(_projectId, _subscriptionId);
                SubscriberClient subscriber = await SubscriberClient.CreateAsync(subscriptionName);

                await subscriber.StartAsync((PubsubMessage message, CancellationToken cancel) =>
                {
                    string text = System.Text.Encoding.UTF8.GetString(message.Data.ToArray());
                    ShowMessage($"[{_subscriptionId}] > {message.MessageId}: {text}");

                    return Task.FromResult(SubscriberClient.Reply.Ack);
                });
            }
        }


        private static void ShowMessage(string msg)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss.ffffff}] {msg}");
        }
    }
}