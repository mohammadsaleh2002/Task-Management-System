using MediatR;
using MongoDB.Driver;
using TaskManagement.Application.Common;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Projects.Commands;

public record UpdateProjectCommand(string Id, string Name, string? Description) : IRequest<bool>;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, bool>
{
    private readonly IMongoDbContext _context;

    public UpdateProjectHandler(IMongoDbContext context) => _context = context;

    public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<Project>.Filter.Eq(p => p.Id, request.Id);

        var update = Builders<Project>.Update
            .Set(p => p.Name, request.Name)
            .Set(p => p.Description, request.Description);

        var result = await _context.Projects.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        return result.ModifiedCount > 0;
    }
}