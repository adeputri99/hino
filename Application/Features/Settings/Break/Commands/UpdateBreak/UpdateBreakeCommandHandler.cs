using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Break.Commands.UpdateBreak
{
    internal class UpdateBreakeCommandHandler : IRequestHandler<UpdateBreakeRequest, Result<SettingBreak>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBreakeRepository _breakeRepository;

        public UpdateBreakeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IBreakeRepository breakRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _breakeRepository = breakRepository;
        }

        public async Task<Result<SettingBreak>> Handle(UpdateBreakeRequest request, CancellationToken cancellationToken)
        {
            var breakeUpdate = await _unitOfWork.Repository<SettingBreak>().GetByIdAsync(request.Id);
            var validateData = await _breakeRepository.ValidateData(breakeUpdate);

            if (validateData != true)
            {
                return await Result<SettingBreak>.FailureAsync(breakeUpdate, "Data already exist");
            }
            if (breakeUpdate != null)
            {
                breakeUpdate.BreakeName = request.BreakName;
                breakeUpdate.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<SettingBreak>().UpdateAsync(breakeUpdate);
                breakeUpdate.AddDomainEvent(new BreakeUpdatedEvent(breakeUpdate));

                await _unitOfWork.Save(cancellationToken);
                return await Result<SettingBreak>.SuccessAsync(breakeUpdate, "Breake Updated");
            }
            return await Result<SettingBreak>.FailureAsync("Breake Not Found");
        }
    }
}