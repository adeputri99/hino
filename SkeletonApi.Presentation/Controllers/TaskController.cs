using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkeletonApi.Application.Features.Settings.Task;
using SkeletonApi.Application.Features.Settings.Task.Commands.CreateTask;
using SkeletonApi.Application.Features.Settings.Task.Commands.DeleteTask;
using SkeletonApi.Application.Features.Settings.Task.Commands.UpdateTask;
using SkeletonApi.Application.Features.Settings.Task.Queries.GetTaskWithPagination;
using SkeletonApi.Application.Features.Settings.Type.Queries.GetTypeWithPagination;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;
using System.Text.Json;

namespace SkeletonApi.Presentation.Controllers
{
    [Route("api/task")]
    public class TaskController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private ILogger _logger;

        public TaskController(IMediator mediator, ILogger<TaskController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Result<CreateTaskResponseDto>>> CreateTask(CreateTaskRequest command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Result<Guid>>> DeleteTask(Guid id)
        {
            return await _mediator.Send(new DeleteTaskRequest(id));
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Result<SettingTask>>> UpdateTask(Guid id, UpdateTaskRequest command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return await _mediator.Send(command);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PaginatedResult<GetTypeWithPaginationDto>>> GetTaskWithPagination([FromQuery] GetTaskWithPaginationQuery query)
        {
            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var validator = new GetTaskWithPaginationValidator();

            var result = validator.Validate(query);

            if (result.IsValid)
            {
                var pg = await _mediator.Send(query);
                var paginationData = new
                {
                    pg.PageNumber,
                    pg.TotalPages,
                    pg.PageSize,
                    pg.TotalCount,
                    pg.HasPrevious,
                    pg.HasNext
                };
                Response.Headers.Add("x-pagination", JsonSerializer.Serialize(paginationData));
                return Ok(pg);
            }

            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }
    }
}