using MediatR;
using MongoDB.Driver;
using TaskManagement.Application.Common;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Users.Queries;

public record GetAllUsersQuery() : IRequest<List<User>>;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<User>>
{
    private readonly IMongoDbContext _context;
    public GetAllUsersHandler(IMongoDbContext context) => _context = context;

    public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users.Find(_ => true).ToListAsync(cancellationToken);
    }
}