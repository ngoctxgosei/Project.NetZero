using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WS.EntityFrameworkCore;
using WS.Tasks;

namespace WS.Migrations.Seed.Tasks
{
    public class DefaultTasksBuilder
    {
        private readonly WSDbContext _context;
        private readonly int _tenantId;

        public DefaultTasksBuilder(WSDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }
         public void Create()
        {
            var tasks = new List<Task>
            {
                new Task(_tenantId, "Task one", "Do something"){ State = TaskState.Open},
                new Task(_tenantId, "Task two", "Do something next"){ State = TaskState.Open},
                new Task(_tenantId, "Task three", "I did this "){ State = TaskState.Completed}
            };

            foreach( var task in tasks)
            {
                var existingTask = _context.Tasks.IgnoreQueryFilters().FirstOrDefault(t => t.TenantId == task.TenantId && task.Title == task.Title);
                if(existingTask == null) { _context.Tasks.Add(task);}
            }

            _context.SaveChanges();
        }
    }
}
