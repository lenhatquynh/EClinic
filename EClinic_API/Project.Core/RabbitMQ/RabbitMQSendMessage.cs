using MassTransit;

namespace Project.Core.RabbitMQ
{
    public static class RabbitMQSendMessage
    {
        public static async Task<IBus> SendMessageWithExchangeName<T>(this IBus bus, T data, string ExchangeName) where T : class
        {
            var endPoint = await bus.GetSendEndpoint(new Uri($"exchange:{ExchangeName}"));
            await endPoint.Send<T>(data);
            return bus;
        }
        public static async Task<IBus> SendMessage<T>(this IBus bus, T data) where T : class
        {
            await bus.Send<T>(data);
            return bus;
        }
    }
}
