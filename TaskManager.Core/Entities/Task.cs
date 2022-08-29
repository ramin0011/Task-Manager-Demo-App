using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using TaskManager.Core.Entities.Base;

namespace TaskManager.Core.Entities
{
    public class Task : Document
    {
        public DateTime Deadline { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ObjectId? ClaimedUser { get; set; }
    }
}
