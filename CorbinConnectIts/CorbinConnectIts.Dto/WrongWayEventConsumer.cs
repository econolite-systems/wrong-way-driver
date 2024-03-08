// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;

namespace Econolite.Ode.CorbinConnectIts.Dto
{
    public class WrongWayEventConsumer : IWrongWayEventConsumer
    {
        private readonly IScalingConsumer<string, WrongWayEvent> _scalingConsumer;

        public WrongWayEventConsumer(IScalingConsumer<string, WrongWayEvent> scalingConsumer)
        {
            _scalingConsumer = scalingConsumer;
        }

        public Task ConsumeOn(string topic, Func<ConsumeResult<string, WrongWayEvent>, Task> consumeFunc, CancellationToken stoppingToken) => _scalingConsumer.ConsumeOn(topic, consumeFunc, stoppingToken);
    }
}
