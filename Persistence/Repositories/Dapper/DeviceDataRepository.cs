using Dapper;
using SkeletonApi.Application.DTOs.RestApiData;
using SkeletonApi.Application.Interfaces;
using SkeletonApi.Application.Interfaces.Repositories.Configuration.Dapper;
using SkeletonApi.Domain.Entities.Tsdb;
using SkeletonApi.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkeletonApi.Persistence.Repositories.Dapper
{
    public class DeviceDataRepository : IDiviceDateRepository
    {
        private readonly IDapperCreateUnitOfWork _dapperUwow;
        private readonly IGetConnection _getConnection;
        private readonly IRestApiClientService _restApiClient;

        public DeviceDataRepository (DapperUnitOfWorkContext dapperUwow, IRestApiClientService restClient)
        {
            _dapperUwow = dapperUwow;
            _getConnection = dapperUwow;
            _restApiClient = restClient;
        }

        public async Task Creates(IEnumerable<MqttRawValueEntity> mqttRawValues)
        {
            try
            {
                using (var uwow = _dapperUwow.Create())
                {
                    var connection = _getConnection.GetConnection();

                    foreach (var row in mqttRawValues)
                    {
                        var deviceData = new DeviceData();
                        deviceData.Id = row.Vid;
                        deviceData.Value = Convert.ToString(row.Value);
                        deviceData.Quality = true;
                        deviceData.Time = row.Time;
                        deviceData.DateTime = row.Datetime;
                       
                        string query = @"insert into ""DeviceData"" (id,value,quality,time,date_time) values (@Id,@Value,@Quality,@Time,@DateTime)";
                        await connection.ExecuteAsync(query, deviceData);                      
                    }
                    await uwow.CommitAsync();
                    uwow.Dispose();
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(JsonSerializer.Serialize(ex.Message));
            }

            await Task.CompletedTask;
        }

    }
}
