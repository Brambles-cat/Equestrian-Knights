using System.Collections.Generic;

namespace PonyMod
{
    public class LocalData
    {
        public string currentPony = "twilight";
        List<string> party = new List<string> { "twilight" };
        List<string> acquiredElements = new List<string>();
        Dictionary<string, int> items = new Dictionary<string, int>();

        public void addPony(string pony)
        {
            party.Add(pony);
        }

        public string nextPony()
        {
            return party[party.IndexOf(currentPony) + 1 % party.Count];
        }
        public string previousPony()
        {
            int prevIndex = party.IndexOf(currentPony) - 1;
            return party[prevIndex < 0 ? party.Count - 1 : prevIndex];
        }
    }
}
