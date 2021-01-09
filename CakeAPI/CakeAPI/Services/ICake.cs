using CakeAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeAPI.Services
{
    public interface ICake
    {
        IEnumerable<CakeModel> GetCakes { get; }
        Task<CakeModel> Add(CakeModel cake);
        Task<CakeModel> GetCake(string cake);
        Task<int> Delete(string id);
        Task<int> Save();

    }
}
