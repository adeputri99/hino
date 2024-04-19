using AutoMapper;
using MediatR;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Task.Commands.CreateTask
{
    internal class CreateTaskCommandHandler : IRequestHandler<CreateTaskRequest, Result<CreateTaskResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public CreateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ITaskRepository taskRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<Result<CreateTaskResponseDto>> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            var taskCreate = _mapper.Map<SettingTask>(request);
            var taskResponse = _mapper.Map<CreateTaskResponseDto>(taskCreate);

            var validateData = await _taskRepository.ValidateData(taskCreate);

            if (validateData != true)
            {
                return await Result<CreateTaskResponseDto>.FailureAsync(taskResponse, "Data already exist");
            }

            taskCreate.CreatedAt = DateTime.UtcNow;
            taskCreate.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<SettingTask>().AddAsync(taskCreate);
            taskCreate.AddDomainEvent(new TaskCreatedEvent(taskCreate));
            await _unitOfWork.Save(cancellationToken);

            return await Result<CreateTaskResponseDto>.SuccessAsync(taskResponse, "Task created.");
        }
    }
}