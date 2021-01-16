using player_log.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Models
{
    public class QuestListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
    }

    public class QuestDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainObjective { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string Reward { get; set; }
        public Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
    }
}
