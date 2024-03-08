// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using System.Xml.Linq;

namespace Econolite.Ode.CorbinConnectIts.Dto
{
    public interface IWrongWayEventConsumer
    {
        Task ConsumeOn(string topic, Func<Messaging.Elements.ConsumeResult<string, WrongWayEvent>, Task> consumeFunc, CancellationToken stoppingToken);
    }
}
