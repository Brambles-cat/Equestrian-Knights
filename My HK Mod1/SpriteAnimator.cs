using Logger = Modding.Logger;
using UnityEngine;
using PonyMod.Ponies;
using Satchel;
using GlobalEnums;

namespace PonyMod {
    class SpriteAnimator {
        private static bool initialized = false;
        public static GameObject displayedPony = new GameObject();
        private static CustomAnimationController animCtrlr = null!;
        public static void init(On.HeroController.orig_Awake orig, HeroController self)
        {
            //Pony.currentPony = Pony.fluttershy;
            orig(self);
            displayedPony.transform.SetParent(HeroController.instance.transform);
            displayedPony.transform.position = HeroController.instance.transform.position;
            animCtrlr = displayedPony.GetAddComponent<CustomAnimationController>();
            
        }

        public static void StartSpritesTemp() // ponyswitcher
        {
            displayedPony.transform.position = HeroController.instance.transform.position + Pony.currentPony.centerOffset;
            initialized = true;
        }

        public static void AnimationUpdate()
        {
            if (!initialized) return;

            ActorStates actorState = HeroController.instance.GetComponent<HeroAnimationController>().actorState;
            HeroControllerStates controllerStates = HeroController.instance.cState;  // move this elsewhere and the above line as well

            if (actorState == ActorStates.running)
            {
                playIfNotAlready(Pony.AnimState.walk);
                if (controllerStates.inWalkZone)
                {
                    animCtrlr.anim.fps = 20;
                    return;
                }
                animCtrlr.anim.fps = 32;
            }
            else if (actorState == ActorStates.idle)
            {
                if (controllerStates.lookingUpAnim)
                {
                    playIfNotAlready(Pony.AnimState.look_up);
                }
                else playIfNotAlready(Pony.AnimState.idle);
            }
        }

        private static void playIfNotAlready(Pony.AnimState animation)
        {
            if (Pony.currentPony.currentAnim != animation)
            {
                Pony.currentPony.currentAnim = animation;
                animCtrlr.Init(Pony.currentPony.anims[(int) animation]);
                animCtrlr.currentFrame = 0;
                Logger.Log($"Playing: {animation}");
            }
        }

        public static void play(Pony.AnimState animation)
        {
            Pony.currentPony.currentAnim = animation;
            animCtrlr.Init(Pony.currentPony.anims[(int) animation]);
            animCtrlr.currentFrame = 0;
            Modding.Logger.Log($"Playing: {animation}");
        }
        public static void updateOffset()
        {
            displayedPony.transform.position = HeroController.instance.transform.position + Pony.currentPony.centerOffset;
        }
    }
}