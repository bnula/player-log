using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Locations;
using PlayerLogMvc.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Armies
{
    public class Army
    {
        public int ArmyId { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string ArmyComposition { get; set; }
        public Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
        public Location HomeLocation { get; set; }
        public int HomeLocationId { get; set; }
        public Location CurrentLocation { get; set; }
        public int CurrentLocationId { get; set; }
        public Npc Leader { get; set; }
        public int LeaderId { get; set; }
    }
}
