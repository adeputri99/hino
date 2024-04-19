using FluentValidation;

namespace SkeletonApi.Application.Features.Settings.Type.Queries.GetTypeWithPagination
{
    public class GetTypeWithPaginationValidator : AbstractValidator<GetTypeWithPaginationQuery>
    {
        public GetTypeWithPaginationValidator()
        {
            RuleFor(x => x.PageNumber)
                   .GreaterThanOrEqualTo(1)
                   .WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}