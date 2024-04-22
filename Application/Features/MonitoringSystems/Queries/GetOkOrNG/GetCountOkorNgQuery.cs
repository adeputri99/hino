using MediatR;
using SkeletonApi.Application.Interfaces.Repositories.Filtering;
using SkeletonApi.Shared;

namespace SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG
{
    public record GetCountOkorNgQuery : IRequest<Result<OkOrNgDto>>
    {
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string View { get; set; }

        public GetCountOkorNgQuery(string type, DateTime startTime, DateTime endTime, string view)
        {
            Type = type;
            Start = startTime;
            End = endTime;
            View = view;
        }
        internal class GetCountOkorNgQueryHandler : IRequestHandler<GetCountOkorNgQuery, Result<OkOrNgDto>>
        {
            private readonly IDayRepository _dayRepository;
            private readonly IWeekRepository _weekRepository;
            private readonly IMonthRepository _monthRepository;
            private readonly IYearRepository _yearRepository;
            private readonly IDefaultRepository _defaultRepository;
            public GetCountOkorNgQueryHandler(IDayRepository dayRepository, IWeekRepository weekRepository, IMonthRepository monthRepository, IYearRepository yearRepository, IDefaultRepository defaultRepository)
            {
                _dayRepository = dayRepository;
                _weekRepository = weekRepository;
                _monthRepository = monthRepository;
                _yearRepository = yearRepository;
                _defaultRepository = defaultRepository;
            }
            public async Task<Result<OkOrNgDto>> Handle(GetCountOkorNgQuery request, CancellationToken cancellationToken)
            {
                if (request.Type == "day")
                {
                    var dt = await _dayRepository.GetOkOrNgDay(request.View, request.Start, request.End);
                    return await Result<OkOrNgDto>.SuccessAsync(dt, "Successfully fetch data");
                }
                else if (request.Type == "week")
                {
                    var dt = await _weekRepository.GetOkOrNgWeek(request.View, request.Start, request.End);
                    return await Result<OkOrNgDto>.SuccessAsync(dt, "Successfully fetch data");
                }
                else if (request.Type == "month")
                {
                    var dt = await _monthRepository.GetOkOrNgMonth(request.View, request.Start, request.End);
                    return await Result<OkOrNgDto>.SuccessAsync(dt, "Successfully fetch data");
                }
                else if (request.Type == "year")
                {
                    var dt = await _yearRepository.GetOkOrNgYear(request.View, request.Start, request.End);
                    return await Result<OkOrNgDto>.SuccessAsync(dt, "Successfully fetch data");
                }

                var defaultData = await _defaultRepository.GetOkOrNgDefault(request.View);
                return await Result<OkOrNgDto>.SuccessAsync(defaultData, "Successfully fetch data");
            }
        }
    }
}