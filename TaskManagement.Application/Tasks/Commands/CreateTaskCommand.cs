using MediatR;
using TaskManagement.Application.Common;
using TaskManagement.Domain.Entities;
using MongoDB.Driver;

namespace TaskManagement.Application.Tasks.Commands;


public record CreateTaskCommand(
    string Title,
    string Description,
    string ProjectId,
    string AssignedUserId,
    DateTime? DueDate) : IRequest<string>;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, string>
{
    private readonly IMongoDbContext _context;

    public CreateTaskHandler(IMongoDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // Find a project to check members
        var project = await _context.Projects
            .Find(p => p.Id == request.ProjectId)
            .FirstOrDefaultAsync(cancellationToken);

        if (project == null)
            return "Error: Project not found.";

        // check user is in Project or not
        if (!project.MemberIds.Contains(request.AssignedUserId) && project.OwnerId != request.AssignedUserId)
        {
            return "Error: The assigned user is not a member of this project.";
        }

        // Create New Task
        var taskItem = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            ProjectId = request.ProjectId,
            AssignedUserId = request.AssignedUserId,
            DueDate = request.DueDate,
            CreatedAt = DateTime.UtcNow,
            Status = TaskManagement.Domain.Enums.TaskItemStatus.ToDo
        };

        // Save in MongoDb
        await _context.Tasks.InsertOneAsync(taskItem, cancellationToken: cancellationToken);

        return $"Task '{taskItem.Title}' created and assigned successfully!";
    }
}