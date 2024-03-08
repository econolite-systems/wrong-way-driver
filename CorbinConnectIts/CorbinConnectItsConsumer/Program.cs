// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.CorbinConnectItsConsumer;
using Econolite.Ode.Status.WrongWayDriver.Messaging.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Econolite.Ode.CorbinConnectIts.Dto.Extensions;
using Status.Common.Messaging.Extensions;
using Econolite.Ode.Extensions.AspNet;

await AppBuilder.BuildAndRunWebHostAsync(args, options => { options.Source = "Corbin Connect Its"; }, (builder, services) =>
{
    services.AddWrongWayDriverProducer(_ => {
        _.ConfigTopic = builder.Configuration["Topics:WrongWayDriverEvents"];
    });
    services.AddCorbinMessageConsumers();
    services.AddActionEventStatusSink(builder.Configuration);
    services.AddHostedService<CorbinMessageConsumer>();
});
