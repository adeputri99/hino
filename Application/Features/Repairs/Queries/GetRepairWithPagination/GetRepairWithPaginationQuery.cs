using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SkeletonApi.Application.Extensions;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Repairs.Queries.GetRepairWithPagination
{
    public record GetRepairWithPaginationQuery : IRequest<PaginatedResult<GetRepairWithPaginationDto>>
    {
        public int page_number { get; set; }
        public int page_size { get; set; }
        public string status { get; set; }
        public DateTime entry { get; set; }
        public DateTime finish { get; set; }

        public GetRepairWithPaginationQuery() { }

        public GetRepairWithPaginationQuery(int pageNumber, int pageSize, string statuss, DateTime entries, DateTime finishs)
        {
            page_number = pageNumber;
            page_size = pageSize;
            status = statuss;
            entry = entries;
            finish = finishs;
        }
    }

    internal class GetRepairWithPaginationQueryHandler : IRequestHandler<GetRepairWithPaginationQuery, PaginatedResult<GetRepairWithPaginationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRepairWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetRepairWithPaginationDto>> Handle(GetRepairWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Repair>().FindByCondition(x => x.DeletedAt == null)
            .Where(o => (query.status == null) || (query.status.ToLower() == o.Status.ToLower()))
            .OrderByDescending(x => x.UpdatedAt).ProjectTo<GetRepairWithPaginationDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(query.page_number, query.page_size, cancellationToken);
        }
    }
}