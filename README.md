# Serilog.Sinks.Kafka

Writes [Serilog](https://serilog.net) events to a Kafka.

```csharp
Log.Logger = new LoggerConfiguration()
	.WriteTo.Kafka(batchSizeLimit: 50, period: 1, brokers: brokers, topic: topic)
	.CreateLogger();
```
### JSON `appsettings.json` configuration

A Serilog settings provider that reads from _Microsoft.Extensions.Configuration_, .NET Core's `appsettings.json` file.

Configuration is read from the `Serilog` section.

```json
{
  "Serilog": {
    "Using": [ "Serilog.Sinks.Kafka" ],
    "MinimumLevel": {
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Kafka",
        "Args": { "batchSizeLimit" : "50", "period" : "5", "brokers" : "10.2.1.174:32768", "topic": "demo" }
      }
    ],
    "Properties": {
      "Application": "Kafka"
    }
  }
}
```

### Samples

You can see how to configure Serilog.Sinks.Kafka using WriteTo or ReadFrom and test it with a SimpleConsumer:

![SimpleConsumer](https://github.com/lurumad/serilog-sinks-kafka/blob/master/assets/kafka.gif)

_Copyright &copy; 2016 Lurumad Contributors

