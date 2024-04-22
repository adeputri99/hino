using MediatR;
using SkeletonApi.Shared;
using System.ComponentModel.DataAnnotations;

namespace SkeletonApi.Application.Features.ManagementUser.Roles.Commands.CreateRoles
{
    public record CreateRolesRequest : IRequest<Result<CreateRolesResponseDto>>
    {
        [Required(ErrorMessage = "Role Name is required")]
        public string? Name { get; init; }
    }
}