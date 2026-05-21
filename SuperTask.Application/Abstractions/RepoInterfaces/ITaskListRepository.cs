using MongoDB.Driver;
using SuperTask.Domain.DomainModels;
using SuperTask.

namespace SuperTask.Infrastructure.RepoInterfaces;

public interface ITaskListRepository
{
    Task CreateAsync(TaskList taskList);
    
    Task UpdateAsync(TaskList taskList);
    
    Task DeleteAsync(Guid id);
    
    Task<TaskList?> GetByIdAsync(Guid id);
    
    Task<IReadOnlyCollection<TaskList>> GetAccessibleAsync(
        Guid userId, 
        int skip, 
        int take, 
        TaskListSortField sortBy,
        SortDirection direction);
}