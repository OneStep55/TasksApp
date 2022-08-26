using Tasks.Models;
using Task = Tasks.Models.Task;

namespace TasksApp.Data
{
    public interface IProjectRepository
    {
        // All methods in ProjectRepository
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProject(Guid id);
        Task<Project> CreateProject(Project project);
        Task<Project> UpdateProject(Guid id, Project project);
        Task<Project> DeleteProject(Guid id);


    }
}
