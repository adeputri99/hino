using SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonApi.Application.Interfaces.Repositories.Filtering
{
    public interface IDefaultRepository
    {
        Task<OkOrNgDto> GetOkOrNgDefault(string view);
    }
}
