using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CakeAPI.Dto
{
    public class CakeCreateDTO
    {
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Enter price")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Enter Description")]
        public string Description { get; set; }
    }
}
