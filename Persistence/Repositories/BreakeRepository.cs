using Microsoft.EntityFrameworkCore;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Persistence.Repositories
{
    public class BreakeRepository : IBreakeRepository
    {
        private readonly IGenericRepository<SettingBreak> _repository;

        public BreakeRepository(IGenericRepository<SettingBreak> repository)
        {
            _repository = repository;
        }

        public async Task<bool> ValidateData(SettingBreak breaks)
        {
            var x = await _repository.FindByCondition(o => o.BreakeName == breaks.BreakeName).CountAsync();
            if (x > 0)
            {
                return false;
            }
            return true;
        }
    }
}