using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlayerLogMvc.Armies;
using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Characters;
using PlayerLogMvc.Locations;
using PlayerLogMvc.Npcs;
using PlayerLogMvc.Quests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerLogMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Npc> Npcs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Army> Armies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Quest> Quests { get; set; }
    }
}
