using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CakeAPI.Model;
using CakeAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CakeAPI.Controllers
{
    [ApiController]
    [Route("api/{name}/charges")]
    public class ChargeController : Controller
    {
        private readonly ICake cake;
        static Verification verification;
        static string key = Environment.GetEnvironmentVariable("Public_Key");
        static string endpoint = Environment.GetEnvironmentVariable("End_Point");
        static string urlBase = endpoint + "charge";
        public ChargeController(ICake cake)
        {
            this.cake = cake;
        }
        [HttpPost]
        public async Task<ActionResult<string>> PayCharge(string name,ChargesTemplate charges) 
        {
            if(name == null) 
            {
                return NotFound();
            }
            var getcake = await cake.GetCake(name);
            charges.Amount = getcake.Price;
            var newcharge = await InitializeCharge(charges);
            verification = JsonConvert.DeserializeObject<Verification>(newcharge);
            return Ok(verification.Data.Reference);
        }
        [HttpPost("{reference}")]
        public async Task<ActionResult<string>> SubmitPin(PinTemplate pin)
        {
            var receipt = await Submit_Pin(pin);
            return receipt;
        }
        [HttpPost("pay")]
        public async Task<ActionResult<Reciept>> SubmitOTP(OTPTemplate otp)
        {
            var receipt = await Submit(otp);
            Reciept reciepts = JsonConvert.DeserializeObject<Reciept>(receipt);
            return reciepts;
        }
        private async Task<string> Submit_Pin(PinTemplate pin)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", key);
            var url = urlBase + "/submit_pin";
            HttpResponseMessage httpResponse;
            var json = JsonConvert.SerializeObject(pin);
            using (StringContent content = new StringContent(json))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpResponse = await client.PostAsync(url, content);
            }
            string contentString = await httpResponse.Content.ReadAsStringAsync();
            var newContent = JToken.Parse(contentString).ToString();
            return newContent;
        }
        private async Task<string> Submit(OTPTemplate otp) 
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", key);
            var url = urlBase + "/submit_otp";
            HttpResponseMessage httpResponse;
            var json = JsonConvert.SerializeObject(otp);
            using (StringContent content = new StringContent(json))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpResponse = await client.PostAsync(url, content);
            }
            string contentString = await httpResponse.Content.ReadAsStringAsync();
            var newContent = JToken.Parse(contentString).ToString();
            return newContent;
        }
        private async Task<string> InitializeCharge(ChargesTemplate charges) 
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", key);
            var url = urlBase;
            HttpResponseMessage httpResponse;
            var json = JsonConvert.SerializeObject(charges);
            using(StringContent content = new StringContent(json)) 
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpResponse = await client.PostAsync(url, content);
            }
            string contentString = await httpResponse.Content.ReadAsStringAsync();
            var newContent = JToken.Parse(contentString).ToString();
            return newContent;

        }
    }
}
