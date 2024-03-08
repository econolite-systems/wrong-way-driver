// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Models.WrongWayDriver.Dto;

namespace Econolite.Ode.Services.WrongWayDriver
{
    public interface IWrongWayDriverConfigService
    {
        Task<WrongWayDriverConfigDto?> GetFirstAsync();
        Task<WrongWayDriverConfigDto> Add(WrongWayDriverConfigAdd add);
        Task<WrongWayDriverConfigDto?> Update(WrongWayDriverConfigUpdate update);
        Task<bool> Delete(Guid id);
    }
}
