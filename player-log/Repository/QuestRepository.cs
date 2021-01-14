using player_log.Contracts;
using player_log.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Repository
{
    public class QuestRepository : IQuestRepository
    {
        private readonly ApplicationDbContext _db;
        public QuestRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(Quest entity)
        {
            _db.Quests.Add(entity);
            return Save();
        }

        public bool Delete(Quest entity)
        {
            _db.Quests.Remove(entity);
            return Save();
        }

        public ICollection<Quest> FindAll()
        {
            var items = _db.Quests.ToList();
            return items;
        }

        public Quest FindById(int id)
        {
            var item = _db.Quests.Find(id);
            return item;
        }

        public bool RecordExists(int id)
        {
            var exists = _db.Quests.Any(q => q.Id == id);
            return exists;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(Quest entity)
        {
            _db.Quests.Update(entity);
            return Save();
        }
    }
}
