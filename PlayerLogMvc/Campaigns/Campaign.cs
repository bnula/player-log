using PlayerLogMvc.Npcs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerLogMvc.Campaigns
{
    public class Campaign
    {
        [Key]
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public IEnumerable<Npc> Npcs { get; set; }
    }
}
