using Serilog.Configuration;
using Serilog.Sinks.Kafka;

namespace Serilog
{
    /// <summary>
    ///     Adds the WriteTo.Kafka() extension method to <see cref="LoggerConfiguration" />.
    /// </summary>
    public static class LoggerConfigurationKafkaExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events to a Kafka topic in the broker endpoints.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="batchSizeLimit">The maximum number of events to include in a single batch.</param>
        /// <param name="period">The time in seconds to wait between checking for event batches.</param>
        /// <param name="brokers">The list of brokers separated by comma.</param>
        /// <param name="topic">The topic name.</param>
        /// <returns></returns>
        public static LoggerConfiguration Kafka(
            this LoggerSinkConfiguration loggerConfiguration,
            int batchSizeLimit,
            int period,
            string brokers,
            string topic)
        {
            var sink = new KafkaSink(
                batchSizeLimit,
                period,
                brokers,
                topic);
            return loggerConfiguration.Sink(sink);
        }
    }
}
