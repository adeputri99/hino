using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkeletonApi.Application.Features.ManagementUser.Permissions;
using SkeletonApi.Application.Features.ManagementUser.Permissions.Commands.CreatePermissions;
using SkeletonApi.Application.Features.ManagementUser.Permissions.Commands.DeletePermissions;
using SkeletonApi.Application.Features.ManagementUser.Permissions.Commands.UpdatePermissions;
using SkeletonApi.Application.Features.ManagementUser.Permissions.Queries.GetPermissionsWithPagination;
using SkeletonApi.Application.Features.ManagementUser.Permissions.Queries.GetRoleWithPagination;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;
using System.Text.Json;

namespace SkeletonApi.Presentation.Controllers
{
    [Route("api/permission")]
    public class PermissionsController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private ILogger _logger;

        public PermissionsController(IMediator mediator, ILogger<PermissionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("create-permission")]
        public async Task<ActionResult<Result<CreatePermissionsResponseDto>>> CreatePermissions(CreatePermissionsRequest command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("delete-permission/{id}")]
        public async Task<ActionResult<Result<string>>> DeletePermissions(string id)
        {
            return await _mediator.Send(new DeletePermissionsRequest(id));
        }

        [HttpPut("update-permission/{id}")]
        public async Task<ActionResult<Result<Permission>>> UpdatePermissions(string id, UpdatePermissionsRequest command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return await _mediator.Send(command);
        }

        [HttpGet("get-list-permission")]
        public async Task<ActionResult<PaginatedResult<GetPermissionsWithPaginationDto>>> GetUserWithPagination([FromQuery] GetPermissionsWithPaginationQuery query)
        {
            var validator = new GetPermissionsWithPaginationValidator();
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
    }
}