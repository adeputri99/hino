using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SkeletonApi.Application.Extensions;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Operator.Queries.GetOperatorWithPagination
{
    public record GetOperatorWithPaginationQuery : IRequest<PaginatedResult<GetOperatorWithPaginationDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetOperatorWithPaginationQuery() { }

        public GetOperatorWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetOperatorWithPaginationQueryHandler : IRequestHandler<GetOperatorWithPaginationQuery, PaginatedResult<GetOperatorWithPaginationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOperatorWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetOperatorWithPaginationDto>> Handle(GetOperatorWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Operators>().FindByCondition(x => x.DeletedAt == null)
            .OrderByDescending(x => x.UpdatedAt)
            .Select(o => new GetOperatorWithPaginationDto
            {
                Id = o.Id,
                Name = o.Name,
                Zone = o.Zone.Name
            })
            .ProjectTo<GetOperatorWithPaginationDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}