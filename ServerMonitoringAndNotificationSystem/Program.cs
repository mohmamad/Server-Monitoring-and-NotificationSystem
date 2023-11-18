using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    public static void CollectComputerStatus(int TimeInterval)
    {
        PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available Bytes");
        PerformanceCounter memoryUsageCounter = new PerformanceCounter("Process", "Working Set", Process.GetCurrentProcess().ProcessName);

        while (true)
        {
            float cpuUsage = cpuCounter.NextValue();
            float availableMemory = ramCounter.NextValue();
            float TotaolmemoryUsage = memoryUsageCounter.NextValue();

            Process[] processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                try
                {
                    float memoryUsage = process.WorkingSet64;
                    TotaolmemoryUsage = TotaolmemoryUsage + memoryUsage;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading memory usage for process {process.ProcessName}: {ex.Message}");
                }
            }

            DateTime timestamp = DateTime.Now;

            Console.WriteLine($"Timestamp: {timestamp}");
            Console.WriteLine($"CPU Usage: {cpuUsage}%");
            Console.WriteLine($"Memory Usage: {TotaolmemoryUsage / (1024 * 1024):F2} MB");
            Console.WriteLine($"Available Memory: {availableMemory / (1024 * 1024):F2} MB");
            Console.WriteLine("------------------------------------");


            Thread.Sleep(TimeInterval * 1000);
        }
    }
    static void Main()
    {
        CollectComputerStatus(5);
    }
}
