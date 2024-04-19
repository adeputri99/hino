using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Interfaces.Repositories
{
    public interface IBreakeRepository
    {
        Task<bool> ValidateData(SettingBreak breaks);
    }
}