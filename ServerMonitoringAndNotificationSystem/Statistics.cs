using System.Diagnostics;

namespace ServerMonitoringAndNotificationSystem
{
    static class Statistics
    {
        public static ServerStatistics CollectComputerStatus()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available Bytes");
            PerformanceCounter memoryUsageCounter = new PerformanceCounter("Process", "Working Set", Process.GetCurrentProcess().ProcessName);


            float cpuUsage = cpuCounter.NextValue();
            float availableMemory = ramCounter.NextValue();
            float totaolMemoryUsage = memoryUsageCounter.NextValue();

            Process[] processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                try
                {
                    float memoryUsage = process.WorkingSet64;
                    totaolMemoryUsage = totaolMemoryUsage + memoryUsage;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading memory usage for process {process.ProcessName}: {ex.Message}");
                }
            }

            DateTime timeStamp = DateTime.Now;

            ServerStatistics serverStatistics = new()
            {
                MemoryUsage = totaolMemoryUsage / (1024 * 1024),
                AvailableMemory = availableMemory / (1024 * 1024),
                CpuUsage = cpuUsage,
                Timestamp = timeStamp
            };

            return serverStatistics;
        }
    }
}
