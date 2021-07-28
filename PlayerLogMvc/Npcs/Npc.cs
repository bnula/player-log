using PlayerLogMvc.Campaigns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Npcs
{
    public class Npc
    {
        [Key]
        public int NpcId { get; set; }
        public string NpcName { get; set; }
        public string Allegiance { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public Location.Location HomeLocation { get; set; }
        public int HomeLocationId { get; set; }
        public Location.Location CurrentLocation { get; set; }
        public int CurrentLocationId { get; set; }
        public Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
    }
}
