using Microsoft.EntityFrameworkCore;
using PlayerLogMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Characters
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly ApplicationDbContext _db;

        public CharacterRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(Character entity)
        {
            await _db.Characters.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Character entity)
        {
            _db.Characters.Remove(entity);
            return await SaveAsync();
        }

        public async Task<IList<Character>> FindAllAsync()
        {
            var items = await _db.Characters.ToListAsync();
            return items;
        }

        public async Task<Character> FindByIdAsync(int id)
        {
            var item = await _db.Characters.FirstOrDefaultAsync(i => i.CharacterId == id);
            return item;
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Character entity)
        {
            _db.Characters.Update(entity);
            return await SaveAsync();
        }
    }
}
