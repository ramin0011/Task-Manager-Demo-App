using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Services.Interfaces;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces.Reposistories;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            this._usersRepository=usersRepository;
        }

        public Task<bool> Login(string username, string password)
        {
            return _usersRepository.IsCredentialsCorrect(username, password);
        }
        
        public Task<User> GetUser(string username)
        {
            return _usersRepository.FindOneAsync(a=>a.UserName==username.ToLower());
        }  
        
        public Task CreateUser(string username, string password,List<string> roles)
        {
            return _usersRepository.InsertOneAsync(new User(){UserName = username,Password = password,Roles = roles});
        }
    }
}
