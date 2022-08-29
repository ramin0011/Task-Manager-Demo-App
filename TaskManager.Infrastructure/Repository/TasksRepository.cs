using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Core.Interfaces.Reposistories;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repository.Base;
using TaskEntity= TaskManager.Core.Entities.Task;
namespace TaskManager.Infrastructure.Repository
{
    public class TasksRepository : MongoRepository<TaskEntity>,ITasksRepository
    {
        public TasksRepository(IMongoDbContext context) : base(context)
        {
        }


    }
}
