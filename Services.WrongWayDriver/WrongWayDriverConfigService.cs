// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Helpers.Exceptions;
using Econolite.Ode.Models.WrongWayDriver.Db;
using Econolite.Ode.Models.WrongWayDriver.Dto;
using Econolite.Ode.Repository.WrongWayDriver;
using Microsoft.Extensions.Logging;

namespace Econolite.Ode.Services.WrongWayDriver
{
    public class WrongWayDriverConfigService : IWrongWayDriverConfigService
    {
        private readonly IWrongWayDriverConfigRepository _configRepository;
        private readonly ILogger<WrongWayDriverConfigService> _logger;

        public WrongWayDriverConfigService(IWrongWayDriverConfigRepository configRepository, ILogger<WrongWayDriverConfigService> logger)
        {
            _configRepository = configRepository;
            _logger = logger;
        }

        public async Task<WrongWayDriverConfigDto?> GetFirstAsync()
        {
            var list = await _configRepository.GetAllAsync();
            return list.Select(e => e.AdaptToDto()).FirstOrDefault();
        }

        public async Task<WrongWayDriverConfigDto> Add(WrongWayDriverConfigAdd add)
        {
            var config = add.AdaptToWrongWayDriverConfig();

            //enforce that only one record can exist
            var list = await _configRepository.GetAllAsync();
            if (list.Any()) {
                throw new AddException("Wrong way driver configuration already exists");
            }

            _configRepository.Add(config);

            var (success, _) = await _configRepository.DbContext.SaveChangesAsync();

            return config.AdaptToDto();
        }

        public async Task<WrongWayDriverConfigDto?> Update(WrongWayDriverConfigUpdate update)
        {
            try
            {
                var config = await _configRepository.GetByIdAsync(update.Id);
                var updated = update.AdaptTo(config);

                _configRepository.Update(updated);

                var (success, errors) = await _configRepository.DbContext.SaveChangesAsync();
                if (!success && !string.IsNullOrWhiteSpace(errors)) throw new UpdateException(errors);
                return updated.AdaptToDto();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                _configRepository.Remove(id);
                var (success, errors) = await _configRepository.DbContext.SaveChangesAsync();
                return success;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}
