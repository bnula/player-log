using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Npc
{
    public class Npc
    {
        [Key]
        public int NpcId { get; set; }
        public string NpcName { get; set; }
        public string Allegiance { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public Campaign.Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
    }
}
