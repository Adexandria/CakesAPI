using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeAPI.Model
{
    public class OrderTemplate
    {
        public Guid Id { get; }
        public string Email { get; set; }
        public int Amount { get; set; }
    }
}
