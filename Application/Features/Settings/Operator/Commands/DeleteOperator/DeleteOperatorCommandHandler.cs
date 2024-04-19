using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Operator.Commands.DeleteOperator
{
    internal class DeleteOperatorCommandHandler : IRequestHandler<DeleteOperatorRequest, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOperatorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteOperatorRequest request, CancellationToken cancellationToken)
        {
            var operatorDelete = await _unitOfWork.Repository<Operators>().GetByIdAsync(request.Id);
            if (operatorDelete != null)
            {
                operatorDelete.DeletedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Operators>().DeleteAsync(operatorDelete);
                operatorDelete.AddDomainEvent(new OperatorDeletedEvent(operatorDelete));
                await _unitOfWork.Save(cancellationToken);

                return await Result<Guid>.SuccessAsync(operatorDelete.Id, "Operator Deleted.");
            }
            return await Result<Guid>.FailureAsync("Operator Not Found");
        }
    }
}