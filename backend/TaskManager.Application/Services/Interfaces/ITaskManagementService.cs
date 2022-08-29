using TaskManager.Application.Models;

namespace TaskManager.Application.Services.Interfaces;

internal interface ITaskManagementService
{
    Task<List<TaskModel>> GetTasks();
    Task CreateTask(TaskModel model);
    Task AssignTask(string taskId, string userId);
}