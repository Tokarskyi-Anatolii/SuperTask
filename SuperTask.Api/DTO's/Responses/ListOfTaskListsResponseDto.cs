namespace SuperTaskTracking.DTO_s.Responses;

public class ListOfTaskListsResponseDto
{
    public IReadOnlyCollection<TaskListShortResponseDto> TaskLists { get; set; } = [];
}