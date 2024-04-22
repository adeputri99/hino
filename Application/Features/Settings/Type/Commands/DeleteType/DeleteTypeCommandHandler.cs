using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Type.Commands.DeleteType
{
    internal class DeleteTypeCommandHandler : IRequestHandler<DeleteTypeRequest, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteTypeRequest request, CancellationToken cancellationToken)
        {
            var type = await _unitOfWork.Repository<Types>().GetByIdAsync(request.Id);
            if (type != null)
            {
                type.DeletedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Types>().DeleteAsync(type);
                type.AddDomainEvent(new TypeDeletedEvent(type));
                await _unitOfWork.Save(cancellationToken);

                return await Result<Guid>.SuccessAsync(type.Id, "Type Deleted.");
            }
            return await Result<Guid>.FailureAsync("Type Not Found");
        }
    }
}