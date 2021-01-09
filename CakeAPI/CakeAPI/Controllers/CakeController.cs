using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CakeAPI.Dto;
using CakeAPI.Model;
using CakeAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CakeAPI.Controllers
{
    [ApiController]
    [Route("api/cakes")]
    public class CakeController : ControllerBase
    {
        private readonly ICake cake;
        private readonly IMapper mapper;
        public CakeController(ICake cake, IMapper mapper)
        {
            this.cake = cake;
            this.mapper = mapper;
        }

        [HttpGet]
        //Get Collection of Cakes
        public ActionResult GetCakes()
        {
            var cakes = cake.GetCakes;
            if (cakes == null)
            {
                return NoContent();
            }
            return Ok(mapper.Map<IEnumerable<CakeDto>>(cakes));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CakeDto>> GetCake(string cakes)
        {
            if (cakes == null) {
                return NotFound();
            }
            var newcake = await cake.GetCake(cakes);
            return Ok(mapper.Map<CakeDto>(newcake));
        }

        [HttpPost]
        public async Task<ActionResult<CakeDto>> Add(CakeCreateDTO cakeCreate)
        {
            if (cakeCreate == null)
            {
                return NotFound();
            }
            var cakes = mapper.Map<CakeModel>(cakeCreate);
            await cake.Add(cakes);
            await cake.Save();
            return Ok(mapper.Map<CakeDto>(cakes));
        }
        [HttpDelete("{id}")]
        public async  Task<ActionResult> Delete(string id) 
        {
            var cakes = await cake.Delete(id);
            return NoContent();
        
        }
    }
}
