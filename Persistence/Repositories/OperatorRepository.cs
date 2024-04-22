using Microsoft.EntityFrameworkCore;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Persistence.Repositories
{
    public class OperatorRepository : IOperatorRepository
    {
        private readonly IGenericRepository<Operators> _repository;

        public OperatorRepository(IGenericRepository<Operators> repository)
        {
            _repository = repository;
        }

        public async Task<bool> ValidateData(Operators operators)
        {
            var x = await _repository.FindByCondition(o => operators.Name.ToLower() == o.Name.ToLower() && operators.ZoneId == o.ZoneId).CountAsync();
            if (x > 0)
            {
                return false;
            }
            return true;
        }
    }
}