using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeAPI.Model
{
    public class ChargesTemplate
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("bank")]
        public Bank Bank { get; set; }
        [JsonProperty("birthday")]
        public DateTime Birthday { get; set; }
    }
}
