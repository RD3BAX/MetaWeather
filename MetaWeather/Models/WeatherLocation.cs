using System.Text.Json.Serialization;
using MetaWeather.Service;

namespace MetaWeather.Models
{
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
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LocationType Type { get; set; }

        /// <summary>
        /// floats, comma separated
        /// </summary>
        [JsonPropertyName("latt_long")]
        [JsonConverter(typeof(JsonCoordinateConverter))]
        public (double Latitude, double Longitude) Location { get; set; }

        /// <summary>
        /// Only returned on a lattlong search
        /// </summary>
        [JsonPropertyName("distance")]
        public int Distance { get; set; }
    }
}