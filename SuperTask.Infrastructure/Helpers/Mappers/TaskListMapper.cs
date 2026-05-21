using SuperTask.Data.DataModels;
using SuperTask.Domain.DomainModels;

namespace SuperTask.Infrastructure.Helpers.Mappers;

public class TaskListMapper
{
    public static TaskListDocument ToDocument(TaskList domain)
    {
        return new TaskListDocument
        {
            Id = domain.Id,
            Name = domain.Name,
            OwnerUserId = domain.OwnerUserId,
            SharedUserIds = domain.SharedUserIds.ToList(),
            CreatedAt = domain.CreatedAt
        };
    }
    
    public static TaskList ToDomain(TaskListDocument doc)
    {
        return TaskList.Restore(
            doc.Id,
            doc.Name,
            doc.OwnerUserId,
            doc.SharedUserIds,
            doc.CreatedAt
        );
    }
}