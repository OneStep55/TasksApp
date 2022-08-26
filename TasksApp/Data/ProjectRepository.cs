using Microsoft.EntityFrameworkCore;
using Tasks.Models;

namespace TasksApp.Data
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _appDbContext;

        // Create repository with dbContext
        public ProjectRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        // Get all projects from dbContext
        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _appDbContext.Projects.ToListAsync();
        }

        // Get one project by id
        public async Task<Project> GetProject(Guid id)
        {
            return await _appDbContext.Projects.FindAsync(id);
        }

        // Add new project to dbContext.Projects
        public async Task<Project> CreateProject(Project project)
        {
            var result = await _appDbContext.Projects.AddAsync(project);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Update existing project
        public async Task<Project> UpdateProject(Guid id, Project project)
        {
            var existingProject = await _appDbContext.Projects.FindAsync(id);

            // If project doesn't exist return null
            if (existingProject == null)
            {
                return null;
            }

            // update existing project
            existingProject.Name = project.Name;
            existingProject.Priority = project.Priority;
            existingProject.Status = project.Status;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;

            // Save all updates
            await _appDbContext.SaveChangesAsync();

            return existingProject;
        }


        // Delete project by it's id
        public async Task<Project> DeleteProject(Guid id)
        {
            var project = await _appDbContext.Projects.FindAsync(id);

            if (project == null)
            {
                return null;
            }

            _appDbContext.Projects.Remove(project);
            await _appDbContext.SaveChangesAsync();
            return project;
        }
    }
}
