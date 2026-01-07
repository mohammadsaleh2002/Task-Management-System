using MediatR;
using MongoDB.Driver;
using TaskManagement.Application.Common;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Projects.Queries;

public record GetAllProjectsQuery() : IRequest<List<Project>>;

public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, List<Project>>
{
    private readonly IMongoDbContext _context;
    public GetAllProjectsHandler(IMongoDbContext context) => _context = context;

    public async Task<List<Project>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Projects.Find(_ => true).ToListAsync(cancellationToken);
    }
}