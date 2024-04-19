using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Task.Commands.UpdateTask
{
    internal class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskRequest, Result<SettingTask>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<SettingTask>> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            var taskUpdate = await _unitOfWork.Repository<SettingTask>().GetByIdAsync(request.Id);

            if (taskUpdate != null)
            {
                taskUpdate.OperatorId = request.OperatorId;
                taskUpdate.TaskName = request.TaskName;
                taskUpdate.TaskDuration = request.TaskDuration;
                taskUpdate.TaskNo = request.TaskNo;
                taskUpdate.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.Repository<SettingTask>().UpdateAsync(taskUpdate);
                taskUpdate.AddDomainEvent(new TaskUpdateEvent(taskUpdate));

                await _unitOfWork.Save(cancellationToken);
                return await Result<SettingTask>.SuccessAsync(taskUpdate, "Task Updated");
            }
            return await Result<SettingTask>.FailureAsync("Task Not Found");
        }
    }
}