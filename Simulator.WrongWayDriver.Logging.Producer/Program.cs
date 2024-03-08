// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Common.Extensions;
using CorbinConnectIts.NonProduction.Extensions;
using Econolite.Ode.Monitoring.Events.Extensions;
using Econolite.Ode.Monitoring.Metrics.Extensions;
using Econolite.Ode.Simulator.WrongWayDriver.Logging.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host
    .CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(_ => _.AddCommandLine(args, new Dictionary<string, string>
    {
        { "-s", "Trigger:SerialNumber" },
        { "-lat", "Trigger:Latitude" },
        { "-long", "Trigger:Longitude" }
    }))
    .AddODELogging()
    .ConfigureServices((builderContext, services) => services
        .AddCorbinNonProduction(_ => _.Topic = builderContext.Configuration["Topics:ManualTrigger"] ?? throw new Exception("Topic missing"))
        .Configure<WrongWayDriverProducerOptions>(builderContext.Configuration.GetSection("Trigger"))
        .AddMetrics(builderContext.Configuration, "Wrong Way Driver")
        .AddUserEventSupport(builderContext.Configuration, _ =>
        {
            _.DefaultSource = "Wrong Way Driver";
            _.DefaultLogName = Econolite.Ode.Monitoring.Events.LogName.SystemEvent;
            _.DefaultCategory = Econolite.Ode.Monitoring.Events.Category.Server;
            _.DefaultTenantId = Guid.Empty;
        })
        .AddHostedService<WrongWayDriverProducer>())
    .Build() 
    .AddUnhandledExceptionLogging()
    .LogStartup()
    .StartAsync();
