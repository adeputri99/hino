using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Operator.Commands.UpdateOperator
{
    internal class UpdateOperatorCommandHandler : IRequestHandler<UpdateOperatorRequest, Result<Operators>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOperatorRepository _operatorRepository;
        public UpdateOperatorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IOperatorRepository operatorRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _operatorRepository = operatorRepository;
        }

        public async Task<Result<Operators>> Handle(UpdateOperatorRequest request, CancellationToken cancellationToken)
        {
            var operatorUpdate = await _unitOfWork.Repository<Operators>().GetByIdAsync(request.Id);
            var validateData = await _operatorRepository.ValidateData(operatorUpdate);

            if (validateData != true)
            {
                return await Result<Operators>.FailureAsync(operatorUpdate,"Data already exist");
            }
            if (operatorUpdate != null)
            {
                operatorUpdate.Name = request.Name;
                operatorUpdate.ZoneId = request.ZonaId;
                operatorUpdate.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Operators>().UpdateAsync(operatorUpdate);
                operatorUpdate.AddDomainEvent(new OperatorUpdateEvent(operatorUpdate));

                await _unitOfWork.Save(cancellationToken);
                return await Result<Operators>.SuccessAsync(operatorUpdate, "Operator Updated");
            }
            return await Result<Operators>.FailureAsync("Operator Not Found");
        }
    }
}