using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SkeletonApi.Application.Extensions;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Type.Queries.GetTypeWithPagination
{
    public record GetTypeWithPaginationQuery : IRequest<PaginatedResult<GetTypeWithPaginationDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Zone { get; set; }

        public GetTypeWithPaginationQuery() { }

        public GetTypeWithPaginationQuery(int pageNumber, int pageSize, string zone)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Zone = zone;
        }
    }

    internal class GetMachinesWithPaginationQueryHandler : IRequestHandler<GetTypeWithPaginationQuery, PaginatedResult<GetTypeWithPaginationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMachinesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetTypeWithPaginationDto>> Handle(GetTypeWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Types>().FindByCondition(x => x.DeletedAt == null)
            .Where(o => (query.Zone == null) || (query.Zone.ToLower() == o.Zone.Name.ToLower()))
            .OrderByDescending(x => x.UpdatedAt)
            .Select(o => new GetTypeWithPaginationDto
            {
                Id = o.Id,
                Name = o.TypeName,
                TaskDuration = o.TaskDuration,
                Zone = o.Zone.Name
            })
            .ProjectTo<GetTypeWithPaginationDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}