using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Operator.Commands.CreateOperator
{
    internal class CreateOperatorCommandHandler : IRequestHandler<CreateOperatorRequest, Result<CreateOperatorResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOperatorRepository _operatorRepository;

        public CreateOperatorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IOperatorRepository operatorRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _operatorRepository = operatorRepository;
        }

        public async Task<Result<CreateOperatorResponseDto>> Handle(CreateOperatorRequest request, CancellationToken cancellationToken)
        {
            var operatorCreate = _mapper.Map<Operators>(request);
            var operatorResponse = _mapper.Map<CreateOperatorResponseDto>(operatorCreate);

            var validateData = await _operatorRepository.ValidateData(operatorCreate);

            if (validateData != true)
            {
                return await Result<CreateOperatorResponseDto>.FailureAsync(operatorResponse, "Data already exist");
            }

            operatorCreate.CreatedAt = DateTime.UtcNow;
            operatorCreate.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<Operators>().AddAsync(operatorCreate);
            operatorCreate.AddDomainEvent(new OperatorCreatedEvent(operatorCreate));
            await _unitOfWork.Save(cancellationToken);

            return await Result<CreateOperatorResponseDto>.SuccessAsync(operatorResponse, "Type created.");
        }
    }
}