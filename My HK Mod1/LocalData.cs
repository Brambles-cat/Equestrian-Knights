using Modding;
using System.Collections.Generic;

namespace PonyMod
{
    public class LocalData
    {
        public string currentPony = "fluttershy";//"twilight sparkle";
        public List<string> party = new List<string> { "applejack", "fluttershy" };
        List<string> acquiredElements = new List<string>();
        Dictionary<string, int> items = new Dictionary<string, int>();

        public void addPony(string pony)
        {
            party.Add(pony);
        }

        public string previousPony()
        {
            int prevIndex = party.IndexOf(currentPony) - 1;
            return party[prevIndex < 0 ? party.Count - 1 : prevIndex];
        }
    }
}
