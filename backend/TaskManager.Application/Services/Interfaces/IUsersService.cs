using TaskManager.Core.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Services.Interfaces;

public interface IUsersService
{
    Task<bool> Login(string username, string password);
    Task<User> GetUser(string username);
    Task CreateUser(string username, string password,List<string> roles);
}