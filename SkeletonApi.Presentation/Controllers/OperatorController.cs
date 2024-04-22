using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkeletonApi.Application.Features.Settings.Operator;
using SkeletonApi.Application.Features.Settings.Operator.Commands.CreateOperator;
using SkeletonApi.Application.Features.Settings.Operator.Commands.DeleteOperator;
using SkeletonApi.Application.Features.Settings.Operator.Commands.UpdateOperator;
using SkeletonApi.Application.Features.Settings.Operator.Queries.GetOperatorByZone;
using SkeletonApi.Application.Features.Settings.Operator.Queries.GetOperatorWithPagination;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;
using System.Text.Json;

namespace SkeletonApi.Presentation.Controllers
{
    [Route("api/operator")]
    public class OperatorController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private ILogger _logger;

        public OperatorController(IMediator mediator, ILogger<OperatorController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Result<CreateOperatorResponseDto>>> Create(CreateOperatorRequest command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Result<Guid>>> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteOperatorRequest(id));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Result<Operators>>> Update(Guid id, UpdateOperatorRequest command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return await _mediator.Send(command);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PaginatedResult<GetOperatorWithPaginationDto>>> GetMachinesWithPagination([FromQuery] GetOperatorWithPaginationQuery query)
        {
            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var validator = new GetOperatorWithPaginationValidator();

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
        public async Task<ActionResult<Result<IEnumerable<GetOperatorByZoneDto>>>> GetByZoneId(Guid zoneId)
        {
            return await _mediator.Send(new GetOperatorByZoneQuery(zoneId));
        }
    }
}