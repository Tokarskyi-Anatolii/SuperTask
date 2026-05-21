using AutoMapper;
using SuperTask.Application.ServiceModels;
using SuperTaskTracking.DTO_s;
using SuperTaskTracking.DTO_s.Responses;

namespace SuperTaskTracking.Helpers.Mappers;

public class TaskListApiProfile : Profile
{
    public TaskListApiProfile()
    {
        CreateMap<CreateTaskListRequestDto, CreateTaskListModel>();
        CreateMap<UpdateTaskListRequestDto, UpdateTaskListModel>();
        CreateMap<TaskListModel, TaskListResponseDto>();
        // CreateMap<TaskListShortModel, TaskListShortResponseDto>();
    }
}