using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Rotv.Models;
using System.Net.Http;

namespace Rotv.Services
{
    
    public class JsonProcessor
    {
        public IEnumerable<Faction> Factions { get; set; }
        public IEnumerable<Rotv.Models.System> Systems { get; set; }
        public JsonProcessor()
        {
            var http = new HttpClient();
            var systemsJob = http.GetStringAsync("https://eddb.io/archive/v5/systems_populated.json");
            var factionsJob = http.GetStringAsync("https://eddb.io/archive/v5/factions.json");
            systemsJob.Wait();
            factionsJob.Wait();
            // var sFile = File.ReadAllText(Directory.GetCurrentDirectory() + "/Models/systems_populated.json");
            // var fFile = File.ReadAllText(Directory.GetCurrentDirectory() + "/Models/factions.json");
            var systems = JsonConvert.DeserializeObject<List<Rotv.Models.System>>(systemsJob.Result);
            Factions = JsonConvert.DeserializeObject<List<Rotv.Models.Faction>>(factionsJob.Result);

            Systems = systems.Where(s => s.minor_faction_presences.Where(p => p.minor_faction_id == 75633).Any());
        }

        public IEnumerable<Rotv.Models.System> GetRotvSystemInformation() {
            foreach (var system in Systems)
            {
                system.updated_at_display = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(system.updated_at);
                foreach (var faction in system.minor_faction_presences)
                {
                    var matchingFaction = Factions.FirstOrDefault(f => f.id == faction.minor_faction_id);
                    faction.name = matchingFaction.name;
                    faction.influence_display = Math.Round(faction.influence.Value).ToString();
                }                
            }

            return Systems;
        }
    }
}
