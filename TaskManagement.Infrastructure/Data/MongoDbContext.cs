using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Common; 

namespace TaskManagement.Infrastructure.Data;

public class MongoDbContext : IMongoDbContext 
{
	private readonly IMongoDatabase _database;

	public MongoDbContext(IConfiguration configuration)
	{
		var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
		_database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
	}

	public IMongoCollection<Project> Projects => _database.GetCollection<Project>("Projects");
	public IMongoCollection<TaskItem> Tasks => _database.GetCollection<TaskItem>("Tasks");
	public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}