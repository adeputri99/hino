using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG;
using SkeletonApi.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonApi.Presentation.Controllers
{
    [Route("api/monitoring-system")]
    public class MonitoringSystemController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private ILogger _logger;
        public MonitoringSystemController(IMediator mediator, ILogger<ActivityUserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("ok")]
        public async Task<ActionResult<Result<OkOrNgDto>>> GetOk(string type, DateTime start, DateTime end)
        {
            string view = "count_ok_day";
            return await _mediator.Send(new GetCountOkorNgQuery(type, start, end, view));
        }
        [HttpGet("ng")]
        public async Task<ActionResult<Result<OkOrNgDto>>> GetNg(string type, DateTime start, DateTime end)
        {
            string view = "count_ng_day";
            return await _mediator.Send(new GetCountOkorNgQuery(type, start, end, view));
        }
    }
}
