using Newtonsoft.Json;

namespace Parcial.Shared.Responses
{
    public class EventControlResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("fechaDeUso")]
        public DateTime? FechaDeUso { get; set; }

        [JsonProperty("iso2")]
        public string? Iso2 { get; set; }
    }
}
