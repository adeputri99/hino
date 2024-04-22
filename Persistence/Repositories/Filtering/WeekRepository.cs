using SkeletonApi.Application.Features.MonitoringSystems.Queries.GetOkOrNG;
using SkeletonApi.Application.Interfaces.Repositories;
using SkeletonApi.Application.Interfaces.Repositories.Filtering;
using SkeletonApi.Domain.Entities.Tsdb;
using System.Globalization;

namespace SkeletonApi.Persistence.Repositories.Filtering
{
    public class WeekRepository : IWeekRepository
    {
        private readonly IDapperReadDbConnection _dapperReadDbConnection;

        public WeekRepository(IDapperReadDbConnection dapperReadDbConnection)
        {
            _dapperReadDbConnection = dapperReadDbConnection;
        }

        public async Task<OkOrNgDto> GetOkOrNgWeek(string view, DateTime? startTime, DateTime? endTime)
        {
            var data = new OkOrNgDto();
            var okOrNG = await _dapperReadDbConnection.QueryAsync<DeviceData>
                ($@"SELECT * FROM {view} WHERE id like '%K6%'
                AND date_trunc('week', date_time) >= date_trunc('week', @starttime::date)
                AND date_trunc('week', date_time) <= date_trunc('week', @endtime::date)
                ORDER BY date_time DESC",
                new { starttime = startTime.Value.Date, endtime = endTime.Value.Date });

            var totals = okOrNG
                 .GroupBy(d => new
                 {
                     WeekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(d.DateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)
                 })
                   .Select(g => new
                   {
                       date_group = new DateTime(g.Key.WeekNumber, 1, 1).AddDays((g.Key.WeekNumber - 1) * 7),
                       total_last = g.Sum(d => Convert.ToDecimal(d.Value))
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
                         Value = val.total_last,
                         Label = "Week " + CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(val.date_group, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday).ToString(),
                         DateTime = val.date_group,
                     }).OrderByDescending(x => x.DateTime).ToList()
                 };
            }
            return data;
        }
    }
}