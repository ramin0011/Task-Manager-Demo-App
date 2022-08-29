using TaskManager.Application.Models;

namespace TaskManager.Application.Services.Interfaces;

public interface ITaskManagementService
{
    Task<List<TaskModel>> GetTasks(bool showAll);
    Task CreateTask(TaskModel model);
    Task AssignTask(string taskId, string userId);
    Task<List<TaskModel>> GetMyTasks(string? getUserId);
}