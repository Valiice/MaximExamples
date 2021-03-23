using Syntra.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Syntra.MVCAdvanced.Services.Interfaces
{
    public interface ILocationDbService
    {
        Task<Location> CreateAsync(Location locationToCreate);
        Task DeleteAsync(int id);
        Task<List<Location>> GetListAsync();
        Task<Location> GetOneAsync(int? id);
        Task<Location> UpdateAsync(Location locationToSave);
    }
}