using CakeAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CakeAPI
{
    public class Dbcontext:DbContext
    {
        public Dbcontext(DbContextOptions<Dbcontext> options):base(options)
        {

        }
        public DbSet<CakeModel> Cakes { get; set; }
    }
}