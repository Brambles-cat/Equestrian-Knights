using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PonyMod.Ponies
{
    public abstract class Pony
    {
        Texture2D texture;


        public static Pony
            twilight = new TwilightSparkle(),
            applejack = new Applejack(),
            fluttershy = new Fluttershy(),
            pinkie = new PinkiePie(),
            rainbow = new RainbowDash(),
            rarity = new Rarity();
    }
}
