using Microsoft.AspNetCore.Mvc.Rendering;
using player_log.Data;
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

        [DisplayName("Class")]
        public string MainClass { get; set; }
        
        [DisplayName("Multiclass?")]
        public bool Multiclass { get; set; }

        [DisplayName("Second Class")]
        public string SecondClass { get; set; }

        [DisplayName("Third Class")]
        public string ThirdClass { get; set; }

        [DisplayName("Total Level")]
        public int Level { get; set; }

        [Range(1,20)]
        public int Strength { get; set; }

        [Range(1, 20)]
        public int Dexterity { get; set; }

        [Range(1, 20)]
        public int Constitution { get; set; }

        [Range(1, 20)]
        public int Wisdom { get; set; }

        [Range(1, 20)]
        public int Intelligence { get; set; }

        [Range(1, 20)]
        public int Charisma { get; set; }
        public IEnumerable<SelectListItem> Campaigns { get; set; }
        public int CampaignId { get; set; }
    }

    public class CharacterViewDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }

        [DisplayName("Class")]
        public string MainClass { get; set; }

        [DisplayName("Multiclass?")]
        public bool Multiclass { get; set; }

        [DisplayName("Second Class")]
        public string SecondClass { get; set; }

        [DisplayName("Third Class")]
        public string ThirdClass { get; set; }

        [DisplayName("Total Level")]
        public int Level { get; set; }

        [Range(1, 20)]
        public int Strength { get; set; }

        [Range(1, 20)]
        public int Dexterity { get; set; }

        [Range(1, 20)]
        public int Constitution { get; set; }

        [Range(1, 20)]
        public int Wisdom { get; set; }

        [Range(1, 20)]
        public int Intelligence { get; set; }

        [Range(1, 20)]
        public int Charisma { get; set; }
        public Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
    }

    public class CharacterListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        
        [DisplayName("Class")]
        public string MainClass { get; set; }
        
        [DisplayName("Multiclass?")]
        public bool Multiclass { get; set; }
        public Campaign Campaign { get; set; }
        public int CampaignId { get; set; }

    }

}
