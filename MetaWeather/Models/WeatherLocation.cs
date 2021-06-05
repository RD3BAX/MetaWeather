using System.Text.Json.Serialization;

namespace MetaWeather.Models
{
    public class WeatherLocation : Location
    {
        /// <summary>
        /// Only returned on a lattlong search
        /// </summary>
        [JsonPropertyName("distance")]
        public int Distance { get; set; }

        public override string ToString() => $"{Title}[{Id}]({Type}):{Coordinates} ({Distance})";
    }
}