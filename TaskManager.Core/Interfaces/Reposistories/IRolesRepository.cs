using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Reposistories.Base;

namespace TaskManager.Core.Interfaces.Reposistories;

public interface IRolesRepository:IMongoRepository<Role>
{
    System.Threading.Tasks.Task CreateRole(string roleName);
}