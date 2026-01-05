using MongoDB.Driver;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Common;

public interface IMongoDbContext
{
    IMongoCollection<Project> Projects { get; }
    IMongoCollection<TaskItem> Tasks { get; }
    IMongoCollection<User> Users { get; }
}