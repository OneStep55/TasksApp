using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tasks.Models;
using Task = Tasks.Models.Task;

namespace TasksApp.Data
{
    // DB context to access Database
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        
        // DataSets for projects and tasks
        public DbSet<Project> Projects { get; set; }

        public DbSet<Task> Tasks { get; set; }



    }

   
}
