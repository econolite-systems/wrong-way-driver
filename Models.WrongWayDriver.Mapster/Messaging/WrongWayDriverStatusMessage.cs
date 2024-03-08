// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
namespace Econolite.Ode.Models.WrongWayDriver.Messaging
{
    public abstract record WrongWayDriverStatusMessage;

    public sealed record UnknownIncidentMessage(string Type, string Data) : WrongWayDriverStatusMessage;

    public sealed record NonParseableIncidentMessage(
        string Type,
        string Data,
        Exception Exception
    ) : WrongWayDriverStatusMessage;
}
