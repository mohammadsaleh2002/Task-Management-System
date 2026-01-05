using MediatR;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Common; 

namespace TaskManagement.Application.Projects.Commands;

public record CreateProjectCommand(string Name, string Description) : IRequest<string>;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, string>
{
    private readonly IMongoDbContext _context; 

    public CreateProjectHandler(IMongoDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project { Name = request.Name, Description = request.Description };
        await _context.Projects.InsertOneAsync(project, cancellationToken);
        return $"Project created successfully!";
    }
}