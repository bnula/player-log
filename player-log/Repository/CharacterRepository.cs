using player_log.Contracts;
using player_log.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly ApplicationDbContext _db;

        public CharacterRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Character entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Character entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<Character> FindAll()
        {
            throw new NotImplementedException();
        }

        public Character FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Character entity)
        {
            throw new NotImplementedException();
        }
    }
}
