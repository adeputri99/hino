﻿using FluentValidation;

namespace SkeletonApi.Application.Features.ActivityUsers.Queries.GetActivityUserWithPagination
{
    public class GetActivityUserWithPaginationValidator : AbstractValidator<GetActivityUserWithPaginationQuery>
    {
        public GetActivityUserWithPaginationValidator()
        {
            RuleFor(x => x.page_number)
             .GreaterThanOrEqualTo(1)
             .WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.page_size)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}