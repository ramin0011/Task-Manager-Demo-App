using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Reposistories;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repository.Base;

namespace TaskManager.Infrastructure.Repository
{
    internal class RolesRepository : MongoRepository<Role> , IRolesRepository
    {
        public RolesRepository(IMongoDbContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task CreateRole(string roleName)
        {
            if(!await AsQueryable().AnyAsync(a=>a.Name==roleName))
                await InsertOneAsync(new Role(){Name = roleName});
        }
    }
}
