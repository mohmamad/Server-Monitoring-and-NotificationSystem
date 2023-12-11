using Microsoft.Extensions.Configuration;
using ServerMonitoringAndNotificationSystem;

public class ServerStatistics
{
    public double MemoryUsage { get; set; }
    public double AvailableMemory { get; set; }
    public double CpuUsage { get; set; }
    public DateTime Timestamp { get; set; }
}

class Program
{
    static void Main()
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        string serverIdentifier = config["ServerStatisticsConfig:ServerIdentifier"];

        int TimeInterval = int.Parse(config["ServerStatisticsConfig:SamplingIntervalSeconds"]);

        Publisher publisher = new Publisher();

        while (true)
        {
            var statistics = Statistics.CollectComputerStatus();
            Thread.Sleep(TimeInterval * 1000);

            publisher.PublishMessage(statistics, "server123");
        }

    }
}
