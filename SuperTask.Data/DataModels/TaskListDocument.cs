namespace SuperTask.Data.DataModels;

public sealed class TaskListDocument
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid OwnerUserId { get; set; }

    public List<Guid> SharedUserIds { get; set; } = [];

    public DateTime CreatedAt { get; set; }
}