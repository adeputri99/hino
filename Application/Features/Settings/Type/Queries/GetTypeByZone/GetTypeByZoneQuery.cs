using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Type.Queries.GetTypeByZone
{
    public record GetTypeByZoneQuery : IRequest<Result<IEnumerable<GetTypeByZoneDto>>>
    {
        public Guid Id { get; set; }

        public GetTypeByZoneQuery(Guid id)
        {
            Id = id;
        }
    }

    internal class GetTypeByZoneQueryHandler : IRequestHandler<GetTypeByZoneQuery, Result<IEnumerable<GetTypeByZoneDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTypeByZoneQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetTypeByZoneDto>>> Handle(GetTypeByZoneQuery request, CancellationToken cancellationToken)
        {
            var type = await _unitOfWork.Repository<Types>().FindByCondition(o => o.ZoneId == request.Id)
                          .Select(x => new GetTypeByZoneDto { Name = x.TypeName })
                          .ProjectTo<GetTypeByZoneDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return await Result<IEnumerable<GetTypeByZoneDto>>.SuccessAsync(type, "Success");
        }
    }
}