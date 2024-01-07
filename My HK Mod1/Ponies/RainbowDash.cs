using UnityEngine;

namespace PonyMod.Ponies
{
    class RainbowDash : Pony
    {
        public RainbowDash(string name, Vector3 offset) : base(name, offset) {
            canFly = true;
        }
    }
}
