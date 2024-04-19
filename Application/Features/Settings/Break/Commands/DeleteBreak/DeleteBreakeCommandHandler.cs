using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Break.Commands.DeleteBreak
{
    internal class DeleteBreakeCommandHandler : IRequestHandler<DeleteBreakeRequest, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBreakeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteBreakeRequest request, CancellationToken cancellationToken)
        {
            var breakeDelete = await _unitOfWork.Repository<SettingBreak>().GetByIdAsync(request.Id);
            if (breakeDelete != null)
            {
                breakeDelete.DeletedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<SettingBreak>().DeleteAsync(breakeDelete);
                breakeDelete.AddDomainEvent(new BreakeDeletedEvent(breakeDelete));
                await _unitOfWork.Save(cancellationToken);

                return await Result<Guid>.SuccessAsync(breakeDelete.Id, "Breake Deleted.");
            }
            return await Result<Guid>.FailureAsync("Breake Not Found");
        }
    }
}