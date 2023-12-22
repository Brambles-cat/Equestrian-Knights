using System.IO;
using UnityEngine;

namespace PonyMod.Ponies
{
    public abstract class Pony
    {
        public Texture2D texture = new Texture2D(4096, 4096);

        public Pony(string spriteSheetName) {
            texture.LoadImage(File.ReadAllBytes($"{PonyMod.DataDirectory}/{spriteSheetName}.png"));
        }

        public static Pony
            currentPony = null!,
            twilight = new TwilightSparkle("Twilight Sparkle"),
            applejack = new Applejack("Applejack"),
            fluttershy = new Fluttershy("Fluttershy"),
            pinkie = new PinkiePie("Pinkie Pie"),
            rainbow = new RainbowDash("Rainbow Dash"),
            rarity = new Rarity("Rarity");
    }
}
