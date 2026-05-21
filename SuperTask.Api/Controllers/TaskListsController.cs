using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuperTask.Application.Interfaces;
using SuperTask.Application.ServiceModels;
using SuperTaskTracking.DTO_s;
using SuperTaskTracking.DTO_s.Responses;

namespace SuperTaskTracking.Controllers;

[Route("api/task-lists")]
[ApiController]
public class TaskListsController : ControllerBase
{
    private readonly ITaskListService _service;
    private readonly IMapper _mapper;
    private readonly ICurrentUserAccessor _currentUserAccessor;
    

    public TaskListsController(ITaskListService service, IMapper mapper,  ICurrentUserAccessor currentUserAccessor)
    {
        _service = service;
        _mapper = mapper;
        _currentUserAccessor = currentUserAccessor;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskListRequestDto request)
    {
        var model = _mapper.Map<CreateTaskListModel>(request);
        
        model.CurrentUserId = _currentUserAccessor.UserId;

        var result = await _service.CreateAsync(model);
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdateTaskListRequestDto request)
    {
        var model = _mapper.Map<UpdateTaskListModel>(request);

        model.CurrentUserId = _currentUserAccessor.UserId;
        
        await _service.UpdateAsync(model);
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var model = new DeleteTaskListModel
        {
            TaskListId = id,
            CurrentUserId = _currentUserAccessor.UserId
        };

        await _service.DeleteAsync(model);

        return NoContent();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var model = new GetTaskListModel
        {
            TaskListId = id,
            CurrentUserId = _currentUserAccessor.UserId
        };

        var result = await _service.GetByIdAsync(model);

        return Ok(_mapper.Map<TaskListResponseDto>(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetListOfTaskListsRequest request)
    {
        var model = new GetListOfTaskListsModel
        {
            CurrentUserId = _currentUserAccessor.UserId,
            Page = request.Page,
            PageSize = request.PageSize,
            SortBy = request.SortBy,
            SortDirection = request.SortDirection
        };
        
        var result = await _service.GetListAsync(model);
        
        return Ok(result);
    }
    
    [HttpGet("{id}/shared-users")]
    public async Task<IActionResult> GetSharedUsers(Guid id)
    {
        var model = new GetSharedUsersModel
        {
            TaskListId = id,
            CurrentUserId = _currentUserAccessor.UserId
        };

        var result = await _service.GetSharedUsersAsync(model);

        return Ok(result);
    }
    
    [HttpPost("{id}/share")]
    public async Task<IActionResult> AddShare(Guid id, [FromBody] ShareTaskListRequestDto request)
    {
        var model = new ShareTaskListModel
        {
            TaskListId = id,
            SharedUserId = request.SharedUserId,
            CurrentUserId = _currentUserAccessor.UserId
        };

        await _service.ShareAsync(model);

        return NoContent();
    }
    
    [HttpDelete("{id}/share/delete")]
    public async Task<IActionResult> RemoveShare(Guid id, [FromBody] RemoveShareTaskListRequestDto request)
    {
        var model = new RemoveShareTaskListModel
        {
            TaskListId = id,
            SharedUserId = request.SharedUserId,
            CurrentUserId = _currentUserAccessor.UserId
        };

        await _service.RemoveShareAsync(model);

        return NoContent();
    }
}