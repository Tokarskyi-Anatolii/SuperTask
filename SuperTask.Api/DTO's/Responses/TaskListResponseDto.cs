namespace SuperTaskTracking.DTO_s.Responses;

public class TaskListResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid OwnerUserId { get; set; }
    
    public IReadOnlyCollection<Guid> SharedUserIds { get; set; } = [];
}