using PlayerLogMvc.Campaign;
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
    }
}
