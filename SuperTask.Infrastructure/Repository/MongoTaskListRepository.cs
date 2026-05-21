using MongoDB.Driver;
using SuperTask.Application.Enums;
using SuperTask.Data.DataModels;
using SuperTask.Domain.DomainModels;
using SuperTask.Infrastructure.Helpers.Constants;
using SuperTask.Infrastructure.Helpers.Mappers;
using SuperTask.Infrastructure.Helpers.Sorting;

namespace SuperTask.Infrastructure.RepoInterfaces;

public class MongoTaskListRepository : ITaskListRepository
{
    private readonly IMongoCollection<TaskListDocument> _collection;

    public MongoTaskListRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<TaskListDocument>(MongoCollections.TaskLists);
    }
    
    public async Task CreateAsync(TaskList taskList)
    {
        await _collection.InsertOneAsync(
            TaskListMapper.ToDocument(taskList));
    }
    
    public async Task UpdateAsync(TaskList taskList)
    {
        var doc = TaskListMapper.ToDocument(taskList);

        await _collection.ReplaceOneAsync(
            x => x.Id == doc.Id,
            doc);
    }
    
    public async Task<TaskList?> GetByIdAsync(Guid id)
    {
        var doc = await _collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();

        return doc is null
            ? null
            : TaskListMapper.ToDomain(doc);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
    
    public async Task<IReadOnlyCollection<TaskList>> GetAccessibleAsync(
        Guid userId, 
        int page, 
        int pageSize,
        TaskListSortField sortBy,
        Application.Enums.SortDirection direction)
    {
        var filter = Builders<TaskListDocument>.Filter.Or(
            Builders<TaskListDocument>.Filter.Eq(x => x.OwnerUserId, userId),
            Builders<TaskListDocument>.Filter.AnyEq(x => x.SharedUserIds, userId)
        );
        
        var sort = SortBuilder.BuildSort(sortBy, direction);
        
        var skip = (page - 1) * pageSize;
        
        var docs = await _collection
            .Find(filter)
            .Sort(sort)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync();

        return docs.Select(TaskListMapper.ToDomain).ToList();
    }
}