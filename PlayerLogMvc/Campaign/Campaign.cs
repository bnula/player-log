using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerLogMvc.Campaign
{
    public class Campaign
    {
        [Key]
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
    }
}
