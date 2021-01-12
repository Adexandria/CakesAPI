using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.UI;
using CakeAPI.Model;
using CakeAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CakeAPI.Controllers
{
    [ApiController]
    [Route("api/{name}/order")]
    public class OrderController : Controller
    {
        private readonly ICake cake;
        static string key = Environment.GetEnvironmentVariable("Public_Key");
        static string endpoint = Environment.GetEnvironmentVariable("End_Point");
        static string urlBase = endpoint + "transaction";
        public OrderController(ICake cake)
        {
            this.cake = cake;
        }
      [HttpPost]
      public async Task<ActionResult<Reciept>> OrderCake(string name,OrderTemplate model) 
        {
            if(name == null) 
            {
                return NotFound("Enter name of cake");
            }
            var getcake = await cake.GetCake(name);
            model.Amount = getcake.Price;
            var reference =   await InitializeTransaction(model);
            var newcontent =await Verify(reference);
            Reciept reciept = JsonConvert.DeserializeObject<Reciept>(newcontent);
            if(reciept != null) 
            {
                return Ok(reciept);

            }
            return BadRequest("Try again");
        }

        private async Task<string> Verify(string reference)
        {
            try
            {
                var client = GetClient();
                var url = urlBase + "/verify/" + reference;
                HttpResponseMessage httpResponse;
                httpResponse = await client.GetAsync(url);
                return await GetContent(httpResponse);
            }
            catch (Exception e)
            {

                return e.Message;
            }
            

        }
       
        private async Task<string> InitializeTransaction(OrderTemplate model) 
        {
            try {
                var client = GetClient();
                var url = urlBase + "/initialize";
                HttpResponseMessage httpResponse;
                var json = JsonConvert.SerializeObject(model);
                using (StringContent content = new StringContent(json))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    httpResponse = await client.PostAsync(url, content);

                }
                var newContent = await GetContent(httpResponse);
                Verification verify = JsonConvert.DeserializeObject<Verification>(newContent);
                return verify.Data.Reference;
            } 
            catch(Exception e)
            {
                return e.Message;
            }
           
        }
        private HttpClient GetClient() 
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", key);
            return client;
        }
        private async Task<string> GetContent(HttpResponseMessage httpResponse)
        {
            string contentString = await httpResponse.Content.ReadAsStringAsync();
            var newContent = JToken.Parse(contentString).ToString();
            return newContent;
        }
    }
}
