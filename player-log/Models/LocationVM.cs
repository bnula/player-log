using Microsoft.AspNetCore.Mvc.Rendering;
using player_log.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Models
{
    public class LocationListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
    }

    public class LocationDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Population { get; set; }
        public string Description { get; set; }
        public IEnumerable<SelectListItem> Campaigns { get; set; }
        public int CampaignId { get; set; }
        public string LocationInventory { get; set; }
    }

    public class LocationViewDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Population { get; set; }
        public string Description { get; set; }
        public Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
        public string LocationInventory { get; set; }
    }
}
