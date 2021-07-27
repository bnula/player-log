using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Character
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int Level { get; set; }
        public Campaign.Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
    }
}
