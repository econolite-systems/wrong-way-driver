// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Microsoft.Extensions.DependencyInjection;

namespace Econolite.Ode.Repository.WrongWayDriver
{
    public static  class WrongWayDriverRepositoryExtensions
    {
        public static IServiceCollection AddWrongWayDriverConfigRepository(this IServiceCollection services)
        {
            services.AddScoped<IWrongWayDriverConfigRepository, WrongWayDriverConfigRepository>();

            return services;
        }
        public static IServiceCollection AddWrongWayDriverStatusRepository(this IServiceCollection services)
        {
            services.AddScoped<IWrongWayDriverStatusRepository, WrongWayDriverStatusRepository>();

            return services;
        }
    }
}
