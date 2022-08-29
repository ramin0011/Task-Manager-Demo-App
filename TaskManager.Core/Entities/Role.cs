using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities.Base;

namespace TaskManager.Core.Entities
{
    public class Role :Document
    {
        public string Name { get; set; }
    }
}
