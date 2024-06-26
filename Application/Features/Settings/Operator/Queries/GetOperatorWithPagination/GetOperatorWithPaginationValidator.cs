﻿using FluentValidation;

namespace SkeletonApi.Application.Features.Settings.Operator.Queries.GetOperatorWithPagination
{
    public class GetOperatorWithPaginationValidator : AbstractValidator<GetOperatorWithPaginationQuery>
    {
        public GetOperatorWithPaginationValidator()
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