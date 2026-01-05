using MediatR;
using TaskManagement.Application.Common.Interfaces; 
using TaskManagement.Application.Common;
using TaskManagement.Domain.Entities;
using MongoDB.Driver;

namespace TaskManagement.Application.Users.Commands;

public record CreateUserCommand(string Username, string Email, string Password) : IRequest<string>;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IMongoDbContext _context;
    private readonly IPasswordHasher _passwordHasher; 

    public CreateUserHandler(IMongoDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher; 
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Hashing Password efore Save to DB
        var hashedPassword = _passwordHasher.Hash(request.Password);

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hashedPassword, // HashedPassword
            CreatedAt = DateTime.UtcNow
        };

        await _context.Users.InsertOneAsync(user, cancellationToken: cancellationToken);

        return user.Id;
    }
}