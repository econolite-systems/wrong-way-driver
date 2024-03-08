// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using CorbinConnectIts.NonProduction;
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Monitoring.Events;
using Econolite.Ode.Monitoring.Events.Extensions;
using Econolite.Ode.Monitoring.Metrics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Econolite.Ode.Simulator.WrongWayDriver.Logging.Producer
{
    public class WrongWayDriverProducer : IHostedService
    {
        private readonly ILogger _logger;
        private readonly ITriggerWrongWayEventProducer _triggerWrongWayEventProducer;
        private readonly WrongWayDriverProducerOptions _options;
        private readonly IMetricsCounter _loopCounter;
        private readonly UserEventFactory _userEventFactory;

        public WrongWayDriverProducer(ITriggerWrongWayEventProducer triggerWrongWayEventProducer, IOptions<WrongWayDriverProducerOptions> options, UserEventFactory userEventFactory, IMetricsFactory metricsFactory, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType().Name);
            _triggerWrongWayEventProducer = triggerWrongWayEventProducer;
            _options = options?.Value ?? new WrongWayDriverProducerOptions();
            _userEventFactory = userEventFactory;
            _loopCounter = metricsFactory.GetMetricsCounter("Simulator");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Sending manual trigger to {@}", _options);
                await _triggerWrongWayEventProducer.Trigger(_options.SerialNumber, _options.Latitude, _options.Longitude);
                _logger.ExposeUserEvent(_userEventFactory.BuildUserEvent(EventLevel.Information, string.Format("Manual trigger sent to {0}", _options.SerialNumber)));
                _loopCounter.Increment();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to send manual trigger");
                _logger.ExposeUserEvent(_userEventFactory.BuildUserEvent(EventLevel.Error, string.Format("Failed to send manual trigger to {0}", _options.SerialNumber)));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

