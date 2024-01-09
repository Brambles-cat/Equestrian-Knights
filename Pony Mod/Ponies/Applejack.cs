using UnityEngine;

namespace PonyMod.Ponies
{
    class Applejack : Pony
    {
        public Applejack(string name, Vector3 offset) : base(name, offset) {
            hitpoints = 5;
        }
    }
}
