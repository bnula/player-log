using AutoMapper;
using player_log.Data;
using player_log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            // map the data class to the view class and vice versa
            CreateMap<Campaign, CampaignListVM>().ReverseMap();
            CreateMap<Campaign, CampaignDetailsVM>().ReverseMap();

            CreateMap<Character, CharacterListVM>().ReverseMap();
            CreateMap<Character, CharacterDetailsVM>().ReverseMap();
            CreateMap<Character, CharacterViewDetailsVM>().ReverseMap();
            
            CreateMap<Companion, CompanionListVM>().ReverseMap();
            CreateMap<Companion, CompanionDetailsVM>().ReverseMap();
            CreateMap<Companion, CompanionViewDetailsVM>().ReverseMap();

            CreateMap<Quest, QuestListVM>().ReverseMap();
            CreateMap<Quest, QuestDetailsVM>().ReverseMap();
            CreateMap<Quest, QuestViewDetailsVM>().ReverseMap();

            CreateMap<Location, LocationListVM>().ReverseMap();
            CreateMap<Location, LocationDetailsVM>().ReverseMap();
            CreateMap<Location, LocationViewDetailsVM>().ReverseMap();
        }
    }
}
