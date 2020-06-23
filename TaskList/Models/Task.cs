using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskList.Models
{
    public class Task
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
