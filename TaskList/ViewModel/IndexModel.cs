using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = TaskList.Models;

namespace TaskList.ViewModel
{
    public class IndexModel
    {
        public List<Models.Task> userTasks { get; set; }
    }
}
