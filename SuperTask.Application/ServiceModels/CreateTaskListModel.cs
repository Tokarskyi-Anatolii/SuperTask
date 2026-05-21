namespace SuperTask.Application.ServiceModels;

public sealed class CreateTaskListModel
{
    public required string Name { get; set; }

    public required Guid CurrentUserId { get; set; }
}