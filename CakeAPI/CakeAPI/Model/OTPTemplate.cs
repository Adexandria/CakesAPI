using Newtonsoft.Json;

namespace CakeAPI.Model

{
    public class OTPTemplate
    {
        [JsonProperty("otp")]
        public string OTP { get; set; }
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
}