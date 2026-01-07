using MediatR;
using MongoDB.Driver;
using TaskManagement.Application.Common;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Tasks.Queries;

public record GetAllTasksQuery() : IRequest<List<TaskItem>>;

public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, List<TaskItem>>
{
    private readonly IMongoDbContext _context;
    public GetAllTasksHandler(IMongoDbContext context) => _context = context;

    public async Task<List<TaskItem>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tasks.Find(_ => true).ToListAsync(cancellationToken);
    }
}