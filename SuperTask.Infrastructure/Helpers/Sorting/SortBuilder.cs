using MongoDB.Driver;
using SuperTask.Application.Enums;
using SuperTask.Data.DataModels;

namespace SuperTask.Infrastructure.Helpers.Sorting;

public static class SortBuilder
{
    public static SortDefinition<TaskListDocument> BuildSort(
        TaskListSortField sortBy,
        Application.Enums.SortDirection direction)
    {
        var builder = Builders<TaskListDocument>.Sort;

        return sortBy switch
        {
            TaskListSortField.CreatedAt =>
                direction == Application.Enums.SortDirection.Asc
                    ? builder.Ascending(x => x.CreatedAt)
                    : builder.Descending(x => x.CreatedAt),

            _ => builder.Descending(x => x.CreatedAt)
        };
    }
}