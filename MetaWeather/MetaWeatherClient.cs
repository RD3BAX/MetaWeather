using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
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

        private static readonly JsonSerializerOptions __JsonOptions = new()
        {
            Converters =
            {
                new JsonStringEnumConverter(),
                new JsonCoordinateConverter()
            }
        };

        /// <summary>
        /// Метод получения географических координат по названию населенного пункта
        /// </summary>
        /// <param name="Name">Населенный пункт</param>
        /// <returns></returns>
        public async Task<WeatherLocation[]> GetLocationByName(string Name, CancellationToken Cancel = default)
        {
            return await _client
                .GetFromJsonAsync<WeatherLocation[]>($"/api/location/search/?query={Name}", __JsonOptions, Cancel)
                .ConfigureAwait(false);
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
        [JsonPropertyName("woeid")]
        public int Id { get; set; }

        /// <summary>
        /// Name of the location
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// (City|Region / State / Province|Country|Continent)
        /// </summary>
        [JsonPropertyName("location_type")]
        public LocationType Type { get; set; }

        /// <summary>
        /// floats, comma separated
        /// </summary>
        [JsonPropertyName("latt_long")]
        public (double Latitude, double Longitude) Location { get; set; }

        /// <summary>
        /// Only returned on a lattlong search
        /// </summary>
        [JsonPropertyName("distance")]
        public int Distance { get; set; }
    }

    public enum LocationType
    {
        City,
        Region,
        State,
        Province,
        Country,
        Continent
    }

    internal class JsonCoordinateConverter : JsonConverter<(double Latitude, double Longitude)>
    {
        public override (double Latitude, double Longitude) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.GetString() is not {Length: >= 3} str) return (double.NaN, double.NaN);
            if(str.Split(',') is not {Length: 2} components) return (double.NaN, double.NaN);
            if(!double.TryParse(components[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var lat)) return (double.NaN, double.NaN);
            if(!double.TryParse(components[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var lon)) return (double.NaN, double.NaN);

            return (lat, lon);
        }

        public override void Write(Utf8JsonWriter writer, (double Latitude, double Longitude) value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"{value.Latitude},{value.Longitude}");
        }
    }
}
