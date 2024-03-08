// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Models.WrongWayDriver.Status.Db;

namespace Econolite.Ode.Repository.WrongWayDriver
{
    public interface IWrongWayDriverStatusRepository
    {
        /// <summary>
        ///     Inserts a new wrong way driver incident object into the time-series collection.
        /// </summary>
        /// <param name="incident">The new wrong way driver incident to insert</param>
        Task InsertAsync(WrongWayDriverStatusMessageDocument incident);

        /// <summary>
        ///     Finds wrong way driver incidents with a timestamp between the
        ///     given start and end dates. If the end date is not given, returns all incidents with a timestamp
        ///     after the given start date.
        /// </summary>
        /// <param name="startDate">Mandatory start date for filtering incident entries</param>
        /// <param name="endDate">Optional end date for filtering incident entries</param>
        /// <returns>A list of wrong way driver incident objects</returns>
        Task<List<WrongWayDriverStatusMessageDocument>> Find(DateTime startDate, DateTime? endDate);
    }
}
