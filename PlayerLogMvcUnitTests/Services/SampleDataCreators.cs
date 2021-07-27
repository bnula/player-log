using PlayerLogMvc.Campaign;
using PlayerLogMvc.Npc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvcUnitTests.Services
{
    public static class SampleDataCreators
    {
        public static List<Campaign> GetTestCamps()
        {
            var camps = new List<Campaign>();
            camps.Add(new Campaign
            {
                CampaignId = 1,
                CampaignName = "test 1"
            });
            camps.Add(new Campaign
            {
                CampaignId = 2,
                CampaignName = "test 2"
            });

            return camps;
        }

        public static List<Npc> GetTestNpcs()
        {
            var npcs = new List<Npc>
        {
            new Npc
            {
                NpcId = 1,
                NpcName = "test1",
                Allegiance = "test1",
                Description = "test1",
                Notes = "test1,",
                CampaignId = 1,
                CurrentLocationId = 1,
                HomeLocationId = 1
            },
            new Npc
            {
                NpcId = 2,
                NpcName = "test2",
                Allegiance = "test2",
                Description = "test2",
                Notes = "test2",
                CampaignId = 2,
                CurrentLocationId = 2,
                HomeLocationId = 2
            }
        };

            return npcs;
        }
    }

}
