using player_log.Contracts;
using player_log.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Repository
{
    public class LocationRepository : ILocationRepository
    {
        ApplicationDbContext _db;
        public LocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(Location entity)
        {
            _db.Add(entity);
            return Save();
        }

        public bool Delete(Location entity)
        {
            _db.Remove(entity);
            return Save();
        }

        public ICollection<Location> FindAll()
        {
            var items = _db.Locations.ToList();
            return items;
        }

        public Location FindById(int id)
        {
            var item = _db.Locations.Find(id);
            return item;
        }

        public bool RecordExists(int id)
        {
            var exists = _db.Locations.Any(q => q.Id == id);
            return exists;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(Location entity)
        {
            _db.Update(entity);
            return Save();
        }
    }
}
