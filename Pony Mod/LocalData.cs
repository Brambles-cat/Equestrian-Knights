using Modding;
using System.Collections.Generic;

namespace PonyMod
{
    public class LocalData
    {
        public string currentPony = "twilight sparkle";
        public List<string> party = new List<string> { "twilight sparkle" };
        List<string> acquiredElements = new List<string>();
        Dictionary<string, int> items = new Dictionary<string, int>();
    }
}
