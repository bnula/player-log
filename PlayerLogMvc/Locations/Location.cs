using PlayerLogMvc.Campaigns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Locations
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string LocationType { get; set; }
        public string LocationInventory { get; set; }
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
    }
}
