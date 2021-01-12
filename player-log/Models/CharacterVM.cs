using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Models
{
    public class CharacterDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public string MainClass { get; set; }
        public bool Multiclass { get; set; }
        public string SecondClass { get; set; }
        public string ThirdClass { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }
        public string Campaign { get; set; }
    }

    public class CharacterCreateVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Race { get; set; }

        [Required]
        [DisplayName("Class")]
        public string MainClass { get; set; }

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

        [Required]
        [DefaultValue("Please pick a Campaign")]
        public string Campaign { get; set; }
    }

    public class CharacterEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Race { get; set; }

        [Required]
        [DisplayName("Class")]
        public string MainClass { get; set; }
        public bool Multiclass { get; set; }
        public string SecondClass { get; set; }
        public string ThirdClass { get; set; }

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

        [Required]
        [DefaultValue("Please pick a Campaign")]
        public string Campaign { get; set; }
    }

    public class CharacterListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        
        [DisplayName("Class")]
        public string MainClass { get; set; }
        public bool Multiclass { get; set; }
        public string Campaign { get; set; }

    }

}
