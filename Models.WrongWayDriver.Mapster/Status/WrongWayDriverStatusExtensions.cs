// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Models.WrongWayDriver.Status.Db;

namespace Econolite.Ode.Models.WrongWayDriver.Status
{
    public static partial class WrongWayDriverStatusExtensions
    {
        public static WrongWayDriversStatusDto AdaptToDto(this WrongWayDriverStatusMessageDocument statusMessage, DateTime activeStartDate, DateTime activeEndDate)
        {
            return new WrongWayDriversStatusDto()
            {
                TimeStamp = statusMessage.TimeStamp,
                Latitude = statusMessage.Latitude,
                Longitude = statusMessage.Longitude,
                Location = statusMessage.Location,
                IsActive = (statusMessage.TimeStamp >= activeStartDate && statusMessage.TimeStamp <= activeEndDate)
            };
        }
    }
}
