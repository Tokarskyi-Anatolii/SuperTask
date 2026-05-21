using SuperTask.Application.Enums;

namespace SuperTaskTracking.DTO_s;

public class GetListOfTaskListsRequest
{
    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 20;

    public TaskListSortField SortBy { get; init; } = TaskListSortField.CreatedAt;

    public SortDirection SortDirection { get; init; } = SortDirection.Desc;
}