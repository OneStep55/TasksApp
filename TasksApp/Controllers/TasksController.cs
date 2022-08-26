using Microsoft.AspNetCore.Mvc;
using TasksApp.Data;
using TasksApp.Models;
using Task = Tasks.Models.Task;

namespace TasksApp.Controllers
{
    [ApiController]
    [Route("/projects")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // Get all tasks of a project by projectID GET /projects/{projectId}/tasks
        [HttpGet("{projectId}/tasks")]
        public async Task<IActionResult> GetTasks(Guid projectId)
        {
            return Ok(await _taskRepository.GetTasks(projectId));
        }

        //Get one task by its id GET projects/tasks/{taskID}
        [HttpGet("tasks/{taskId}")]
        public async Task<IActionResult> GetTask(Guid taskId)
        {
            var task = await _taskRepository.GetTask(taskId);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // Create  task inside project using projectId POST /projects/{projectID}/tasks
        [HttpPost("{projectId}/tasks")]
        public async Task<IActionResult> CreateTask(Guid projectId, CreateTaskRequest newTask)
        {
            // Create task object from CreateTaskRequest
            var task = new Task
            {
                Id = Guid.NewGuid(),
                Name = newTask.Name,
                Description = newTask.Description,
                ProjectId = projectId,
                Priority = newTask.Priority,
                Status = newTask.Status

            };

            var res = await _taskRepository.CreateTask(task);
            
            return Ok(res);
        }

        // Edit task using its id   PUT /projects/tasks/{taskId}
        [HttpPut("tasks/{taskId}")]
        public async Task<IActionResult> UpdateTask(Guid taskId, UpdateTaskRequest updatedTask)
        {
            
            var task = new Task
            {
                Id = taskId,
                Name = updatedTask.Name,
                Description = updatedTask.Description,
                ProjectId = Guid.Empty,
                Priority = updatedTask.Priority,
                Status = updatedTask.Status

            };



            var res = await _taskRepository.UpdateTask(taskId, task);

            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        // Delete one task by its ID DELETE projects/tasks/{taskID}
        [HttpDelete("tasks/{taskId}")]

        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            var res = await _taskRepository.DeleteTask(taskId);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }



        // Delete all tasks from projects using projectId  DELETE projects/{projectId}/tasks
        [HttpDelete("{projectId}/tasks")]
        public async Task<IActionResult> DeleteTasks(Guid projectId)
        {
            // Get all tasks inside a project
            var res = await _taskRepository.DeleteTasks(projectId);

            return Ok(res);

        }
    }
}
