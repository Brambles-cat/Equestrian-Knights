using PonyMod.Ponies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonyMod
{
    public class LocalData
    {
        string currentPony = "twilight";
        List<string> party = new List<string> { "twilight" };
        List<string> acquiredElements = new List<string>();
        Dictionary<string, int> items = new Dictionary<string, int>();

        public Pony nextPony()
        {
            currentPony = party[party.IndexOf(currentPony) + 1 % party.Count];
            switch (currentPony)
            {
                case "twilight sparkle": return Pony.twilight;
                case "applejack": return Pony.applejack;
                case "fluttershy": return Pony.fluttershy;
                case "pinkie pie": return Pony.pinkie;
                case "rainbow dash": return Pony.rainbow;
                case "rarity": return Pony.rarity;

                default: throw new NotImplementedException($"invalid pony name: {currentPony}");
            }
        }

        public void addPony(string pony)
        {
            party.Add(pony);
        }
    }
}
