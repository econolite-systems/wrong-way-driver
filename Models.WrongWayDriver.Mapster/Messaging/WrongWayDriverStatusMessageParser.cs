// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Models.WrongWayDriver.Status.Db;
using System.Text.Json;

namespace Econolite.Ode.Models.WrongWayDriver.Messaging
{
    public class WrongWayDriverStatusMessageParser
    {
        public WrongWayDriverStatusMessage Parse(string type, string data)
        {
            try
            {
                return type switch
                {
                    nameof(WrongWayDriverStatusMessageDocument) => JsonSerializer.Deserialize<WrongWayDriverStatusMessageDocument>(data)!,
                    _ => new UnknownIncidentMessage(type, data)
                };
            }
            catch (Exception ex)
            {
                return new NonParseableIncidentMessage(type, data, ex);
            }
        }
    }
}
