using MediatR;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Task.Commands.CreateTask
{
    public class CreateTaskRequest : IRequest<Result<CreateTaskResponseDto>>
    {
        public Guid OperatorId { get; set; }
        public string? TaskName { get; set; }
        public string TaskNo { get; set; }
        public int TaskDuration { get; set; }
    }
}