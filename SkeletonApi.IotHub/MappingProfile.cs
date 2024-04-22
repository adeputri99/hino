using AutoMapper;
using SkeletonApi.Application.Features.Settings.Task;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Domain.Entities.Tsdb;
using SkeletonApi.IotHub.Model;

namespace SkeletonApi.IotHub;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //CreateMap<EnginePartDto, EnginePart>().ReverseMap();
        CreateMap<MqttRawValue, MqttRawValueEntity>()
           .ForMember(c => c.Datetime, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeMilliseconds(src.Time).DateTime));
        //CreateMap<NotificationModel, Notifications>().ReverseMap();
        CreateMap<SettingTask, TaskDto>();
        CreateMap<SettingTask, IEnumerable<TaskDto>>().ReverseMap();
        CreateMap<IEnumerable<TaskDto>, IEnumerable<SettingTask>>().ReverseMap();
        CreateMap<TaskDto, SettingTask>().ReverseMap();
    }
}