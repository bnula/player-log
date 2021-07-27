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
    }
}
