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
            if(verification != null) 
            {
                return Ok(verification.Data.Reference);
            }
            return BadRequest("Try again");
        }

        [HttpPost("{reference}")]
        public async Task<ActionResult<string>> SubmitPin(PinTemplate pin)
        {
            var receipt = await Submit_Pin(pin);
            return receipt;
        }

        [HttpPost("{reference}/pay")]
        public async Task<ActionResult<Reciept>> SubmitOTP(OTPTemplate otp)
        {
            var receipt = await Submit(otp);
            Reciept reciepts = JsonConvert.DeserializeObject<Reciept>(receipt);
            return reciepts;
        }


        private async Task<string> Submit_Pin(PinTemplate pin)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", key);
                var url = urlBase + "/submit_pin";
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var json = JsonConvert.SerializeObject(pin);
                return await GetContent(httpResponse, json, url, client);
            }
            catch (Exception e)
            { 
                return e.Message;
            }
            
        }

        private async Task<string> Submit(OTPTemplate otp) 
        {
           try
           { 
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", key);
            var url = urlBase + "/submit_otp";
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            var json = JsonConvert.SerializeObject(otp);
            return await GetContent(httpResponse, json, url, client);
           }
           catch (Exception e)
           {

                return e.Message;
           }
        }

        private async Task<string> InitializeCharge(ChargesTemplate charges)
        {
            try
            {
                var client = GetClient();
                var url = urlBase;
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                var json = JsonConvert.SerializeObject(charges);
                return await GetContent(httpResponse, json, url, client);
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        private async Task<string> GetContent(HttpResponseMessage httpResponse,string json,string url,HttpClient client)
        { 
            using (StringContent content = new StringContent(json))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpResponse = await client.PostAsync(url, content);
            }
            string contentString = await httpResponse.Content.ReadAsStringAsync();
            var newContent = JToken.Parse(contentString).ToString();
            return newContent;
        }

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", key);
            return client;
        }
    }
}
