using MongoDB.Driver;
using SuperTask.Infrastructure.Models;

namespace SuperTask.Infrastructure.RepoInterfaces;

public class MongoTaskListRepository
{
    private readonly IMongoCollection<TaskList> _collection;

    public MongoTaskListRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<TaskList>("task_lists");
    }
    public async Task<Guid> CreateAsync(TaskList taskList)
    {
        await _collection.InsertOneAsync(taskList);
        return taskList.Id;
    }
}