using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Reposistories.Base;

namespace TaskManager.Core.Interfaces.Reposistories;

public interface IUsersRepository:IMongoRepository<User>
{
    Task<bool> IsCredentialsCorrect(string username, string password);
    Task<bool> HasRole(string username, string roleName);
}