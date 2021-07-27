using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlayerLogMvc.Campaign;
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

        public DbSet<Campaign.Campaign> Campaigns { get; set; }
        public DbSet<Npc.Npc> Npcs { get; set; }
        public DbSet<Location.Location> Locations { get; set; }
        public DbSet<Army.Army> Armies { get; set; }
        public DbSet<Character.Character> Characters { get; set; }
        public DbSet<Quest.Quest> Quests { get; set; }
    }
}
