namespace SuperTask.Application.ServiceModels;

public class DeleteTaskListModel
{
    public Guid TaskListId { get; set; }

    public Guid CurrentUserId { get; set; }
}