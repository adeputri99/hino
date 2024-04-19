using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Task.Commands.DeleteTask
{
    internal class DeleteTaskCoomandHandler : IRequestHandler<DeleteTaskRequest, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskCoomandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
        {
            var taskDelete = await _unitOfWork.Repository<SettingTask>().GetByIdAsync(request.Id);
            if (taskDelete != null)
            {
                taskDelete.DeletedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<SettingTask>().DeleteAsync(taskDelete);
                taskDelete.AddDomainEvent(new TaskDeletedEvent(taskDelete));
                await _unitOfWork.Save(cancellationToken);

                return await Result<Guid>.SuccessAsync(taskDelete.Id, "Task Deleted.");
            }
            return await Result<Guid>.FailureAsync("Task Not Found");
        }
    }
}