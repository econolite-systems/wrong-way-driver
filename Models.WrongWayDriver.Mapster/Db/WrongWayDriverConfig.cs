// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Persistence.Common.Interfaces;

namespace Econolite.Ode.Models.WrongWayDriver.Db
{
    public class WrongWayDriverConfig : IIndexedEntity<Guid>
    {
        public WrongWayDriverConfig()
        {

        }

        /// <summary>
        /// The number of days in which a wrong way driver incident log entry will be considered "active".  Once the timestamp of the log entry has exceeded this number of days the entry will no longer be shown on the map.
        /// </summary>
        public byte ActiveDays { get; set; } //valid range 0 - 255

        /// <summary>
        ///     Id of the configuration
        /// </summary>
        public Guid Id { get; set; }
    }
}
