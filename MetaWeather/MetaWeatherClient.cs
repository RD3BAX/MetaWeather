using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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

        /// <summary>
        /// Метод получения географических координат по названию населенного пункта
        /// </summary>
        /// <param name="Name">Населенный пункт</param>
        /// <returns></returns>
        public async Task<WeatherLocation[]> GetLocationByName(string Name)
        {
            return await _client.GetFromJsonAsync<WeatherLocation[]>($"/api/location/search/?query={Name}");
        }

        #endregion // Методы

        #region Конструктор

        public MetaWeatherClient(HttpClient Client) => _client = Client;

        #endregion // Конструктор
    }

    public class WeatherLocation
    {
        /// <summary>
        /// Where On Earth ID
        /// </summary>
        public int woeid { get; set; }

        /// <summary>
        /// Name of the location
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// (City|Region / State / Province|Country|Continent)
        /// </summary>
        public string location_type { get; set; }

        /// <summary>
        /// floats, comma separated
        /// </summary>
        public string latt_long { get; set; }

        /// <summary>
        /// Only returned on a lattlong search
        /// </summary>
        public int distance { get; set; }
    }
}
