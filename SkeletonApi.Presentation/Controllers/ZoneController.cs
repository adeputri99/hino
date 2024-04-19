using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkeletonApi.Application.Features.Settings.Zone.Queries.GetZoneAll;
using SkeletonApi.Shared;

namespace SkeletonApi.Presentation.Controllers
{
    [Route("api/zone")]
    public class ZoneController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private ILogger _logger;

        public ZoneController(IMediator mediator, ILogger<ZoneController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("get-all-zone")]
        public async Task<ActionResult<Result<List<GetZoneAllDto>>>> GetAll()
        {
            return await _mediator.Send(new GetZoneAllQuery());
        }
    }
}