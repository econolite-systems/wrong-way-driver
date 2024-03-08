// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Models.WrongWayDriver.Messaging;

namespace Econolite.Ode.Models.WrongWayDriver.Status.Db
{
    //NOTE:  letting Mongo auto generate the id since it will be timescale
    public sealed record WrongWayDriverStatusMessageDocument(
    DateTime TimeStamp,
    double Latitude,
    double Longitude,
    string Location
    ) : WrongWayDriverStatusMessage;
}
