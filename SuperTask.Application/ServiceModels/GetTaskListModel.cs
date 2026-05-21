namespace SuperTask.Application.ServiceModels;

public class GetTaskListModel
{
    public Guid TaskListId { get; set; }

    public Guid CurrentUserId { get; set; }
}