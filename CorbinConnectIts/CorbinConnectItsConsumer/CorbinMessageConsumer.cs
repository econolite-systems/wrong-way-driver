// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.CorbinConnectIts.Dto;
using Econolite.Ode.CorbinConnectIts.Dto.Extensions;
using Econolite.Ode.Messaging;
using Econolite.Ode.Monitoring.Metrics;
using Econolite.Ode.Status.Common;
using Econolite.Ode.Status.WrongWayDriver.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Econolite.Ode.CorbinConnectItsConsumer
{
    public class CorbinMessageConsumer : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ISink<ActionEventStatus> _actionEventStatusSink;
        private readonly IWrongWayDriverProducer _wrongWayDriverProducer;
        private readonly IWrongWayEventConsumer _wrongWayEventConsumer;
        private readonly string _topic;
        private readonly Guid _tenantId;
        private readonly IMetricsCounter _messageCounter;

        public CorbinMessageConsumer(ISink<ActionEventStatus> actionEventStatusSink, IWrongWayDriverProducer wrongWayDriverProducer, IWrongWayEventConsumer wrongWayEventConsumer, IConfiguration configuration, IMetricsFactory metricsFactory, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType().Name);
            _actionEventStatusSink = actionEventStatusSink;
            _wrongWayDriverProducer = wrongWayDriverProducer;
            _wrongWayEventConsumer = wrongWayEventConsumer;
            _topic = configuration["Topics:ConnectItsWrongWay"] ?? throw new NullReferenceException("Topics:ConnectItsWrongWay missing in config.");
            _tenantId = Guid.Parse(configuration["Tenant:Id"] ?? throw new NullReferenceException("Tenant:Id missing in config."));
            _messageCounter = metricsFactory.GetMetricsCounter("Wrong Way Events");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Starting the main loop");
                    try
                    {
                        _logger.LogInformation("Consuming wrong way events from topic: {@}", _topic);
                        await _wrongWayEventConsumer.ConsumeOn(_topic, async _ =>
                        {
                            _messageCounter.Increment();
                            var odedata = _.Value.ToOde();
                            await Task.WhenAll(_wrongWayDriverProducer.ProduceAsync(_tenantId, odedata, stoppingToken), _actionEventStatusSink.SinkAsync(_tenantId, odedata, stoppingToken));
                        }, stoppingToken);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex) 
                    {
                        _logger.LogError(ex, "");
                    }
                }
            }
            catch (OperationCanceledException)
            { }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "");
            }
            finally
            {
                _logger.LogInformation("Ending the main loop");
            }
        }
    }
}
