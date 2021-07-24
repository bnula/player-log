using Microsoft.EntityFrameworkCore;
using PlayerLogMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerLogMvc.Campaign
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
            await _db.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Campaign entity)
        {
            _db.Remove(entity);
            return await SaveAsync();
        }

        public async Task<IList<Campaign>> FindAllAsync()
        {
            return await _db.Campaigns.ToListAsync();
        }

        public async Task<Campaign> FindByIdAsync(int id)
        {
            return await _db.Campaigns.FindAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Campaign entity)
        {
            _db.Update(entity);
            return await SaveAsync();
        }
    }
}
