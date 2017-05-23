using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace WriteTo
{
    class Program
    {
        static void Main(string[] args)
        {
            var brokers = args[0];
            var topic = args[1];

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Kafka(batchSizeLimit: 50, period: 1, brokers: brokers, topic: topic)
                .CreateLogger();

            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory
                .AddSerilog();
            var logger = loggerFactory.CreateLogger<Program>();

            for (int i = 0; i < 10; i++)
            {
                logger.LogInformation($"Message {i}");
            }

            Console.WriteLine($"Finish to log messages.");
            Console.Read();

            Log.CloseAndFlush();
        }
    }
}
