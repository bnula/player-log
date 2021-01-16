using Microsoft.AspNetCore.Mvc.Rendering;
using player_log.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Models
{
    public class CompanionListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
    }

    public class CompanionDetailsVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Cr { get; set; }
        public string CompanionClass { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public IEnumerable<SelectListItem> Campaigns { get; set; }
        public int CampaignId { get; set; }
    }

    public class CompanionViewDetailsVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Cr { get; set; }
        public string CompanionClass { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
    }
}
