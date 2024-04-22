using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkeletonApi.Application.Features.ActivityUsers.Queries.GetActivityUserWithPagination;
using SkeletonApi.Application.Features.ActivityUsers.Queries.GetAllLogType;
using SkeletonApi.Application.Features.ActivityUsers.Queries.GetAllUsername;
using SkeletonApi.Shared;
using System.Text.Json;

namespace SkeletonApi.Presentation.Controllers
{
    [Route("api/activity")]
    public class ActivityUserController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private ILogger _logger;

        public ActivityUserController(IMediator mediator, ILogger<ActivityUserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("get-activity-user")]
        public async Task<ActionResult<PaginatedResult<GetActivityUserWithPaginationDto>>> GetActivityUserWithPagination([FromQuery] GetActivityUserWithPaginationQuery query)
        {
            var validator = new GetActivityUserWithPaginationValidator();
            // Call Validate or ValidateAsync and pass the object which needs to be validated

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

        [HttpGet("get-list-log-type")]
        public async Task<ActionResult<Result<List<GetAllLogTypeDto>>>> GetAllLogType()
        {
            return await _mediator.Send(new GetAllLogTypeQuery());
        }

        [HttpGet("get-list-username")]
        public async Task<ActionResult<Result<List<GetAllUsernameDto>>>> GetAllUsername()
        {
            return await _mediator.Send(new GetAllUsernameQuery());
        }
    }
}