using SuperTask.Application.Interfaces;

namespace SuperTaskTracking.Common;

public class CurrentUserAccessor : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var raw = _httpContextAccessor.HttpContext?
                .Request.Headers["X-User-Id"]
                .FirstOrDefault();

            if (!Guid.TryParse(raw, out var userId))
                throw new Exception("Invalid or missing X-User-Id");

            return userId;
        }
    }
}