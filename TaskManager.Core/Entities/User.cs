using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities.Base;

namespace TaskManager.Core.Entities
{
    public class User :Document
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<string> Roles { get; set; }

    }
}
