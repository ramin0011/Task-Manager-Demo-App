using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using TaskManager.Core.Interfaces.Reposistories;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repository.Base;
using User= TaskManager.Core.Entities.User;
namespace TaskManager.Infrastructure.Repository
{
    public class UsersRepository :MongoRepository<User>,IUsersRepository
    {

        public UsersRepository(IMongoDbContext context) : base(context)
        {
        }

        public Task<bool> IsCredentialsCorrect(string username, string password)
        {
            return AsQueryable().AnyAsync(a => a.UserName == username.Trim() && password == password.Trim());
        }

        public Task<bool> HasRole(string username, string roleName)
        {
            return AsQueryable().AnyAsync(a => a.UserName == username.Trim() && a.Roles.Any(b => b == roleName) );
        }

        public Task AssignRole(string username, string rolename)
        {
            var user = FindOne(a => a.UserName == username);
            if(user.Roles.Any(a=>a==rolename))
                user.Roles.Add(rolename);
            return this.ReplaceOneAsync(user);
        }


    }
}
