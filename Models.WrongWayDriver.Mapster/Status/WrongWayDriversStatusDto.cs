// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Messaging.Elements;
using Econolite.Ode.Models.WrongWayDriver.Messaging;

namespace Econolite.Ode.Models.WrongWayDriver.Status
{
    public partial record WrongWayDriversStatusDto : WrongWayDriverStatusMessage
    {
        public DateTime TimeStamp { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string? Location { get; set; }
        public bool IsActive { get; set; }
    }
}
