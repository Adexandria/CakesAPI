using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeAPI.Model
{
    public class Reciept
    {
        [JsonProperty("data")]
        public RecieptData Data { get; set; }
    }
}
