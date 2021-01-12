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
            CreateMap<Campaign, CampaignCreateVM>().ReverseMap();
            CreateMap<Campaign, CampaignEditVM>().ReverseMap();
            CreateMap<Campaign, CampaignDetailsVM>().ReverseMap();
            
            CreateMap<Character, CharacterCreateVM>().ReverseMap();
            CreateMap<Character, CharacterEditVM>().ReverseMap();
            CreateMap<Character, CharacterDetailsVM>().ReverseMap();
            CreateMap<Character, CharacterListVM>().ReverseMap();
            
            CreateMap<Companion, CompanionCreateVM>().ReverseMap();
            CreateMap<Companion, CompanionDetailsVM>().ReverseMap();
            CreateMap<Companion, CompanionEditVM>().ReverseMap();
            CreateMap<Companion, CompanionListVM>().ReverseMap();
        }
    }
}
