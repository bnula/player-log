using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Data
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [DefaultValue("Name")]
        public string Name { get; set; }

        [Required]
        [DefaultValue("Human")]
        public string Race { get; set; }

        [Required]
        [DefaultValue("Pick a Class")]
        public string MainClass { get; set; }

        [DefaultValue(false)]
        public bool Multiclass { get; set; }
        public string SecondClass { get; set; }
        public string ThirdClass { get; set; }

        [Required]
        [DefaultValue(1)]
        public int Level { get; set; }

        [Required]
        [DefaultValue(10)]
        public int Strength { get; set; }

        [Required]
        [DefaultValue(10)]
        public int Dexterity { get; set; }

        [Required]
        [DefaultValue(10)]
        public int Constitution { get; set; }

        [Required]
        [DefaultValue(10)]
        public int Wisdom { get; set; }

        [Required]
        [DefaultValue(10)]
        public int Intelligence { get; set; }

        [Required]
        [DefaultValue(10)]
        public int Charisma { get; set; }
    }
}
