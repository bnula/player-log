using Microsoft.EntityFrameworkCore;
using PlayerLogMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Armies
{
    public class ArmyRepository : IArmyRepository
    {
        private readonly ApplicationDbContext _db;

        public ArmyRepository(ApplicationDbContext _db)
        {
            this._db = _db;
        }
        public async Task<bool> CreateAsync(Army entity)
        {
            await _db.Armies.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Army entity)
        {
            _db.Armies.Remove(entity);
            return await SaveAsync();
        }

        public async Task<IEnumerable<Army>> FindAllAsync()
        {
            var items = await _db.Armies.ToListAsync();
            return items;
        }

        public async Task<Army> FindByIdAsync(int id)
        {
            var item = await _db.Armies.FirstOrDefaultAsync(i => i.ArmyId == id);
            return item;
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Army entity)
        {
            _db.Armies.Update(entity);
            return await SaveAsync();
        }
    }
}
