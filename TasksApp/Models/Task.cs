
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasks.Enums;


namespace Tasks.Models;

[Table("tasks")]
public class Task
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = String.Empty;
    public TaskStatuses Status { get; set; }
    public string Description { get; set; } = String.Empty;
    public int Priority {   get; set; }
    
}