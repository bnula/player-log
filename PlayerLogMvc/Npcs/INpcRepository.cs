using PlayerLogMvc.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Npcs
{
    public interface INpcRepository : IRepositoryBase<Npc>
    {
        Task<IEnumerable<Npc>> FindByHomeLocationId(int id);
        Task<IEnumerable<Npc>> FindByCurrentLocationId(int id);
    }
}
