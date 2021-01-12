using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeAPI.Model
{
    public class PinTemplate
    {
        [JsonProperty("pin")]
        public string Pin { get; set; }
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
}
