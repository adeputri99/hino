using MediatR;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Task.Commands.UpdateTask
{
    public class UpdateTaskRequest : IRequest<Result<SettingTask>>
    {
        public Guid Id { get; set; }
        public Guid OperatorId { get; set; }
        public string? TaskName { get; set; }
        public string TaskNo { get; set; }
        public int TaskDuration { get; set; }
    }
}