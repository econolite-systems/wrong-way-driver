// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging;
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Messaging.Extensions;
using Econolite.Ode.Status.WrongWayDriver;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Econolite.Ode.CorbinConnectIts.Dto.Extensions
{
    public static class Defined
    {
        public static IServiceCollection AddCorbinMessageConsumers(this IServiceCollection services) => services
            .AddMessaging()
            .AddTransient<IPayloadSpecialist<WrongWayEvent>, JsonPayloadSpecialist<WrongWayEvent>>()
            .AddSingleton<Func<byte[], string>>(_ => Encoding.UTF8.GetString(_))
            .AddTransient<IConsumeResultFactory<string, WrongWayEvent>, ConsumeResultFactory<string, WrongWayEvent>>()
            .AddTransient<IScalingConsumer<string, WrongWayEvent>, ScalingConsumer<string, WrongWayEvent>>()
            .AddTransient<IWrongWayEventConsumer, WrongWayEventConsumer>();

        public static WrongWayDriverEvent ToOde(this WrongWayEvent wrongWayEvent)
        {
            double.TryParse(wrongWayEvent.SensorParams["lat"], out var lat);
            double.TryParse(wrongWayEvent.SensorParams["lon"], out var @long);
            DateTime.TryParse(wrongWayEvent.Timestamp, out var timestamp);
            return new WrongWayDriverEvent
            {
                Latitude = lat,
                Longitude = @long,
                TimeStamp = timestamp,
            };
        }
    }
}
