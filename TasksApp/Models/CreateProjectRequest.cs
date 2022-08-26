using System.ComponentModel.DataAnnotations;
using Tasks.Enums;

namespace TasksApp.Models
{
    // Model for creating a Project 
    public class CreateProjectRequest
    {
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public Date StartDate { get; set; }
        [Required] 
        public Date EndDate { get; set; }

        [Required]
        [Range(0, 2)]
        public ProjectStatus Status { get; set; }
        [Required]
        [Range(1, 3)]
        public int Priority { get; set; }
    }
}
