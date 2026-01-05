using MediatR;
using MongoDB.Driver;
using TaskManagement.Application.Common;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Users.Commands;

public record LoginCommand(string Email, string Password) : IRequest<string>;

public class LoginHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IMongoDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginHandler(IMongoDbContext context, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Find User By Email
        var user = await _context.Users.Find(u => u.Email == request.Email).FirstOrDefaultAsync();

        if (user == null) return "Error: Invalid credentials";

        // Check Password
        bool isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid) return "Error: Invalid credentials";

        // Generate Token
        return _jwtProvider.Generate(user);
    }
}