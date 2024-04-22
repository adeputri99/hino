using SkeletonApi.Application.Interfaces.Repositories;
using System.Data;

namespace SkeletonApi.Persistence.Repositories.Configuration
{
    public interface IDapperWriteDbConnection : IDapperReadDbConnection
    {
        Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);
    }
}