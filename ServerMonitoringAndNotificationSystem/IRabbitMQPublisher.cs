namespace ServerMonitoringAndNotificationSystem
{
    internal interface IRabbitMQPublisher
    {
        public void PublishMessage(ServerStatistics serverStatistics , string ServerIdentifier);
    }
}
