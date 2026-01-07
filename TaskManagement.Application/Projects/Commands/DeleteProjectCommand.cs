using MediatR;
using TaskManagement.Application.Common;
using MongoDB.Driver;
public record DeleteProjectCommand(string Id) : IRequest<bool>;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IMongoDbContext _context;
    public DeleteProjectHandler(IMongoDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Projects.DeleteOneAsync(p => p.Id == request.Id, cancellationToken);
        return result.DeletedCount > 0;
    }
}