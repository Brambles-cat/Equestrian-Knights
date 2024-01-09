using UnityEngine;

namespace PonyMod.Ponies
{
    class Fluttershy : Pony
    {
        public Fluttershy(string name, Vector3 offset) : base(name, offset) {
            canFly = true;
        }
    }
}
