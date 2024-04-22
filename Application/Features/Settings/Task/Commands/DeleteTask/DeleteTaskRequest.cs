using MediatR;
using SkeletonApi.Application.Common.Mappings;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Task.Commands.DeleteTask
{
    public class DeleteTaskRequest : IRequest<Result<Guid>>, IMapFrom<SettingTask>
    {
        public Guid Id { get; set; }

        public DeleteTaskRequest(Guid id)
        {
            Id = id;
        }

        public DeleteTaskRequest()
        {
        }
    }
}