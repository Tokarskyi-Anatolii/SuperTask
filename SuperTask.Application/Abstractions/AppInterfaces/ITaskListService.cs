
using SuperTask.Application.ServiceModels;
using SuperTask.Domain.DomainModels;

namespace SuperTask.Application.Interfaces;

public interface ITaskListService
{
    Task<Guid> CreateAsync(CreateTaskListModel model);

    Task UpdateAsync(UpdateTaskListModel model);

    Task DeleteAsync(DeleteTaskListModel model);
    
    Task<TaskListModel> GetByIdAsync(GetTaskListModel model);
    
    Task<IReadOnlyCollection<TaskListShortModel>> GetListAsync(GetListOfTaskListsModel model);

    Task ShareAsync(ShareTaskListModel model);

    Task RemoveShareAsync(RemoveShareTaskListModel model);

    Task<IReadOnlyCollection<Guid>> GetSharedUsersAsync(GetSharedUsersModel model);
}