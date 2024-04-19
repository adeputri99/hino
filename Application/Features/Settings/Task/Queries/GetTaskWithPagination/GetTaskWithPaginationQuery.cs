using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SkeletonApi.Application.Extensions;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Task.Queries.GetTaskWithPagination
{
    public record GetTaskWithPaginationQuery : IRequest<PaginatedResult<GetTaskWithPaginationDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetTaskWithPaginationQuery() { }

        public GetTaskWithPaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetTaskWithPaginationQueryHandler : IRequestHandler<GetTaskWithPaginationQuery, PaginatedResult<GetTaskWithPaginationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTaskWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetTaskWithPaginationDto>> Handle(GetTaskWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<SettingTask>().FindByCondition(x => x.DeletedAt == null)
            .OrderByDescending(x => x.UpdatedAt)
            .Select(o => new GetTaskWithPaginationDto
            {
                Id = o.Id,
                Zone = o.Operator.Zone.Name,
                TypeName = o.Operator.Zone.Types.Where(t => t.ZoneId == o.Operator.ZoneId).Select(u => u.Name).FirstOrDefault(),
                OperatorNumber = o.Operator.Name,
                TaskNumber = o.TaskNo,
                TaskName = o.TaskName,
                DurationTask = o.TaskDuration
            })
            .ProjectTo<GetTaskWithPaginationDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}