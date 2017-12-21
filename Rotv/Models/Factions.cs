using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rotv.Models
{

    public class Factions
    {
        public Faction[] AllFactions { get; set; }
    }

    public class Faction
    {
        public int id { get; set; }
        public string name { get; set; }
        public int updated_at { get; set; }
        public int? government_id { get; set; }
        public string government { get; set; }
        public int? allegiance_id { get; set; }
        public string allegiance { get; set; }
        public int? state_id { get; set; }
        public string state { get; set; }
        public int? home_system_id { get; set; }
        public bool is_player_faction { get; set; }
    }

}
