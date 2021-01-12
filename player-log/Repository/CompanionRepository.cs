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
            throw new NotImplementedException();
        }

        public bool Delete(Companion entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<Companion> FindAll()
        {
            throw new NotImplementedException();
        }

        public Companion FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool RecordExists(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Companion entity)
        {
            throw new NotImplementedException();
        }
    }
}
