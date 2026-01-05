namespace TaskManagement.Domain.Entities;

public class Project
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // we just have one project manager
    public string OwnerId { get; set; } = string.Empty;

    // we can have some member in project
    public List<string> MemberIds { get; set; } = new();
}