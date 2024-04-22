using SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Application.Interfaces.Repositories.Filtering;
using SkeletonApi.Domain.Entities.Tsdb;

namespace SkeletonApi.Persistence.Repositories.Filtering
{
    public class DefaultRepository : IDefaultRepository
    {
        private readonly IDapperReadDbConnection _dapperReadDbConnection;

        public DefaultRepository(IDapperReadDbConnection dapperReadDbConnection)
        {
            _dapperReadDbConnection = dapperReadDbConnection;
        }

        public async Task<OkOrNgDto> GetOkOrNgDefault(string view)
        {
            var data = new OkOrNgDto();
            var okOrNG = await _dapperReadDbConnection.QueryAsync<DeviceData>
                ($@"SELECT * FROM {view} WHERE id like '%K6%'
                AND date_trunc('week', date_time) = date_trunc('week', now())
                ORDER BY date_time DESC");

            var totals = okOrNG.GroupBy(p => new { p.DateTime.Year, p.DateTime.Month, p.DateTime.Day }).Select(g => new
            {
                date_time = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day),
                last = g.Select(p => p.Value).First()
            }).ToList();

            if (okOrNG.Count() == 0)
            {
                data = new OkOrNgDto
                {
                    Vid = "K6"
                };
            }
            else
            {
                data =
                 new OkOrNgDto
                 {
                     Vid = "K6",
                     Data = totals.Select(val => new Data
                     {
                         Value = Convert.ToDecimal(val.last),
                         Label = val.date_time.AddHours(7).ToString("ddd"),
                         DateTime = val.date_time,
                     }).OrderByDescending(x => x.DateTime).ToList()
                 };
            }
            return data;
        }
    }
}