using CakeAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeAPI.Services
{
    public class CakeRepos : ICake
    {
        private readonly Dbcontext db;
        public CakeRepos(Dbcontext db)
        {
            this.db = db;
        }
        public IEnumerable<CakeModel> GetCakes 
        {
            get 
            {
                return db.Cakes.OrderBy(i => i.Name);
            }
        }
        public async Task<CakeModel> Add(CakeModel cake)
        {
            if(cake == null) {
                throw new NullReferenceException(nameof(cake));
            }
            cake.Id = new Guid();
            await db.AddAsync(cake);
            return cake;
        }

        public async Task<int> Delete(string id)
        {
            if (id == null) 
            {
                throw new NullReferenceException(nameof(id));
            }
            var cake = await GetCake(id);
            db.Cakes.Remove(cake);
            return await Save();
        }

        public async Task<CakeModel> GetCake(string cake)
        {
            if(cake == null) 
            {
                throw new NullReferenceException(nameof(cake));
            }
            return await db.Cakes.Where(i => i.Name == cake).FirstOrDefaultAsync();
        }

        public Task<int> Save()
        {
            return db.SaveChangesAsync();
        }
    }
}
