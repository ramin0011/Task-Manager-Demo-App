using MongoDB.Driver;

namespace TaskManager.Infrastructure.Data;

public interface IMongoDbContext
{
    IMongoDatabase Database { get; }
}