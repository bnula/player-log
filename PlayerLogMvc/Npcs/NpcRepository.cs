using Microsoft.EntityFrameworkCore;
using PlayerLogMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Npcs
{
    public class NpcRepository : INpcRepository
    {
        private readonly ApplicationDbContext _db;

        public NpcRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(Npc entity)
        {
            await _db.Npcs.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Npc entity)
        {
            _db.Npcs.Remove(entity);
            return await SaveAsync();
        }

        public async Task<IEnumerable<Npc>> FindAllAsync()
        {
            var items = await _db.Npcs
                .Include(r => r.Campaign)
                .ToListAsync();
            return items;
        }

        public async Task<Npc> FindByIdAsync(int id)
        {
            var item = await _db.Npcs
                .Include(r => r.Campaign)
                .FirstOrDefaultAsync(i => i.NpcId == id);
            return item;
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Npc entity)
        {
            _db.Npcs.Update(entity);
            return await SaveAsync();
        }
    }
}
