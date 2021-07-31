using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Npcs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Locations
{
    public class LocationVM
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationType { get; set; }
        public Campaign Campaign { get; set; }
    }

    public class LocationDetailsVM
    {
        public int LocationId { get; set; }
        [Required]
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        [Required]
        public string LocationType { get; set; }
        public string LocationInventory { get; set; }
        [Required]
        public Campaign Campaign { get; set; }
        public IEnumerable<Npc> HomeNpcs { get; set; }
        public IEnumerable<Npc> CurrentNpcs { get; set; }
    }
}
