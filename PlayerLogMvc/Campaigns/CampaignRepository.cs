using Microsoft.EntityFrameworkCore;
using PlayerLogMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerLogMvc.Campaigns
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ApplicationDbContext _db;

        public CampaignRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateAsync(Campaign entity)
        {
            await _db.Campaigns.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Campaign entity)
        {
            _db.Campaigns.Remove(entity);
            return await SaveAsync();
        }

        public async Task<IList<Campaign>> FindAllAsync()
        {
            return await _db.Campaigns
                .Include(t => t.Npcs)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Campaign> FindByIdAsync(int id)
        {
            return await _db.Campaigns
                .Include(t => t.Npcs)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.CampaignId == id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Campaign entity)
        {
            _db.Campaigns.Update(entity);
            return await SaveAsync();
        }
    }
}
