using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.PeriodicBatching;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Serilog.Sinks.Kafka
{
    public class KafkaSink : PeriodicBatchingSink
    {
        private string topic;
        private Producer<Null, string> producer;
        private JsonFormatter formatter;

        public KafkaSink(
            int batchSizeLimit,
            int period,
            string brokers,
            string topic) : base(batchSizeLimit, TimeSpan.FromSeconds(period))
        {
            var config = new Dictionary<string, object> { { "bootstrap.servers", brokers } };
            producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8));
            formatter = new JsonFormatter(closingDelimiter: null, renderMessage: true);
            this.topic = topic;
        }

        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            foreach (var @event in events)
            {
                using (var output = new StringWriter(CultureInfo.InvariantCulture))
                {
                    formatter.Format(@event, output);
                    await producer.ProduceAsync(topic, null, output.ToString());
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            producer?.Flush();
            producer?.Dispose();
            base.Dispose(disposing);
        }
    }
}
