using AutoMapper;
using player_log.Data;
using player_log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Mappings
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            // map the data class to the view class and vice versa
            CreateMap<Campaign, ListCampaignsVM>().ReverseMap();
            CreateMap<Campaign, CreateCampaignVM>().ReverseMap();
        }
    }
}
