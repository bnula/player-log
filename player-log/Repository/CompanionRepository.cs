using player_log.Contracts;
using player_log.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Repository
{
    public class CompanionRepository : ICompanionRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanionRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(Companion entity)
        {
            // add the item to the db
            _db.Add(entity);
            // save changes to the db
            return Save();
        }

        public bool Delete(Companion entity)
        {
            // remove the item from the db
            _db.Remove(entity);
            // save cahnges to the db
            return Save();
        }

        public ICollection<Companion> FindAll()
        {
            // retrieve all of the items from the db table
            var items = _db.Companions.ToList();
            return items;
        }

        public Companion FindById(int id)
        {
            // retrieve an item from the db table based on the id
            var item = _db.Companions.Find(id);
            return item;
        }

        public bool RecordExists(int id)
        {
            // check if there is an item in the db based on the id
            var exists = _db.Companions.Any(q => q.Id == id);
            return exists;
        }

        public bool Save()
        {
            // save changes to the db and return number of records changed
            var changes = _db.SaveChanges();
            // return if there were any changes
            return changes > 0;
        }

        public bool Update(Companion entity)
        {
            // update the item in the db
            _db.Update(entity);
            // save changes to the db
            return Save();
        }
    }
}
