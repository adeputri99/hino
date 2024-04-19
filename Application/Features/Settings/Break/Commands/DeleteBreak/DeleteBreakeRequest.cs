using MediatR;
using SkeletonApi.Application.Common.Mappings;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Break.Commands.DeleteBreak
{
    public class DeleteBreakeRequest : IRequest<Result<Guid>>, IMapFrom<SettingBreak>
    {
        public Guid Id { get; set; }
        public DeleteBreakeRequest(Guid id)
        {
            Id = id;
        }
        public DeleteBreakeRequest()
        {
            
        }
    }
}