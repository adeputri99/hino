using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Type.Commands.UpdateType
{
    internal class UpdateTypeCommandHandler : IRequestHandler<UpdateTypeRequest, Result<Types>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Types>> Handle(UpdateTypeRequest request, CancellationToken cancellationToken)
        {
            var type = await _unitOfWork.Repository<Types>().GetByIdAsync(request.Id);
            Console.WriteLine(type);
            if (type != null)
            {
                type.TypeName = request.Name;
                type.ZoneId = request.ZonaId;
                type.TaskDuration = request.TaskDuration;
                type.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<Types>().UpdateAsync(type);
                type.AddDomainEvent(new TypeUpdateEvent(type));

                await _unitOfWork.Save(cancellationToken);
                return await Result<Types>.SuccessAsync(type, "Type Updated");
            }
            return await Result<Types>.FailureAsync("Type Not Found");
        }
    }
}