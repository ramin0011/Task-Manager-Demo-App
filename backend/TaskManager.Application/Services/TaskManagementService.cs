using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Application.Exception;
using TaskManager.Application.Mapper;
using TaskManager.Application.Models;
using TaskManager.Application.Services.Interfaces;
using TaskManager.Core.Interfaces.Reposistories;
using TaskEntity = TaskManager.Core.Entities.Task;

namespace TaskManager.Application.Services
{
    public class TaskManagementService : ITaskManagementService
    {
        private readonly ITasksRepository _taskRepository;
        private readonly IUsersRepository _usersRepository;
        private long MAXIMUM_TASKS_ALLOWED_PERUSER=2;

        public TaskManagementService(ITasksRepository taskRepository, IUsersRepository usersRepository)
        {
            this._taskRepository = taskRepository;
            this._usersRepository = usersRepository;
        }

        public async Task<List<TaskModel>> GetTasks()
        { 
            var data=await _taskRepository.FilterByAsync(a=>a.Deadline>DateTime.Now);
            var result= ObjectMapper.Mapper.Map<List<TaskModel>>(data);
            result.ForEach(GetUserName);
            return result;
        }

        private void GetUserName(TaskModel obj)
        {
            if (obj.ClaimedUser.HasValue)
            {
                obj.ClaimedUserName = _usersRepository.FindById(obj.ClaimedUser.Value.ToString()).UserName;
            }
        }

        public async Task CreateTask(TaskModel model)
        { 
           await _taskRepository.InsertOneAsync(ObjectMapper.Mapper.Map<TaskEntity>(model));
        } 
        
        public async Task AssignTask(string taskId,string userId)
        {
            var usersTasks =await _taskRepository.CountAsync(a=>a.ClaimedUser==ObjectId.Parse(userId));

            if (usersTasks < MAXIMUM_TASKS_ALLOWED_PERUSER)
            {
                var task =await _taskRepository.FindByIdAsync(taskId);
                if (task.ClaimedUser == null)
                {
                    task.ClaimedUser=ObjectId.Parse(userId);
                    await _taskRepository.ReplaceOneAsync(task);
                }
                else
                {
                    throw new AppException("The Task Is Already Assigned To Someone");
                }
            }
            else
            {
                throw new AppException($"The user has already max :{MAXIMUM_TASKS_ALLOWED_PERUSER} tasks assigned");
            }
        }

        public async Task<List<TaskModel>> GetMyTasks(string? userId)
        {
            var data = await _taskRepository.FilterByAsync(a => a.ClaimedUser == ObjectId.Parse(userId));
            var result = ObjectMapper.Mapper.Map<List<TaskModel>>(data);
            result.ForEach(GetUserName);
            return result;
        }
    }
}
