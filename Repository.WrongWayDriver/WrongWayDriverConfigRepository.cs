// SPDX-License-Identifier: MIT
// Copyright: 2023 Econolite Systems, Inc.
using Econolite.Ode.Models.WrongWayDriver.Db;
using Econolite.Ode.Persistence.Mongo.Context;
using Econolite.Ode.Persistence.Mongo.Repository;
using Microsoft.Extensions.Logging;

namespace Econolite.Ode.Repository.WrongWayDriver
{
    public class WrongWayDriverConfigRepository : GuidDocumentRepositoryBase<WrongWayDriverConfig>, IWrongWayDriverConfigRepository
    {
        public WrongWayDriverConfigRepository(IMongoContext context, ILogger<WrongWayDriverConfigRepository> logger) : base(context, logger)
        {
        }
    }
}
