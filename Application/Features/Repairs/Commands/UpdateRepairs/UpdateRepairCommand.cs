using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Repairs.Commands.UpdateRepairs
{
    internal class UpdateRepairCommand : IRequestHandler<UpdateRepairRequest, Result<Repair>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRepairCommand(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Repair>> Handle(UpdateRepairRequest request, CancellationToken cancellationToken)
        {
            var repair = await _unitOfWork.Repository<Repair>().GetByIdAsync(request.Id);
            Console.WriteLine(repair);
            if (repair != null)
            {
                repair.FrameNumber = request.FrameNumber;
                repair.Status = request.Status;
                repair.Description = request.Description;
                repair.Entry = request.Entry;
                repair.Finish = request.Finish;
                repair.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Repair>().UpdateAsync(repair);
                repair.AddDomainEvent(new RepairUpdateEvent(repair));

                await _unitOfWork.Save(cancellationToken);
                return await Result<Repair>.SuccessAsync(repair, "Repair Updated");
            }
            return await Result<Repair>.FailureAsync("Repair Not Found");
        }
    }
}