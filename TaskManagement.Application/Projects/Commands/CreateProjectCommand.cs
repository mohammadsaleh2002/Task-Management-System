using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Application.Projects.Commands;

// The Request: Data needed to create a project
public record CreateProjectCommand(string Name, string Description) : IRequest<string>;

// The Handler: The logic to save the project to MongoDB
public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, string>
{
    private readonly MongoDbContext _context;

    public CreateProjectHandler(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Name = request.Name,
            Description = request.Description
        };

        await _context.Projects.InsertOneAsync(project, cancellationToken: cancellationToken);

        return $"Project '{project.Name}' was successfully created!";
    }
}