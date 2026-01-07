using MediatR;
using TaskManagement.Application.Common;
using TaskManagement.Domain.Entities;
using MongoDB.Driver;

public record GetTaskByIdQuery(string Id) : IRequest<TaskItem?>;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskItem?>
{
    private readonly IMongoDbContext _context;
    public GetTaskByIdHandler(IMongoDbContext context) => _context = context;

    public async Task<TaskItem?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tasks.Find(t => t.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
    }
}