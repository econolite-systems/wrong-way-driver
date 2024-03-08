// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.

using Econolite.Ode.Models.WrongWayDriver.Db;
using Econolite.Ode.Persistence.Common.Repository;

namespace Econolite.Ode.Repository.WrongWayDriver
{
    public interface IWrongWayDriverConfigRepository : IRepository<WrongWayDriverConfig, Guid>
    {
    }
}
