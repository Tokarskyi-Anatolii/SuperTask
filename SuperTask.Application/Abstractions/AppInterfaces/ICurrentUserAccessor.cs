namespace SuperTask.Application.Interfaces;

public interface ICurrentUserAccessor
{
    Guid UserId { get; }
}