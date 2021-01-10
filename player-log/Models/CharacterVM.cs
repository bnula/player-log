using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Models
{
    public class DetailsCharacterVM
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
}
