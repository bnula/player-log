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
            // create the record in the Db, save changes and return true if there was a change
            _db.Characters.Add(entity);
            return Save();
        }

        public bool Delete(Character entity)
        {
            // remove the record from the Db, save changes and return true if there was a change
            _db.Remove(entity);
            return Save();
        }

        public ICollection<Character> FindAll()
        {
            // query the DB table to return a list of all the items in the table
            var items = _db.Characters.ToList();
            return items;
        }

        public Character FindById(int id)
        {
            // query the DB table to return an item based on the Id
            var item = _db.Characters.Find(id); // var item = _db.Characters.FirstOrDefault(t => t.Id == id);
            return item;
        }

        public bool Save()
        {
            // save any changes done to the Db and return true if there has been at least 1 change
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(Character entity)
        {
            // update the record in the Db, save changes and return true if there was a change
            _db.Update(entity);
            return Save();
        }
    }
}
