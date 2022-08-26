using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Task = Tasks.Models.Task;

namespace TasksApp.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;

        // Create TaskRepository with dbContext 
        public TaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all tasks in project by project id
        public async Task<IEnumerable<Task>> GetTasks(Guid projectId)
        {
            return await _dbContext.Tasks.Where(task => task.ProjectId == projectId).ToListAsync();

        }

        // Get task by task id
        public async Task<Task> GetTask(Guid id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);

            return task;
        }

        // Add new task to dbContext.Tasks.
        public async Task<Task> CreateTask(Task task)
        {
            var res = await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return res.Entity;
        }

        // Update existing task by it's id
        public async Task<Task> UpdateTask(Guid taskId, Task task)
        {
            var existingTask = await _dbContext.Tasks.FindAsync(taskId);
            if (existingTask == null)
            {
                return null;
            }

            existingTask.Name = task.Name;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.Priority = task.Priority;
            await _dbContext.SaveChangesAsync();
            return existingTask;
        }


        // Delete task by its id
        public async Task<Task> DeleteTask(Guid taskId)
        {
            var task = await _dbContext.Tasks.FindAsync(taskId);

            if (task == null)
            {
                return null;
            }

            _dbContext.Tasks.Remove(task);

            await _dbContext.SaveChangesAsync();
            return task;
        }

        // Delete all tasks inside a project
        public async Task<IEnumerable<Task>> DeleteTasks(Guid projectId)
        {
            // Get all tasks in inside project
            var tasks = await _dbContext.Tasks.Where(task => task.ProjectId == projectId).ToListAsync();

            // Iterate trough each of them and delete each task
            foreach (var task in tasks)
            {
                _dbContext.Tasks.Remove(task);
            }

            await _dbContext.SaveChangesAsync();

            return tasks;
        }
    }
}
