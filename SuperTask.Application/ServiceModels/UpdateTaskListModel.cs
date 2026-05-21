namespace SuperTask.Application.ServiceModels;

public class UpdateTaskListModel
{
    public required Guid TaskListId { get; set; }

    public required string Name { get; set; }

    public required Guid CurrentUserId { get; set; }
}