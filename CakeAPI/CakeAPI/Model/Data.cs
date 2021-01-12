using Newtonsoft.Json;

namespace CakeAPI.Model
{
    public class Data
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
}