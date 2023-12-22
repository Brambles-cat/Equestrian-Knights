using PonyMod.Ponies;
using System;
using static PonyMod.PonyMod;

namespace PonyMod
{
    class PonySwitcher
    {
        public static Pony getPony(string pony)
        {
            switch (pony)
            {
                case "twilight sparkle": return Pony.twilight;
                case "applejack": return Pony.applejack;
                case "fluttershy": return Pony.fluttershy;
                case "pinkie pie": return Pony.pinkie;
                case "rainbow dash": return Pony.rainbow;
                case "rarity": return Pony.rarity;

                default: throw new NotImplementedException($"invalid pony name: {localSaveData.currentPony}");
            }
        }
        public static Pony nextPony()
        {
            localSaveData.currentPony = localSaveData.nextPony();
            return getPony(localSaveData.currentPony);
        }

        public static Pony previousPony()
        {
            localSaveData.currentPony = localSaveData.previousPony();
            return getPony(localSaveData.currentPony);
        }
    }
}
