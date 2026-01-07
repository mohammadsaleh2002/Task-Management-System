using MediatR;
using TaskManagement.Application.Common;
using MongoDB.Driver;
using TaskManagement.Domain.Entities;

public record DeleteTaskCommand(string Id) : IRequest<bool>;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly IMongoDbContext _context;
    public DeleteTaskHandler(IMongoDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Tasks.DeleteOneAsync(t => t.Id == request.Id, cancellationToken);
        return result.DeletedCount > 0;
    }
}