﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Models
{
    public class ListCampaignsVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateCampaignVM
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}