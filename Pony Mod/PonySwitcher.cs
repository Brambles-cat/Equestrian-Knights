using Modding;
using PonyMod.Ponies;
using static PonyMod.PonyMod;

namespace PonyMod
{
    class PonySwitcher
    {
        
        public static void nextPony()
        {
            int newIndex = (data.party.IndexOf(data.currentPony) + 1) % data.party.Count;
            data.currentPony = data.party[newIndex];

            Pony.currentPony = Pony.getFromStr(data.currentPony);
            Logger.Log(data.currentPony);
            SpriteAnimator.updateOffset();
            SpriteAnimator.play(Pony.currentPony.currentAnimState);
        }

        public static void previousPony()
        {
            int prevIndex = data.party.IndexOf(data.currentPony) - 1;
            data.currentPony = data.party[prevIndex < 0 ? data.party.Count - 1 : prevIndex];
            Logger.Log(data.currentPony);
            Pony.currentPony = Pony.getFromStr(data.currentPony);
            SpriteAnimator.updateOffset();
            SpriteAnimator.play(Pony.currentPony.currentAnimState);
        }
    }
}
