﻿using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Npcs
{
    public class NpcVM
    {
        public int NpcId { get; set; }
        [Required]
        public string NpcName { get; set; }
        public string Allegiance { get; set; }
        [Required]
        public Campaign Campaign { get; set; }
    }

    public class NpcDetailsVM 
    {
        public int NpcId { get; set; }
        [Required]
        public string NpcName { get; set; }
        public string Allegiance { get; set; }
        [Required]
        public string Description { get; set; }
        public string Notes { get; set; }
        [Required]
        public int HomeLocationId { get; set; }
        public Location HomeLocation { get; set; }
        [Required]
        public int CurrentLocationId { get; set; }
        public Location CurrentLocation { get; set; }
        public Campaign Campaign { get; set; }
        [Required]
        public int CampaignId { get; set; }
    }

}
