// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Persistence.Mongo.Context;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Econolite.Ode.Models.WrongWayDriver.Status.Db;

namespace Econolite.Ode.Repository.WrongWayDriver
{
    public class WrongWayDriverStatusRepository : IWrongWayDriverStatusRepository
    {
        private readonly IMongoCollection<WrongWayDriverStatusMessageDocument> _wwdIncidentCollection;

        public WrongWayDriverStatusRepository(IConfiguration configuration, IMongoContext mongoContext)
        {
            _wwdIncidentCollection = mongoContext.GetCollection<WrongWayDriverStatusMessageDocument>(configuration["Collections:WrongWayDriverStatus"] ?? "Collection missing");
        }

        public async Task InsertAsync(WrongWayDriverStatusMessageDocument incident)
        {
            await _wwdIncidentCollection.InsertOneAsync(incident);
        }

        public async Task<List<WrongWayDriverStatusMessageDocument>> Find(DateTime startDate, DateTime? endDate)
        {
            return await QueryAsync(startDate, endDate);
        }

        private static FilterDefinition<WrongWayDriverStatusMessageDocument> MakeDateRangeFilter(DateTime startDate, DateTime? endDate)
        {
            var afterStart = Builders<WrongWayDriverStatusMessageDocument>.Filter.Gte(s => s.TimeStamp, startDate);

            if (endDate is null) return afterStart;

            var beforeEnd = Builders<WrongWayDriverStatusMessageDocument>.Filter.Lte(s => s.TimeStamp, endDate);
            return afterStart & beforeEnd;
        }

        private async Task<List<WrongWayDriverStatusMessageDocument>> QueryAsync(DateTime startDate,
            DateTime? endDate)
        {
            var cursor = await _wwdIncidentCollection.FindAsync(MakeDateRangeFilter(startDate, endDate));
            return await cursor.ToListAsync();
        }
    }
}
