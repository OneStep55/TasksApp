using Task = Tasks.Models.Task;

namespace TasksApp.Data
{
    public interface ITaskRepository 
    {
        // All methods in Task repository
        Task<IEnumerable<Task>> GetTasks(Guid projectId);

        Task<Task> GetTask(Guid taskId);

        Task<Task> CreateTask(Task task);

        Task<Task> UpdateTask(Guid taskId, Task task);

        Task<Task> DeleteTask(Guid taskId);

        Task<IEnumerable<Task>> DeleteTasks(Guid projectId);



    }
}
