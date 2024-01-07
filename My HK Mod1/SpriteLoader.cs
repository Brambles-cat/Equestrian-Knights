using Logger = Modding.Logger;
using UnityEngine;
using PonyMod.Ponies;
using System;
using System.Collections.ObjectModel;
using Satchel;
using System.IO;
using GlobalEnums;

namespace PonyMod {
    class SpriteLoader {
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
            
            if (actorState == ActorStates.running)
            {
                playIfNotAlready(Pony.AnimState.walk);
                animCtrlr.anim.fps = 32;
            }
            else if (actorState == ActorStates.idle)
            {
                playIfNotAlready(Pony.AnimState.idle);
            }

            HeroControllerStates controllerStates = HeroController.instance.GetComponent<HeroControllerStates>();

            if (controllerStates == null) return;

            if(controllerStates.inWalkZone)
            {
                Logger.Log("in walk zone");
                playIfNotAlready(Pony.AnimState.walk);
                animCtrlr.anim.fps = 20;
            }
        }

        private static void playIfNotAlready(Pony.AnimState animation)
        {
            if (Pony.currentPony.currentAnim != animation)
            {
                Pony.currentPony.currentAnim = animation;
                animCtrlr.Init(Pony.currentPony.anims[(int) animation]);
                animCtrlr.currentFrame = 0;
                Modding.Logger.Log($"Playing: {animation}");
            }
        }

        public static void play(Pony.AnimState animation)
        {
            Pony.currentPony.currentAnim = animation;
            animCtrlr.Init(Pony.currentPony.anims[(int) animation]);
            animCtrlr.currentFrame = 0;
            Modding.Logger.Log($"Playing: {animation}");
        }
    }
}