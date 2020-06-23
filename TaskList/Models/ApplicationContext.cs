using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskList.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().HasOne(p => p.User).WithMany(t => t.Tasks).HasForeignKey(p => p.UserID);
            modelBuilder.Entity<User>().HasMany(p => p.Tasks).WithOne(u => u.User).HasForeignKey(p => p.TaskID);
            modelBuilder.Entity<User>().HasData(new User { Login = "admin", Password = "admin", UserID = -1, Tasks = null });
        }
    }
}
