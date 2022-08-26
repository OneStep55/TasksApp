using System.ComponentModel.DataAnnotations;
using Tasks.Enums;

namespace TasksApp.Models
{
    // Model for updating Task
    public class UpdateTaskRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, 2)]
        public TaskStatuses Status { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, 3)]
        public int Priority { get; set; }
    }
}
