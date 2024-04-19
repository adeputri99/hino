using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkeletonApi.Application.Features.ActivityUsers.Queries.GetActivityUserWithPagination;
using SkeletonApi.Application.Features.Repairs;
using SkeletonApi.Application.Features.Repairs.Commands.CreateRepairs;
using SkeletonApi.Application.Features.Repairs.Commands.DeleteRepairs;
using SkeletonApi.Application.Features.Repairs.Commands.UpdateRepairs;
using SkeletonApi.Application.Features.Repairs.Queries.GetRepairWithPagination;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkeletonApi.Presentation.Controllers
{
    [Route("api/repair")]
    public class RepairController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private ILogger _logger;
        public RepairController(IMediator mediator, ILogger<ActivityUserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("get-repair")]
        public async Task<ActionResult<PaginatedResult<GetRepairWithPaginationDto>>> GetRepairWithPagination([FromQuery] GetRepairWithPaginationQuery query)
        {
            var validator = new GetRepairWithPaginationValidator();
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

        [HttpPost]
        public async Task<ActionResult<Result<CreateRepairResponseDto>>> Create(CreateRepairRequest command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Result<Guid>>> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteRepairRequest(id));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Result<Repair>>> Update(Guid id, UpdateRepairRequest command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return await _mediator.Send(command);
        }

    }
}
