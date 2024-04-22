using SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG;

namespace SkeletonApi.Application.Interfaces.Repositories.Filtering
{
    public interface IMonthRepository
    {
        Task<OkOrNgDto> GetOkOrNgMonth(string view, DateTime? startTime, DateTime? endTime);
    }
}