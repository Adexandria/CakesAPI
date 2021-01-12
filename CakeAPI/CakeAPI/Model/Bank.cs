using Newtonsoft.Json;

namespace CakeAPI.Model
{
    public class Bank
    {
        [JsonProperty("account_number")]
        public string AccountNo { get; set; }
        [JsonProperty("code")]
        public string CVV { get; set; }
    }
}