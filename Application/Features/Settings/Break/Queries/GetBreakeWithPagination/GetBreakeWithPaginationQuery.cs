using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SkeletonApi.Application.Extensions;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Break.Queries.GetBreakeWithPagination
{
    public record GetBreakeWithPaginationQuery : IRequest<PaginatedResult<GetBreakeWithPaginationDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetBreakeWithPaginationQuery() { }

        public GetBreakeWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetBreakeWithPaginationQueryHandler : IRequestHandler<GetBreakeWithPaginationQuery, PaginatedResult<GetBreakeWithPaginationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBreakeWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetBreakeWithPaginationDto>> Handle(GetBreakeWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<SettingBreak>().FindByCondition(x => x.DeletedAt == null)
            .OrderByDescending(x => x.UpdatedAt)
            .ProjectTo<GetBreakeWithPaginationDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}