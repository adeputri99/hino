using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Application.Interfaces.Repositories
{
    public interface IRepairRepository
    {
        Task<bool> ValidateData(Repair repair);

        void DeleteMachines(Repair repairs);
    }
}