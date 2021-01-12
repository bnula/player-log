using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using player_log.Models;

namespace player_log.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Companion> Companions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<ListCharactersVM> ListCharactersVM { get; set; }
        public DbSet<ListCampaignsVM> ListCampaignsVM { get; set; }
        public DbSet<player_log.Models.CreateCampaignVM> CreateCampaignVM { get; set; }
        public DbSet<player_log.Models.EditCampaignVM> EditCampaignVM { get; set; }
        public DbSet<player_log.Models.DetailsCampaignVM> DetailsCampaignVM { get; set; }

    }
}
