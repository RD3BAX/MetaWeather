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
        public async Task<WeatherLocation[]> GetLocationByName(string Name, CancellationToken Cancel = default)
        {
            return await _client
                .GetFromJsonAsync<WeatherLocation[]>($"/api/location/search/?query={Name}", /*__JsonOptions,*/ Cancel)
                .ConfigureAwait(false);
        }

        #endregion // Методы

        #region Конструктор

        public MetaWeatherClient(HttpClient Client) => _client = Client;

        #endregion // Конструктор
    }
}
