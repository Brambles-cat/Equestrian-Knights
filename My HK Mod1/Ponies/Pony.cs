using System;
using System.IO;
using UnityEngine;

namespace PonyMod.Ponies
{
    // this is probably not optimized at all but whatever, i'll fix this later
    public abstract class Pony
    {
        static bool logFailed = false, logSucceeded = true;

        public Satchel.Animation[] anims = new Satchel.Animation[Enum.GetValues(typeof(AnimState)).Length];
        public int hitpoints = 4;
        public bool isCrystalized = false, canFly = false;
        public Vector3 centerOffset;
        public AnimState currentAnim = AnimState.idle;

        public Pony(string ponyName, Vector3 centerOffset) {
            foreach (AnimState state in Enum.GetValues(typeof(AnimState))) {
                try {
                    anims[(int) state] = Satchel.CustomAnimation.LoadAnimation(Path.Combine(PonyMod.DataDirectory, $"{ponyName}\\{state}.json"));
                    if (logSucceeded) Modding.Logger.Log($"Loaded {ponyName}/{state}");
                }
                catch {
                    if (logFailed) Modding.Logger.Log($"Animation {ponyName}/{state} failed to load");
                }
            }
            this.centerOffset = centerOffset;
        }

        public static Pony
            currentPony = null!,
            twilight = new TwilightSparkle("Twilight Sparkle", Vector3.zero),
            applejack = new Applejack("Applejack", new Vector3(-0.3f, 0.1f, 0)),
            fluttershy = new Fluttershy("Fluttershy", new Vector3(-0.6f, 0.1f, 0f)),
            pinkie = new PinkiePie("Pinkie Pie", Vector3.zero),
            rainbow = new RainbowDash("Rainbow Dash", Vector3.zero),
            rarity = new Rarity("Rarity", Vector3.zero);

        public static Pony getFromStr(string pony)
        {
            switch (pony)
            {
                case "twilight sparkle": return twilight;
                case "applejack": return applejack;
                case "fluttershy": return fluttershy;
                case "pinkie pie": return pinkie;
                case "rainbow dash": return rainbow;
                case "rarity": return rarity;

                default: throw new NotImplementedException($"invalid pony name: {pony}");
            }
        }

        public enum AnimState
        {
            sit,
            look_up,
            look_down,
            idle,
            walk,
            run,
            air_ascending,
            fly,
            fall,
        }
    }
}
