using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskList.Models
{
    public class TasksContext: DbContext
    {
        public TasksContext(DbContextOptions<TasksContext> options): base(options)
        {

        }

    }
}
