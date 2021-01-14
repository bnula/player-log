using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Data
{
    public class Quest
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainObjective { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }

        [ForeignKey("CamapignId")]
        public string Campaign { get; set; }
        public int? CampaignId { get; set; }
    }
}
