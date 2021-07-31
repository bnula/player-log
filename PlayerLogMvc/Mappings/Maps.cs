using AutoMapper;
using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Locations;
using PlayerLogMvc.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Campaign, CampaignVM>().ReverseMap();
            CreateMap<Campaign, CampaignDetailsVM>().ReverseMap();
            CreateMap<Npc, NpcVM>().ReverseMap();
            CreateMap<Npc, NpcDetailsVM>().ReverseMap();
            CreateMap<Location, LocationVM>().ReverseMap();
            CreateMap<Location, LocationDetailsVM>().ReverseMap();
        }
    }
}
