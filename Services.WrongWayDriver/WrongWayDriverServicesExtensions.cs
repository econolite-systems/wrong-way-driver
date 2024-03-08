// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Microsoft.Extensions.DependencyInjection;

namespace Econolite.Ode.Services.WrongWayDriver
{
    public static class WrongWayDriverServicesExtensions
    {
        public static IServiceCollection AddWrongWayDriverConfigService(this IServiceCollection services)
        {
            services.AddScoped<IWrongWayDriverConfigService, WrongWayDriverConfigService>();

            return services;
        }

        public static IServiceCollection AddWrongWayDriverStatusService(this IServiceCollection services)
        {
            services.AddScoped<IWrongWayDriverStatusService, WrongWayDriverStatusService>();

            return services;
        }
    }
}
