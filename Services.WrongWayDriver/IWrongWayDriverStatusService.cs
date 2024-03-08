// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Models.WrongWayDriver.Status;
using Econolite.Ode.Models.WrongWayDriver.Status.Db;

namespace Econolite.Ode.Services.WrongWayDriver
{
    public interface IWrongWayDriverStatusService
    {
        Task<WrongWayDriversStatusDto> Add(WrongWayDriverStatusMessageDocument incident);
        Task<IEnumerable<WrongWayDriversStatusDto>> Find(DateTime startDate, DateTime? endDate);
        Task<IEnumerable<WrongWayDriversStatusDto>> FindActive();
    }
}
