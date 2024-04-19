using FluentValidation;

namespace SkeletonApi.Application.Features.Settings.Break.Queries.GetBreakeWithPagination
{
    public class GetBreakeWithPaginationValidator : AbstractValidator<GetBreakeWithPaginationQuery>
    {
        public GetBreakeWithPaginationValidator()
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