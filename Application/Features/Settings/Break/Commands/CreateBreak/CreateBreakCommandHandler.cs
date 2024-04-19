using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Break.Commands.CreateBreak
{
    internal class CreateBreakCommandHandler : IRequestHandler<CreateBreakRequest, Result<CreateBreakeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBreakeRepository _breakRepository;

        public CreateBreakCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IBreakeRepository breakeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _breakRepository = breakeRepository;
        }

        public async Task<Result<CreateBreakeResponseDto>> Handle(CreateBreakRequest request, CancellationToken cancellationToken)
        {
            var breakCreate = _mapper.Map<SettingBreak>(request);
            var operatorResponse = _mapper.Map<CreateBreakeResponseDto>(breakCreate);

            var validateData = await _breakRepository.ValidateData(breakCreate);

            if (validateData != true)
            {
                return await Result<CreateBreakeResponseDto>.FailureAsync(operatorResponse, "Data already exist");
            }

            breakCreate.CreatedAt = DateTime.UtcNow;
            breakCreate.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<SettingBreak>().AddAsync(breakCreate);
            breakCreate.AddDomainEvent(new BreakCreatedEvent(breakCreate));
            await _unitOfWork.Save(cancellationToken);

            return await Result<CreateBreakeResponseDto>.SuccessAsync(operatorResponse, "Type created.");
        }
    }
}