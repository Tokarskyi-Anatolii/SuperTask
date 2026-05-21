namespace SuperTaskTracking.DTO_s.Responses;

public class SharedUsersResponseDto
{
    public IReadOnlyCollection<Guid> UserIds { get; set; } = [];
}