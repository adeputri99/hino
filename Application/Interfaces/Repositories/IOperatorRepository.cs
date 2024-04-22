using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Interfaces.Repositories
{
    public interface IOperatorRepository
    {
        Task<bool> ValidateData(Operators operators);
    }
}