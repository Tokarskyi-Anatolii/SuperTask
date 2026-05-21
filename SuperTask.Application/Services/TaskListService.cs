using SuperTask.Application.Interfaces;
using SuperTask.Application.ServiceModels;
using SuperTask.Domain.DomainModels;
using SuperTask.Infrastructure.RepoInterfaces;

namespace SuperTask.Application.Services;

public class TaskListService : ITaskListService
{
    private readonly ITaskListRepository _repository;
    public TaskListService(ITaskListRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Guid> CreateAsync(CreateTaskListModel model)
    {
        var taskList = new TaskList(
            Guid.NewGuid(),
            model.Name,
            model.CurrentUserId);
        
        await _repository.CreateAsync(taskList);

        return taskList.Id;
    }

    public async Task UpdateAsync(UpdateTaskListModel model)
    {
        var taskList = await _repository.GetByIdAsync(model.TaskListId);
        
        taskList.EnsureAccess(model.CurrentUserId);
        
        taskList.UpdateName(model.Name);
        
        await _repository.UpdateAsync(taskList);
    }

    public async Task DeleteAsync(DeleteTaskListModel model)
    {
        var taskList = await _repository.GetByIdAsync(model.TaskListId);
        
        if (taskList is null)
            throw new InvalidOperationException("TaskList not found");
        
        taskList.EnsureOwner(model.CurrentUserId);
        
        await _repository.DeleteAsync(taskList.Id);
    }

    public async Task<TaskListModel> GetByIdAsync(GetTaskListModel model)
    {
        var taskList = await _repository.GetByIdAsync(model.TaskListId);
        
        if (taskList is null)
            throw new InvalidOperationException("TaskList not found");
        
        if (!taskList.HasAccess(model.CurrentUserId))
            throw new UnauthorizedAccessException("No access to this TaskList");

        return new TaskListModel()
        {
            Id = taskList.Id,
            Name = taskList.Name,
            OwnerUserId = taskList.OwnerUserId,
            SharedUserIds = taskList.SharedUserIds
        };
    }

    public async Task<IReadOnlyCollection<TaskListShortModel>> GetListAsync(GetListOfTaskListsModel model)
    {
        var taskLists = await _repository.GetAccessibleAsync(
            model.CurrentUserId,
            model.Page,
            model.PageSize,
            model.SortBy,
            model.SortDirection);
        
        return taskLists.Select(x => new TaskListShortModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }

    public async Task<IReadOnlyCollection<Guid>> GetSharedUsersAsync(GetSharedUsersModel model)
    {
        var taskList = await _repository.GetByIdAsync(model.TaskListId);
        
        if (taskList is null)
            throw new InvalidOperationException("TaskList not found");

        if (!taskList.HasAccess(model.CurrentUserId))
            throw new UnauthorizedAccessException("No access to this TaskList");
        
        return taskList.SharedUserIds;
    }
    
    public async Task ShareAsync(ShareTaskListModel model)
    {
        var taskList = await _repository.GetByIdAsync(model.TaskListId);

        if (taskList is null)
            throw new InvalidOperationException("TaskList not found");

        taskList.AddSharedUser(model.SharedUserId, model.CurrentUserId);

        await _repository.UpdateAsync(taskList);
    }
    
    public async Task RemoveShareAsync(RemoveShareTaskListModel model)
    {
        var taskList = await _repository.GetByIdAsync(model.TaskListId);

        if (taskList is null)
            throw new InvalidOperationException("TaskList not found");

        taskList.RemoveSharedUser(model.SharedUserId, model.CurrentUserId);

        await _repository.UpdateAsync(taskList);
    }
}