namespace SuperTask.Application.ServiceModels;

public class RemoveShareTaskListModel
{
    public Guid TaskListId { get; set; }

    public Guid SharedUserId { get; set; }

    public Guid CurrentUserId { get; set; }
}