using MediatR;
using MongoDB.Driver;
using TaskManagement.Application.Common;

namespace TaskManagement.Application.Users.Commands;

public record UpdatePasswordCommand(string UserId, string NewPassword) : IRequest<bool>;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordCommand, bool>
{
    private readonly IMongoDbContext _context;
    public UpdatePasswordHandler(IMongoDbContext context) => _context = context;

    public async Task<bool> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var update = Builders<TaskManagement.Domain.Entities.User>.Update
            .Set(u => u.PasswordHash, request.NewPassword);

        var result = await _context.Users.UpdateOneAsync(u => u.Id == request.UserId, update, cancellationToken: cancellationToken);
        return result.ModifiedCount > 0;
    }
}