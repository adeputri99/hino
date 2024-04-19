using Microsoft.EntityFrameworkCore;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonApi.Persistence.Repositories
{
    public class RepairRepository : IRepairRepository
    {
        private readonly IGenericRepository<Repair> _repository;

        public RepairRepository(IGenericRepository<Repair> repository)
        {
            _repository = repository;
        }

        public void DeleteMachines(Repair repair) => _repository.DeleteAsync(repair);

        public async Task<bool> ValidateData(Repair repairs)
        {
            var x = await _repository.Entities.Where(o => repairs.FrameNumber.ToLower() == o.FrameNumber.ToLower()).CountAsync();
            if (x > 0)
            {
                return false;
            }
            return true;
        }
    }
}
