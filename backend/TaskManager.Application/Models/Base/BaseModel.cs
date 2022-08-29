using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace TaskManager.Application.Models.Base
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.CreatedAt=DateTime.Now;
        }
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
