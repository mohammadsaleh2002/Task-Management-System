using MediatR;
using MongoDB.Driver;
using TaskManagement.Application.Common;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

public record UpdateTaskStatusCommand(string Id, TaskItemStatus NewStatus) : IRequest<bool>;

public class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusCommand, bool>
{
    private readonly IMongoDbContext _context;
    public UpdateTaskStatusHandler(IMongoDbContext context) => _context = context;

    public async Task<bool> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var filter = Builders<TaskItem>.Filter.Eq(t => t.Id, request.Id);
        var update = Builders<TaskItem>.Update.Set(t => t.Status, request.NewStatus);

        var result = await _context.Tasks.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 1;
    }
}