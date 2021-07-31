using PlayerLogMvc.Locations;
using PlayerLogMvc.Npcs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerLogMvc.Campaigns
{
    public class CampaignVM
    {
        public int CampaignId { get; set; }
        
        [Required]
        [MaxLength(50)]
        [MinLength(3, ErrorMessage ="Minimal length is {1} characters")]
        public string CampaignName { get; set; }
    }

    public class CampaignDetailsVM
    {
        public string CampaignName { get; set; }

        public IEnumerable<Npc> Npcs { get; set; }
        public IEnumerable<Location> Locations { get; set; }
    }
}
