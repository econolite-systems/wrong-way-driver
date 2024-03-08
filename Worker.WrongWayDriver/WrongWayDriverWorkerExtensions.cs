// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Models.WrongWayDriver.Status.Db;
using Econolite.Ode.Status.WrongWayDriver;
using Econolite.Ode.Status.WrongWayDriver.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Econolite.Ode.Worker.WrongWayDriver
{
    public static class WrongWayDriverWorkerExtensions
    {
        public static IServiceCollection AddWrongWayDriverConsumers(this IServiceCollection services, string configTopic)
        {
            services.AddTransient<IPayloadSpecialist<WrongWayDriverEvent>, JsonPayloadSpecialist<WrongWayDriverEvent>>();
            services.AddTransient<IConsumeResultFactory<Guid, WrongWayDriverEvent>, ConsumeResultFactory<WrongWayDriverEvent>>();
            services.AddTransient<IConsumer<Guid, WrongWayDriverEvent>, Consumer<Guid, WrongWayDriverEvent>>();
            services.Configure<WrongWayDriverOptions>(_ => _.ConfigTopic = configTopic);
            services.AddTransient<IWrongWayDriverConsumer, WrongWayDriverConsumer>();

            return services;
        }

        public static WrongWayDriverStatusMessageDocument ToWrongWayDocument(this WrongWayDriverEvent message) => new(message.TimeStamp, message.Latitude, message.Longitude, "");
    }
}
