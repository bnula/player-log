﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Data
{
    public class Quest
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainObjective { get; set; }
        public string Description { get; set; }
    }
}
