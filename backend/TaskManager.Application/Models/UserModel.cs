using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Models.Base;

namespace TaskManager.Application.Models
{
    internal class UserModel :BaseModel  
    {
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
