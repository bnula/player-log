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
        public DbSet<CharacterListVM> CharacterListVM { get; set; }
        public DbSet<CampaignListVM> CampaignListVM { get; set; }
        public DbSet<CampaignCreateVM> CampaignCreateVM { get; set; }
        public DbSet<CampaignEditVM> CampaignEditVM { get; set; }
        public DbSet<CampaignDetailsVM> CampaignDetailsVM { get; set; }
        public DbSet<CompanionListVM> CompanionListVM { get; set; }
        public DbSet<CompanionDetailsVM> CompanionDetailsVM { get; set; }
        public DbSet<CompanionCreateVM> CompanionCreateVM { get; set; }
        public DbSet<CompanionEditVM> CompanionEditVM { get; set; }
        public DbSet<CharacterDetailsVM> CharacterDetailsVM { get; set; }

    }
}
