using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Repairs.Commands.DeleteRepairs
{
    internal class DeleteRepairCommandHandler : IRequestHandler<DeleteRepairRequest, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRepairCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteRepairRequest request, CancellationToken cancellationToken)
        {
            var Repair = await _unitOfWork.Repository<Repair>().GetByIdAsync(request.Id);
            if (Repair != null)
            {
                Repair.DeletedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Repair>().UpdateAsync(Repair);
                Repair.AddDomainEvent(new RepairDeletedEvent(Repair));
                await _unitOfWork.Save(cancellationToken);

                return await Result<Guid>.SuccessAsync(Repair.Id, "Repair Deleted.");
            }
            return await Result<Guid>.FailureAsync("Repair Not Found");
        }
    }
}