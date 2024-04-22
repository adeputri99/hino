using SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG;

namespace SkeletonApi.Application.Interfaces.Repositories.Filtering
{
    public interface IDefaultRepository
    {
        Task<OkOrNgDto> GetOkOrNgDefault(string view);
    }
}