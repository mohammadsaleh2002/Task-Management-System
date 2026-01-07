using MediatR;
using TaskManagement.Application.Common;
using MongoDB.Driver;
public record DeleteUserCommand(string Id) : IRequest<bool>;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IMongoDbContext _context;
    public DeleteUserHandler(IMongoDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Users.DeleteOneAsync(u => u.Id == request.Id, cancellationToken);
        return result.DeletedCount > 0;
    }
}