using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasks.Models;
using TasksApp.Data;
using TasksApp.Models;
using Task = Tasks.Models.Task;

namespace TasksApp.Controllers
{
    [ApiController]
    [Route("/projects")]
    public class ProjectsController: ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            this._projectRepository = projectRepository;
        }

        // Get Request for all tasks  GET projects/
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            return Ok(await _projectRepository.GetProjects());
        }
        // Get one project by id GET projects/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(Guid id)
        {
            var project = await _projectRepository.GetProject(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }


        // Create a new project POST /projects
        [HttpPost]
        public async Task<IActionResult> AddProject(CreateProjectRequest createdProjectRequest)
        {

            if (createdProjectRequest.StartDate.DateIsValid() == false || createdProjectRequest.EndDate.DateIsValid() == false)
            {
                return BadRequest("Invalid day");
            }
            // Create project object form CreateProjectRequestObject
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = createdProjectRequest.Name,
                Priority = createdProjectRequest.Priority,
                Status = createdProjectRequest.Status,
                // Create Datetime for Project
                StartDate = new DateTime(createdProjectRequest.StartDate.Year, createdProjectRequest.StartDate.Month, createdProjectRequest.StartDate.Day),
                EndDate = new DateTime(createdProjectRequest.EndDate.Year, createdProjectRequest.EndDate.Month, createdProjectRequest.EndDate.Day),
            }; 
            await _projectRepository.CreateProject(project);
            return Ok(project);
        }

        // Edit project by it's id  PUT /project/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, UpdateProjectRequest updatedProject)
        {
            if (updatedProject.StartDate.DateIsValid() == false || updatedProject.EndDate.DateIsValid() == false)
            {
                return BadRequest("Invalid day");
            } 
            
            //Create project object for UpdateProjectRequest
            var project = new Project
            {
                Id = id,
                Name = updatedProject.Name,
                Priority = updatedProject.Priority,
                Status = updatedProject.Status,
                // Create Datetime for Project
                StartDate = new DateTime(updatedProject.StartDate.Year, updatedProject.StartDate.Month, updatedProject.StartDate.Day),
                EndDate = new DateTime(updatedProject.EndDate.Year, updatedProject.EndDate.Month, updatedProject.EndDate.Day),
            };


            var res = await _projectRepository.UpdateProject(id, project);

            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }


        // Delete project by its Id DELETE projects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var res =  await _projectRepository.DeleteProject(id);

            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }



}
