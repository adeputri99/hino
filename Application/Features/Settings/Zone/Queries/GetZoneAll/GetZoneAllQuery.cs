using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.Settings.Zone.Queries.GetZoneAll
{
    public record GetZoneAllQuery : IRequest<Result<List<GetZoneAllDto>>>;

    internal class GetAllMachineQueryHandler : IRequestHandler<GetZoneAllQuery, Result<List<GetZoneAllDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMachineQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetZoneAllDto>>> Handle(GetZoneAllQuery query, CancellationToken cancellationToken)
        {
            var type = await _unitOfWork.Repository<Zones>().Entities
                            .ProjectTo<GetZoneAllDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

            return await Result<List<GetZoneAllDto>>.SuccessAsync(type, "Successfully fetch data");
        }
    }
}