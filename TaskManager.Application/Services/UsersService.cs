using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Interfaces.Reposistories;

namespace TaskManager.Application.Services
{
    internal class UsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            this._usersRepository=usersRepository;
        }

        public Task Login(string username, string password)
        {
            return Task.CompletedTask;
        }
    }
}
