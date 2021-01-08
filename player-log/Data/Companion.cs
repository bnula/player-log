using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Data
{
    public class Companion
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cr { get; set; }
        public string CompanionClass { get; set; }
        public string Description { get; set; }

    }
}
