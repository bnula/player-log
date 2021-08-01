using Microsoft.EntityFrameworkCore;
using PlayerLogMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Locations
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateAsync(Location entity)
        {
            await _db.Locations.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Location entity)
        {
            _db.Locations.Remove(entity);
            return await SaveAsync();
        }

        public async Task<IEnumerable<Location>> FindAllAsync()
        {
            var items = await _db.Locations
                .Include(l => l.Campaign)
                .ToListAsync();
            return items;
        }

        public async Task<Location> FindByIdAsync(int id)
        {
            var item = await _db.Locations
                .Where(l => l.LocationId == id)
                .Include(l => l.Campaign)
                .FirstOrDefaultAsync();
            return item;
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Location entity)
        {
            _db.Locations.Update(entity);
            return await SaveAsync();
        }
    }
}
