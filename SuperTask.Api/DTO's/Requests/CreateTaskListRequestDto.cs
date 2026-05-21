namespace SuperTaskTracking.DTO_s;

public class CreateTaskListRequestDto
{
    public required string Name { get; init; }

    public required Guid CurrentUserId { get; init; }
}