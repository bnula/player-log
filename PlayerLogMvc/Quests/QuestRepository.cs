using Microsoft.EntityFrameworkCore;
using PlayerLogMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Quests
{
    public class QuestRepository : IQuestRepository
    {
        private readonly ApplicationDbContext _db;

        public QuestRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateAsync(Quest entity)
        {
            await _db.Quests.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Quest entity)
        {
            _db.Quests.Remove(entity);
            return await SaveAsync();
        }

        public async Task<IList<Quest>> FindAllAsync()
        {
            var items = await _db.Quests.ToListAsync();
            return items;
        }

        public async Task<Quest> FindByIdAsync(int id)
        {
            var item = await _db.Quests.FirstOrDefaultAsync(i => i.QuestId == id);
            return item;
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Quest entity)
        {
            _db.Quests.Update(entity);
            return await SaveAsync();
        }
    }
}
