using AutoMapper;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.IotHub.DTOs;

namespace SkeletonApi.IotHub.Services.Store
{
    public class TaskStore
    {
        private IEnumerable<TaskDto> _Task { get; set; }

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        public TaskStore(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
            _Task = new List<TaskDto>();
            this.Dispatch();
        }

        public Task Dispatch()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var scoped = scope.ServiceProvider.GetRequiredService<ITaskRepository>();
                    var tasks = scoped.GetAllTasks();

                    _Task = _mapper.Map<IEnumerable<TaskDto>>(tasks);
                }
            }
            catch (Exception ex)
            {
                // Handle exception appropriately, log or throw
                Console.Out.WriteLine($"An error occurred while dispatching tasks: {ex.Message}");
            }
            return Task.CompletedTask;
        }

        public IEnumerable<TaskDto> GetAllTask()
        {
            return _Task;
        }
    }
}