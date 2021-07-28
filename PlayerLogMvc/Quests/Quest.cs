using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Locations;
using PlayerLogMvc.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Quests
{
    public class Quest
    {
        public int QuestId { get; set; }
        public string QuestName { get; set; }
        public string Reward { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public Location StartingLocation { get; set; }
        public int StartingLocationId { get; set; }
        public Npc QuestGiver { get; set; }
        public int QuestGiverId { get; set; }
        public Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
    }
}
