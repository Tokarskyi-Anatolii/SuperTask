using SuperTask.Application.Enums;

namespace SuperTask.Application.ServiceModels;

public class GetListOfTaskListsModel
{
    public Guid CurrentUserId { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
    
    public TaskListSortField SortBy { get; init; } = TaskListSortField.CreatedAt;

    public SortDirection SortDirection { get; init; } = SortDirection.Desc;
}