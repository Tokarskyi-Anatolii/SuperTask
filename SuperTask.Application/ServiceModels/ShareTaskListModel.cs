namespace SuperTask.Application.ServiceModels;

public class ShareTaskListModel
{
    public required Guid TaskListId { get; set; }

    public required Guid SharedUserId { get; set; }

    public required Guid CurrentUserId { get; set; }
}