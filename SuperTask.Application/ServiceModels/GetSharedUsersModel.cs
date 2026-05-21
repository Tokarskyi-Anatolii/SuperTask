namespace SuperTask.Application.ServiceModels;

public class GetSharedUsersModel
{
    public Guid TaskListId { get; set; }

    public Guid CurrentUserId { get; set; }
}