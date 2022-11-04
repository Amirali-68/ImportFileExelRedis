using StackExchange.Redis;

namespace Application.Publisher
{
    public partial class EventPublisher : IEventPublisher
    {
        private const string RedisConnectionString = "127.0.0.1:6379";
        private static ConnectionMultiplexer connection =
                        ConnectionMultiplexer.Connect(RedisConnectionString);
        private const string Channel = "message-ImportExcel";

        public virtual async Task PublishAsync(string message)
        {
            var pubsub = connection.GetSubscriber();
            await pubsub.PublishAsync(Channel, message);
        }
    }
}
