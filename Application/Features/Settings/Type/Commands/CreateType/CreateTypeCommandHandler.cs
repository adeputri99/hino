using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Type.Commands.CreateType
{
    internal class CreateTypeCommandHandler : IRequestHandler<CreateTypeRequest, Result<CreateTypeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITypeRepository _typeRepository;

        public CreateTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ITypeRepository typeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _typeRepository = typeRepository;
        }

        public async Task<Result<CreateTypeResponseDto>> Handle(CreateTypeRequest request, CancellationToken cancellationToken)
        {
            var type = _mapper.Map<Types>(request);
            var typeResponse = _mapper.Map<CreateTypeResponseDto>(type);

            var validateData = await _typeRepository.ValidateData(type);

            if (validateData != true)
            {
                return await Result<CreateTypeResponseDto>.FailureAsync(typeResponse, "Data already exist");
            }

            type.CreatedAt = DateTime.UtcNow;
            type.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<Types>().AddAsync(type);
            type.AddDomainEvent(new TypeCreatedEvent(type));
            await _unitOfWork.Save(cancellationToken);

            return await Result<CreateTypeResponseDto>.SuccessAsync(typeResponse, "Type created.");
        }
    }
}