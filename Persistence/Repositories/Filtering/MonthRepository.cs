using SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Application.Interfaces.Repositories.Filtering;
using SkeletonApi.Domain.Entities.Tsdb;

namespace SkeletonApi.Persistence.Repositories.Filtering
{
    public class MonthRepository : IMonthRepository
    {
        private readonly IDapperReadDbConnection _dapperReadDbConnection;

        public MonthRepository(IDapperReadDbConnection dapperReadDbConnection)
        {
            _dapperReadDbConnection = dapperReadDbConnection;
        }

        public async Task<OkOrNgDto> GetOkOrNgMonth(string view, DateTime? startTime, DateTime? endTime)
        {
            var data = new OkOrNgDto();
            var okOrNg = await _dapperReadDbConnection.QueryAsync<DeviceData>
                ($@"SELECT * FROM {view} WHERE id like '%K6%'
                AND date_trunc('month', date_time) >= date_trunc('month', @starttime::date)
                AND date_trunc('month', date_time) <= date_trunc('month', @endtime::date)
                ORDER BY date_time DESC",
                new { starttime = startTime.Value.Date, endtime = endTime.Value.Date });

            var totals = okOrNg.GroupBy(p => new { p.DateTime.Year, p.DateTime.Month }).Select(g => new
            {
                date_time = new DateTime(g.Key.Year, g.Key.Month, 1),
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
                         Label = val.date_time.AddHours(7).ToString("mmmm"),
                         DateTime = val.date_time,
                     }).OrderByDescending(x => x.DateTime).ToList()
                 };
            }
            return data;
        }
    }
}