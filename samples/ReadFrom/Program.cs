using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;

namespace ReadFrom
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog();
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
