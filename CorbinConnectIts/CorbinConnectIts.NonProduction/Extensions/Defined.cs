// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.CorbinConnectIts.Dto;
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CorbinConnectIts.NonProduction.Extensions
{
    public static class Defined
    {
        public static IServiceCollection AddCorbinNonProduction(this IServiceCollection services) => services
            .AddCorbinNonProduction(_ => { });

        public static IServiceCollection AddCorbinNonProduction(this IServiceCollection services, Action<TriggerWrongWayEventProducerOptions> action) => services
            .AddMessaging()
            .Configure<TriggerWrongWayEventProducerOptions>(_ => action(_))
            .AddTransient<IBuildMessagingConfig<string, CameraManualTriggerEvent>, BuildMessagingConfig<string, CameraManualTriggerEvent>>()
            .AddTransient<ITriggerWrongWayEventProducer, TriggerWrongWayEventProducer> ();
    }
}
