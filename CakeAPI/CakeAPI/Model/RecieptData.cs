﻿using Newtonsoft.Json;

namespace CakeAPI.Model
{
    public class RecieptData
    {
        [JsonProperty("Reference")]
        public string Reference { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("gateway_response")]
        public string Response { get; set; }
    }
}