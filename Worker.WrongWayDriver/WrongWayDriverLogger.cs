// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Models.WrongWayDriver.Status.Db;
using Econolite.Ode.Monitoring.Events;
using Econolite.Ode.Monitoring.Events.Extensions;
using Econolite.Ode.Monitoring.Metrics;
using Econolite.Ode.Repository.WrongWayDriver;
using Econolite.Ode.Status.WrongWayDriver.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Econolite.Ode.Worker.WrongWayDriver
{
    public class WrongWayDriverLogger : BackgroundService
    { 
        private readonly IWrongWayDriverStatusRepository _logStorage;
        private readonly ILogger<WrongWayDriverLogger> _logger;
        private readonly IMetricsCounter _loopCounter;
        private readonly IWrongWayDriverConsumer _wrongWayDriverConsumer;
        private readonly UserEventFactory _userEventFactory;

        public WrongWayDriverLogger(
            IWrongWayDriverConsumer wrongWayDriverConsumer,
            IServiceProvider serviceProvider,
            UserEventFactory userEventFactory,
            IMetricsFactory metricsFactory,
            ILogger<WrongWayDriverLogger> logger
        )
        { 
            _logger = logger;
            _wrongWayDriverConsumer = wrongWayDriverConsumer;
            _userEventFactory = userEventFactory;

            _loopCounter = metricsFactory.GetMetricsCounter("Status Logs");

            var serviceScope = serviceProvider.CreateScope();
            _logStorage = serviceScope.ServiceProvider.GetRequiredService<IWrongWayDriverStatusRepository>();
        }

        private async Task LogIncidentAsync(WrongWayDriverStatusMessageDocument data)
        {
            await _logStorage.InsertAsync(data);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () => {
                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            var result = _wrongWayDriverConsumer.Consume(stoppingToken);

                            try
                            {
                                await LogIncidentAsync(result.WrongWayDriverEvent.ToWrongWayDocument());

                                _loopCounter.Increment();
                                _wrongWayDriverConsumer.Complete(result.ConsumeResult);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, "Unhandled exception while processing: {@MessageType}", result);

                                _logger.ExposeUserEvent(_userEventFactory.BuildUserEvent(EventLevel.Error, string.Format("Error while processing Wrong Way Driver: {0}", result)));
                            }
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e, "Exception thrown while trying to consume WrongWayDriverStatusMessage");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Worker.Logging stopping");
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Exception thrown while processing messages");
                }
            }, stoppingToken);
        }
    }
}
