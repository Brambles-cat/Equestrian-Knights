using Logger = Modding.Logger;
using AnimState = PonyMod.Ponies.Pony.AnimState;
using UnityEngine;
using PonyMod.Ponies;
using Satchel;
using GlobalEnums;

namespace PonyMod {
    class SpriteAnimator {
        public static GameObject displayedPony = new GameObject();
        private static AnimController animCtrlr = null!;
        private static HeroControllerStates controllerStates = null!;
        private static HeroAnimationController knightAnim = null!;
        public static void init(On.HeroController.orig_Awake orig, HeroController self)
        {
            orig(self);
            displayedPony.transform.SetParent(self.transform);
            animCtrlr = displayedPony.GetAddComponent<AnimController>();

            controllerStates = self.cState;
            knightAnim = self.GetComponent<HeroAnimationController>();
            displayedPony.transform.position = self.transform.position + Pony.currentPony.centerOffset;
            //HutongGames.PlayMaker.Actions.light;
            //HeroController.instance.vignetteFSM. = false;
            //HeroController.instance.gameObject.LocateMyFSM("")
            //SpriteRenderer sr = displayedPony.GetAddComponent<SpriteRenderer>();
            //sr.material.color = Color.clear;
            //Shader s = Shader.Find("Unlit/Texture");
            //sr.material.shader = s;
            //PlayMakerFSM.FindFsmOnGameObject()
        }

        public static void AnimationUpdate()
        {
            ActorStates actorState = knightAnim.actorState;

            if (actorState == ActorStates.running) {
                // might need to actually fix this someday instead of writing that
                // just so walk can replace the looking back to idle anim
                if (Pony.currentPony.currentAnimState == AnimState.look_up) { animCtrlr.priority = 0; }

                PlayIfNotAlready(AnimState.walk);
                
                animCtrlr.anim.fps = controllerStates.inWalkZone ? 24 : 38;

            } else if (actorState == ActorStates.idle) {
                if (controllerStates.lookingUpAnim) {
                    PlayIfNotAlready(AnimState.look_up);
                }
                else if (animCtrlr.currentFrame == 4 && animCtrlr.anim == Pony.currentPony.anims[(int) AnimState.look_up]) {
                    PlayIfNotAlready(AnimState.look_up, 1, true);
                } else {
                    PlayIfNotAlready(AnimState.idle);
                }
            }
        }

        private static void PlayIfNotAlready(AnimState animState, int priority = 0, bool reversed = false)
        {
            if (Pony.currentPony.currentAnimState != animState || priority > animCtrlr.priority)
            {
                Logger.Log($"Check: {animState}");
                bool played = animCtrlr.Play(Pony.currentPony.anims[(int)animState], priority, reversed);
                if (played) Pony.currentPony.currentAnimState = animState;
            }
        }

        public static void play(AnimState animation) {
            Pony.currentPony.currentAnimState = animation;
            animCtrlr.Play(Pony.currentPony.anims[(int) animation]);
            animCtrlr.currentFrame = 0;
            Logger.Log($"Playing: {animation}");
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