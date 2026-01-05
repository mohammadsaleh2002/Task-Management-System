namespace TaskManagement.Domain.Entities;

public class Project
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); // string for mongo
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Communication with tasks
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}