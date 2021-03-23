using Microsoft.EntityFrameworkCore;
using Syntra.Models;
using Syntra.MVCAdvanced.DB;
using Syntra.MVCAdvanced.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Syntra.MVCAdvanced.Services
{
    public class LocationDbService : ILocationDbService
    {
        private readonly DanceSchoolDbContext _context;

        public LocationDbService(DanceSchoolDbContext context)
        {
            this._context = context;
        }
        public async Task<Location> GetOneAsync(int? id)
        {
            return await _context.Locations.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Location> UpdateAsync(Location locationToSave)
        {
            _context.Locations.Update(locationToSave);
            await _context.SaveChangesAsync();
            return locationToSave;
        }
        public async Task<List<Location>> GetListAsync()
        {
            var allLocations = await _context.Locations.ToListAsync();
            if (allLocations.Count() != 0)
                return allLocations;
            else
                throw new KeyNotFoundException("The locations don't exist");
        }
        public async Task<Location> CreateAsync(Location locationToCreate)
        {
            _context.Locations.Add(locationToCreate);
            await _context.SaveChangesAsync();
            return locationToCreate;
        }
        public async Task DeleteAsync(int id)
        {
            var locationToDelete = await _context.Locations.FirstOrDefaultAsync(x => x.Id == id);
            _context.Locations.Remove(locationToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
