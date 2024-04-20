using SkeletonApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonApi.Application.Interfaces.Repositories
{
    public interface IRepairRepository
    {
        Task<bool> ValidateData(Repair repair);

        void DeleteMachines(Repair repairs);
    }
}
