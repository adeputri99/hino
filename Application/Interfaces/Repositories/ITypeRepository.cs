using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Interfaces.Repositories
{
    public interface ITypeRepository
    {
        Task<bool> ValidateData(Types machines);
    }
}