using SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Application.Interfaces.Repositories.Filtering;
using SkeletonApi.Domain.Entities.Tsdb;

namespace SkeletonApi.Persistence.Repositories.Filtering
{
    public class YearRepository : IYearRepository
    {
        private readonly IDapperReadDbConnection _dapperReadDbConnection;

        public YearRepository(IDapperReadDbConnection dapperReadDbConnection)
        {
            _dapperReadDbConnection = dapperReadDbConnection;
        }

        public async Task<OkOrNgDto> GetOkOrNgYear(string view, DateTime? startTime, DateTime? endTime)
        {
            var data = new OkOrNgDto();
            var okOrNg = await _dapperReadDbConnection.QueryAsync<DeviceData>
                ($@"SELECT * FROM {view} WHERE id like '%K6%'
                AND date_trunc('year', date_time) >= date_trunc('year', @starttime::date)
                AND date_trunc('year', date_time) <= date_trunc('year', @endtime::date)
                ORDER BY date_time DESC",
                new { starttime = startTime.Value.Date, endtime = endTime.Value.Date });

            var totals = okOrNg.GroupBy(p => new { p.DateTime.Year }).Select(g => new
            {
                date_time = new DateTime(g.Key.Year, 1, 1),
                last = g.Select(p => p.Value).First()
            }).ToList();

            if (okOrNg.Count() == 0)
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
                         Label = val.date_time.AddHours(7).ToString("yyyy"),
                         DateTime = val.date_time,
                     }).OrderByDescending(x => x.DateTime).ToList()
                 };
            }
            return data;
        }
    }
}