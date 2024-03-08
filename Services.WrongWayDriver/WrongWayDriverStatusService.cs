// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Repository.WrongWayDriver;
using Econolite.Ode.Models.WrongWayDriver.Status.Db;
using Econolite.Ode.Models.WrongWayDriver.Status;

namespace Econolite.Ode.Services.WrongWayDriver
{
    public class WrongWayDriverStatusService : IWrongWayDriverStatusService
    {
        private readonly IWrongWayDriverStatusRepository _wwdIncidentRepo;
        private readonly IWrongWayDriverConfigRepository _configRepo;

        public WrongWayDriverStatusService(IWrongWayDriverConfigRepository configRepo, IWrongWayDriverStatusRepository wwdIncidentRepo)
        {
            _wwdIncidentRepo = wwdIncidentRepo;
            _configRepo = configRepo;       
        }

        public async Task<WrongWayDriversStatusDto> Add(WrongWayDriverStatusMessageDocument incident)
        {
            var (activeStartDate, activeEndDate) = FindActiveStartAndEndDates();
            await _wwdIncidentRepo.InsertAsync(incident);
            return incident.AdaptToDto(activeStartDate, activeEndDate);
        }

        public async Task<IEnumerable<WrongWayDriversStatusDto>> Find(DateTime startDate, DateTime? endDate)
        {
            var (activeStartDate, activeEndDate) = FindActiveStartAndEndDates();
            
            var result = await _wwdIncidentRepo.Find(startDate, endDate);
            return result.Select(r => r.AdaptToDto(activeStartDate, activeEndDate));
        }
        public async Task<IEnumerable<WrongWayDriversStatusDto>> FindActive()
        {
            var (activeStartDate, activeEndDate) = FindActiveStartAndEndDates();
            var result = await _wwdIncidentRepo.Find(activeStartDate, activeEndDate);
            return result.Select(r => r.AdaptToDto(activeStartDate, activeEndDate));
        }

        private (DateTime StartDate, DateTime EndDate) FindActiveStartAndEndDates()
        {
            //default to the latest week
            var endDate = DateTime.UtcNow;
            var startDate = endDate.AddDays(-7);

            //get the wrong way driver configuration and see the number of days they want to be considered "active"
            var config = _configRepo.GetAll();
            if (config.Any())
            {
                var singleConfig = config.First();
                startDate = endDate.AddDays(-singleConfig.ActiveDays);
            }
            var dateRange = (StartDate: startDate, EndDate: endDate);
            return dateRange;
        }
    }
}
