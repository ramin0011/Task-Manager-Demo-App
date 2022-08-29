using TaskManager.Application.Models;
using TaskManager.Application.Services.Interfaces;
using TaskManager.Core.Interfaces.Reposistories;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Api
{
    public static class SeedDataExt 
    {
        public static IServiceProvider SeedData(this IServiceProvider host)
        {
            using (var scope = host.CreateScope())
            {
                var services = scope.ServiceProvider;
                var usersService = services.GetService(typeof(IUsersService)) as IUsersService;
                var taskManagementService = services.GetService(typeof(ITaskManagementService)) as ITaskManagementService;

                if (usersService.GetUser("worker").Result == null)
                {
                    usersService.CreateUser("worker", "worker",new List<string>(){"worker"}).Wait();
                } 
                if (usersService.GetUser("admin").Result == null)
                {
                    usersService.CreateUser("admin", "admin", new List<string>() { "admin" }).Wait();
                }  
                
                if (taskManagementService.GetTasks(true).Result?.Count == 0)
                {
                    var admin=usersService.GetUser("admin").Result;
                    taskManagementService.CreateTask(new TaskModel()
                    {
                        Deadline = DateTime.UtcNow.AddMonths(1), Name = "Test Task 1", Description = "Great Task 1"
                    }).Wait(); 
                    taskManagementService.CreateTask(new TaskModel()
                    {
                        Deadline = DateTime.UtcNow.AddMonths(1), Name = "Test Task 2", Description = "awesome Task 2"
                    }).Wait();
                }
            }

            return host;
        }
    }
}
