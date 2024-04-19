using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkeletonApi.Application.Features.Settings.Break;
using SkeletonApi.Application.Features.Settings.Break.Commands.CreateBreak;
using SkeletonApi.Application.Features.Settings.Break.Commands.DeleteBreak;
using SkeletonApi.Application.Features.Settings.Break.Commands.UpdateBreak;
using SkeletonApi.Application.Features.Settings.Break.Queries.GetBreakeWithPagination;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;
using System.Text.Json;

namespace SkeletonApi.Presentation.Controllers
{
    [Route("api/breake")]
    public class BreakeController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private ILogger _logger;

        public BreakeController(IMediator mediator, ILogger<BreakeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Result<CreateBreakeResponseDto>>> CreateBreake(CreateBreakRequest command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Result<Guid>>> DeleteBreake(Guid id)
        {
            return await _mediator.Send(new DeleteBreakeRequest(id));
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Result<SettingBreak>>> UpdateBreake(Guid id, UpdateBreakeRequest command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return await _mediator.Send(command);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PaginatedResult<GetBreakeWithPaginationDto>>> GetBreakeWithPagination([FromQuery] GetBreakeWithPaginationQuery query)
        {
            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var validator = new GetBreakeWithPaginationValidator();

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