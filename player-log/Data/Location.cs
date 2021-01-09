using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Data
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Population { get; set; }
        public string Description { get; set; }

        [ForeignKey("CampaignId")]
        public string Campaign { get; set; }
        public int CampaignId { get; set; }
        public string LocationInventory { get; set; }
    }
}
