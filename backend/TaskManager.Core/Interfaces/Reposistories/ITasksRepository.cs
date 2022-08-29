using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Reposistories.Base;
using Task = TaskManager.Core.Entities.Task;

namespace TaskManager.Core.Interfaces.Reposistories;

public interface ITasksRepository:IMongoRepository<Task>
{
    
}