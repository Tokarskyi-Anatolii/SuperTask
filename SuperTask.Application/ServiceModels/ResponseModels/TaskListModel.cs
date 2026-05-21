namespace SuperTask.Application.ServiceModels;

public class TaskListModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid OwnerUserId { get; set; }

    public IReadOnlyCollection<Guid> SharedUserIds { get; set; } = [];
}