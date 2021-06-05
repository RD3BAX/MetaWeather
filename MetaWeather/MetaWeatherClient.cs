using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using MetaWeather.Models;

namespace MetaWeather
{
    /// <summary>
    /// Класс для работы с HTTP клиентом
    /// </summary>
    public class MetaWeatherClient
    {
        #region Поля

        private readonly HttpClient _client;

        #endregion // Поля

        #region Методы

        //private static readonly JsonSerializerOptions __JsonOptions = new()
        //{
        //    Converters =
        //    {
        //        new JsonStringEnumConverter(),
        //        new JsonCoordinateConverter()
        //    }
        //};

        /// <summary>
        /// Метод получения географических координат по названию населенного пункта
        /// </summary>
        /// <param name="Name">Населенный пункт</param>
        /// <returns></returns>
        public async Task<WeatherLocation[]> GetLocation(string Name, CancellationToken Cancel = default)
        {
            return await _client
                .GetFromJsonAsync<WeatherLocation[]>($"/api/location/search/?query={Name}", /*__JsonOptions,*/ Cancel)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Получение места по координатам
        /// </summary>
        /// <param name="Location"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<WeatherLocation[]> GetLocation((double Latitude, double Longitude) Location, CancellationToken Cancel = default)
        {
            return await _client
                .GetFromJsonAsync<WeatherLocation[]>($"/api/location/search/?lattlong={Location.Latitude.ToString(CultureInfo.InvariantCulture)},{Location.Longitude.ToString(CultureInfo.InvariantCulture)}", Cancel)
                .ConfigureAwait(false);
        }

        public async Task<LocationInfo> GetInfo(int WoeId, CancellationToken Cancel = default)
        {
            return await _client.GetFromJsonAsync<LocationInfo>($"/api/location/{WoeId}/", Cancel)
                .ConfigureAwait(default);
        }

        public Task<LocationInfo> GetInfo(WeatherLocation Location, CancellationToken Cancel = default) =>
            GetInfo(Location.Id, Cancel);

        public async Task<WeatherInfo[]> GetWeather(int WoeId, DateTime Time, CancellationToken Cancel = default)
        {
            return await _client.GetFromJsonAsync<WeatherInfo[]>($"/api/location/{WoeId}/{Time:yyyy}/{Time:MM}/{Time:dd}/", Cancel)
                .ConfigureAwait(default);
        }

        #endregion // Методы

        #region Конструктор

        public MetaWeatherClient(HttpClient Client) => _client = Client;

        #endregion // Конструктор
    }
}
