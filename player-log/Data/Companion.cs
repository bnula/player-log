using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Data
{
    public class Companion
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cr { get; set; }
        public string CompanionClass { get; set; }
        public string Description { get; set; }
        
        [ForeignKey("LocationId")]
        public string Location { get; set; }
        public int LocationId { get; set; }

        [ForeignKey("CampaignId")]
        public string Campaign { get; set; }
        public int? CampaignId { get; set; }

    }
}
