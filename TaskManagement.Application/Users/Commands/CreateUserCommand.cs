using MediatR;
using TaskManagement.Application.Common;
using TaskManagement.Domain.Entities;
using MongoDB.Driver;

namespace TaskManagement.Application.Users.Commands;

public record CreateUserCommand(string Username, string Email, string Password) : IRequest<string>;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IMongoDbContext _context;

    public CreateUserHandler(IMongoDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = request.Password,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Users.InsertOneAsync(user, cancellationToken: cancellationToken);

        return user.Id;
    }
}