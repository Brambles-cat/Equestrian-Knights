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
        private static HeroControllerStates controllerStates = null!;
        private static HeroAnimationController knightAnim = null!;
        public static void init(On.HeroController.orig_Awake orig, HeroController self)
        {
            orig(self);
            displayedPony.transform.SetParent(self.transform);
            animCtrlr = displayedPony.GetAddComponent<CustomAnimationController>();

            controllerStates = self.cState;
            knightAnim = self.GetComponent<HeroAnimationController>();
            displayedPony.transform.position = self.transform.position + Pony.currentPony.centerOffset;

            SpriteRenderer sr = displayedPony.GetAddComponent<SpriteRenderer>();
            sr.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        }

        public static void AnimationUpdate()
        {
            ActorStates actorState = knightAnim.actorState;

            if (actorState == ActorStates.running)
            {
                playIfNotAlready(Pony.AnimState.walk);
                if (controllerStates.inWalkZone)
                {
                    animCtrlr.anim.fps = 24;
                    return;
                }
                animCtrlr.anim.fps = 35;
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
            displayedPony.transform.position =
                new Vector3(
                    HeroController.instance.transform.position.x + (controllerStates.facingRight ? Pony.currentPony.centerOffset.x : -Pony.currentPony.centerOffset.x),
                    HeroController.instance.transform.position.y + Pony.currentPony.centerOffset.y
                );
        }
    }
}