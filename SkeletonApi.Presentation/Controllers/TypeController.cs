using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkeletonApi.Application.Features.Settings.Type;
using SkeletonApi.Application.Features.Settings.Type.Commands.CreateType;
using SkeletonApi.Application.Features.Settings.Type.Commands.DeleteType;
using SkeletonApi.Application.Features.Settings.Type.Commands.UpdateType;
using SkeletonApi.Application.Features.Settings.Type.Queries.GetTypeByZone;
using SkeletonApi.Application.Features.Settings.Type.Queries.GetTypeWithPagination;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;
using System.Text.Json;

namespace SkeletonApi.Presentation.Controllers
{
    [Route("api/type")]
    public class TypeController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private ILogger _logger;

        public TypeController(IMediator mediator, ILogger<TypeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Result<CreateTypeResponseDto>>> Create(CreateTypeRequest command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Result<Guid>>> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteTypeRequest(id));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Result<Types>>> Update(Guid id, UpdateTypeRequest command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return await _mediator.Send(command);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PaginatedResult<GetTypeWithPaginationDto>>> GetMachinesWithPagination([FromQuery] GetTypeWithPaginationQuery query)
        {
            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var validator = new GetTypeWithPaginationValidator();

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

        [HttpGet("{zoneId:guid}")]
        public async Task<ActionResult<Result<IEnumerable<GetTypeByZoneDto>>>> GetByZoneId(Guid zoneId)
        {
            return await _mediator.Send(new GetTypeByZoneQuery(zoneId));
        }
    }
}