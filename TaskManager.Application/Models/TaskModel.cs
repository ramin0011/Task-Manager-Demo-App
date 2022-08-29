using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace TaskManager.Application.Models
{
    public class TaskModel
    {
        public DateTime Deadline { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ObjectId? ClaimedUser { get; set; }
        public string ClaimedUserName { get; set; }
        public bool IsAssigned => ClaimedUser != null;
    }
}
