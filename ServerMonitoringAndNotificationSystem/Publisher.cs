using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ServerMonitoringAndNotificationSystem
{
    public class Publisher : IRabbitMQPublisher
    {
        public void PublishMessage(ServerStatistics serverStatistics, string ServerIdentifier)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);


            var messageBody = JsonConvert.SerializeObject(serverStatistics);
            var body = Encoding.UTF8.GetBytes(messageBody);

            channel.BasicPublish(exchange: "topic_logs",
                                 routingKey: $"ServerStatistics.{ServerIdentifier}",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($"published message'{ServerIdentifier}':'{body}'");
        }
    }
}
