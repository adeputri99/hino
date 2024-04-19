using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Operator.Queries.GetOperatorByZone
{
    public record GetOperatorByZoneQuery : IRequest<Result<IEnumerable<GetOperatorByZoneDto>>>
    {
        public Guid Id { get; set; }

        public GetOperatorByZoneQuery(Guid id)
        {
            Id = id;
        }
    }

    internal class GetTypeByZoneQueryHandler : IRequestHandler<GetOperatorByZoneQuery, Result<IEnumerable<GetOperatorByZoneDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTypeByZoneQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetOperatorByZoneDto>>> Handle(GetOperatorByZoneQuery request, CancellationToken cancellationToken)
        {
            var type = await _unitOfWork.Repository<Operators>().FindByCondition(o => o.ZoneId == request.Id)
                          .Select(x => new GetOperatorByZoneDto { Name = x.Name })
                          .ProjectTo<GetOperatorByZoneDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return await Result<IEnumerable<GetOperatorByZoneDto>>.SuccessAsync(type, "Success");
        }
    }
}