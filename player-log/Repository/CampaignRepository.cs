using player_log.Contracts;
using player_log.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Repository
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ApplicationDbContext _db;
        public CampaignRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Campaign entity)
        {
            _db.Add(entity);
            return Save();
        }

        public bool Delete(Campaign entity)
        {
            _db.Remove(entity);
            return Save();
        }

        public ICollection<Campaign> FindAll()
        {
            var items = _db.Campaigns.ToList();
            return items;
        }

        public Campaign FindById(int id)
        {
            var item = _db.Campaigns.Find(id);
            return item;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(Campaign entity)
        {
            _db.Update(entity);
            return Save();
        }
    }
}
