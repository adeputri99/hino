using SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG;

namespace SkeletonApi.Application.Interfaces.Repositories.Filtering
{
    public interface IWeekRepository
    {
        Task<OkOrNgDto> GetOkOrNgWeek(string view, DateTime? startTime, DateTime? endTime);
    }
}