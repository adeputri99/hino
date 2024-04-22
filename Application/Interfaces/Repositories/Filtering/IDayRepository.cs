using SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG;

namespace SkeletonApi.Application.Interfaces.Repositories.Filtering
{
    public interface IDayRepository
    {
        Task<OkOrNgDto> GetOkOrNgDay(string view, DateTime? startTime, DateTime? endTime);
    }
}