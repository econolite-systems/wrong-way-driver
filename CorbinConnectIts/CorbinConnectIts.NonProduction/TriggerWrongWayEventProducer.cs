// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Confluent.Kafka;
using Econolite.Ode.CorbinConnectIts.Dto;
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CorbinConnectIts.NonProduction
{
    public class TriggerWrongWayEventProducer : ITriggerWrongWayEventProducer
    {
        private readonly ILogger _logger;
        private readonly Confluent.Kafka.IProducer<string, string> _producer;
        private readonly TriggerWrongWayEventProducerOptions _options;

        public TriggerWrongWayEventProducer(IBuildMessagingConfig buildMessagingConfig, IOptions<ProducerOptions<string, CameraManualTriggerEvent>> producerOptions, IOptions<TriggerWrongWayEventProducerOptions> options, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType().Name);
            _producer = new ProducerBuilder<string, string>(buildMessagingConfig.BuildProducerClientConfig(producerOptions.Value))
            .AddLogging(_logger)
            .Build();
            _options = options?.Value ?? new TriggerWrongWayEventProducerOptions();
        }

        public async Task Trigger(string deviceId, double latitude, double longitude)
        {
            _logger.LogInformation("Sending manual wrong way triggering event {@}", (deviceId, latitude, longitude));
            var manualevent = new CameraManualTriggerEvent
            {
                Params = new Dictionary<string, string>(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("vehicle", "wrongway"),
                    new KeyValuePair<string, string>("lat", latitude.ToString()),
                    new KeyValuePair<string, string>("lon", longitude.ToString())
                })
            };
            _ = await _producer.ProduceAsync(_options.Topic, new Message<string, string>
            {
                Key = deviceId,
                Value = JsonSerializer.Serialize(manualevent)
            });
        }
    }
}
