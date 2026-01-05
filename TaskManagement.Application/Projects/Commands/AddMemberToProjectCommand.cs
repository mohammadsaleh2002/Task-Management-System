using MediatR;
using TaskManagement.Application.Common;
using MongoDB.Driver;

namespace TaskManagement.Application.Projects.Commands;

public record AddMemberToProjectCommand(string ProjectId, string UserId) : IRequest<string>;

public class AddMemberToProjectHandler : IRequestHandler<AddMemberToProjectCommand, string>
{
    private readonly IMongoDbContext _context;

    public AddMemberToProjectHandler(IMongoDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(AddMemberToProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .Find(p => p.Id == request.ProjectId)
            .FirstOrDefaultAsync(cancellationToken);

        if (project == null) return "Project not found!";

        // We do not have duplicate members
        if (!project.MemberIds.Contains(request.UserId))
        {
            project.MemberIds.Add(request.UserId);

            var filter = Builders<Domain.Entities.Project>.Filter.Eq(p => p.Id, project.Id);
            await _context.Projects.ReplaceOneAsync(filter, project, cancellationToken: cancellationToken);

            return "Member added successfully!";
        }

        return "User is already a member of this project.";
    }
}