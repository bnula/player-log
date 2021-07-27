using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Army
{
    public class Army
    {
        public int ArmyId { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string ArmyComposition { get; set; }
        public Campaign.Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
        public Location.Location HomeLocation { get; set; }
        public int HomeLocationId { get; set; }
        public Location.Location CurrentLocation { get; set; }
        public int CurrentLocationId { get; set; }
        public Npc.Npc Leader { get; set; }
        public int LeaderId { get; set; }
    }
}
