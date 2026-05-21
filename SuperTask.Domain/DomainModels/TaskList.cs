namespace SuperTask.Domain.DomainModels;

public sealed class TaskList
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public Guid OwnerUserId { get; private set; }

    public IReadOnlyCollection<Guid> SharedUserIds => _sharedUserIds;

    public DateTime CreatedAt { get; private set; }

    private readonly List<Guid> _sharedUserIds = [];
    
    private TaskList()
    {
    }

    public TaskList(
        Guid id,
        string name,
        Guid ownerUserId)
    {
        ValidateName(name);

        Id = id;
        Name = name;
        OwnerUserId = ownerUserId;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void UpdateName(string name)
    {
        ValidateName(name);

        Name = name;
    }

    public void AddSharedUser(Guid userId, Guid currentUserId)
    {
        EnsureOwner(currentUserId);
        
        if (userId == OwnerUserId)
            return;

        if (_sharedUserIds.Contains(userId))
            return;

        _sharedUserIds.Add(userId);
    }

    public void RemoveSharedUser(Guid userId, Guid currentUserId)
    {
        EnsureOwner(currentUserId);
        
        _sharedUserIds.Remove(userId);
    }

    public bool HasAccess(Guid userId)
    {
        return OwnerUserId == userId
               || _sharedUserIds.Contains(userId);
    }
    
    public void EnsureAccess(Guid userId)
    {
        if (!HasAccess(userId))
            throw new UnauthorizedAccessException("No access");
    }
    
    public void EnsureOwner(Guid userId)
    {
        if (OwnerUserId != userId)
            throw new UnauthorizedAccessException("User is not the owner of this TaskList");
    }
    
    public static TaskList Restore(
        Guid id,
        string name,
        Guid ownerUserId,
        IReadOnlyCollection<Guid> sharedUserIds,
        DateTime createdAt)
    {
        var taskList = new TaskList
        {
            Id = id,
            Name = name,
            OwnerUserId = ownerUserId,
            CreatedAt = createdAt
        };

        foreach (var userId in sharedUserIds)
        {
            taskList._sharedUserIds.Add(userId);
        }

        return taskList;
    }
    
    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required");
        }

        if (name.Length > 255)
        {
            throw new ArgumentException("Name is too long");
        }
    }
}