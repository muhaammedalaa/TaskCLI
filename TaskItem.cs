using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCli
{
    internal class TaskItem
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; } // todo, in-progress, done
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
