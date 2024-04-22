using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Repairs.Commands.CreateRepairs
{
    internal class CreateRepairCommandHandler : IRequestHandler<CreateRepairRequest, Result<CreateRepairResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepairRepository _repairRepository;
        private readonly IMapper _mapper;

        public CreateRepairCommandHandler(IUnitOfWork unitOfWork, IRepairRepository repairRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repairRepository = repairRepository;
            _mapper = mapper;
        }

        public async Task<Result<CreateRepairResponseDto>> Handle(CreateRepairRequest request, CancellationToken cancellationToken)
        {
            var Repair = _mapper.Map<Repair>(request);
            var frameNumberResponse = _mapper.Map<CreateRepairResponseDto>(Repair);
            var validateData = await _repairRepository.ValidateData(Repair);

            if (validateData != true)
            {
                return await Result<CreateRepairResponseDto>.FailureAsync(frameNumberResponse, "Data already exist");
            }

            Repair.CreatedAt = DateTime.UtcNow;
            Repair.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<Repair>().AddAsync(Repair);
            Repair.AddDomainEvent(new RepairCreatedEvent(Repair));
            await _unitOfWork.Save(cancellationToken);
            return await Result<CreateRepairResponseDto>.SuccessAsync(frameNumberResponse, "repair created.");
        }
    }
}