namespace SuperTaskTracking.DTO_s;

public class UpdateTaskListRequestDto
{
    public Guid TaskListId { get; set; }
    public string Name { get; set; } = null!;
}