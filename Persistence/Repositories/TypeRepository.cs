using Microsoft.EntityFrameworkCore;
using SkeletonApi.Application.Features.Settings.Type.Queries.GetTypeByZone;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;

namespace SkeletonApi.Persistence.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly IGenericRepository<Types> _repository;
        public TypeRepository(IGenericRepository<Types> repository)
        {
            _repository = repository;
        }


        public async Task<bool> ValidateData(Types type)
        {
            var x = await _repository.Entities.Where(o => type.Name.ToLower() == o.Name.ToLower()).CountAsync();
            if (x > 0)
            {
                return false;
            }
            return true;
        }
    }
}