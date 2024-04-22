using SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG;

namespace SkeletonApi.Application.Interfaces.Repositories.Filtering
{
    public interface IYearRepository
    {
        Task<OkOrNgDto> GetOkOrNgYear(string view, DateTime? startTime, DateTime? endTime);
    }
}