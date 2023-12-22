using Modding;
using System.Collections.Generic;
using UnityEngine;
using Satchel;
using PonyMod.Ponies;

namespace PonyMod
{
    public class PonyMod : Mod, ILocalSettings<LocalData>, IGlobalSettings<GlobalData>
    {
        public static string DataDirectory = AssemblyUtils.getCurrentDirectory();
        new public string GetName() => "Equestrian Knights";
        public override string GetVersion() => "v0.0.1";
        public static LocalData localSaveData { get; set; } = new LocalData();
        public static GlobalData globalSaveData { get; set; } = new GlobalData();


        public override void Initialize()
        {
            On.HeroController.Awake += SpriteLoader.init;
            ModHooks.HeroUpdateHook += OnHeroUpdate;
            On.HeroController.SceneInit += InitHero;
        }


        private void InitHero(On.HeroController.orig_SceneInit orig, HeroController self)
        {
            orig(self);
            Log("Ready");
        }

        public override List<(string, string)> GetPreloadNames()
        {
            return base.GetPreloadNames();
        }

        string playingClip = null!;

        public void OnHeroUpdate()
        {
            if (!SpriteLoader.animCtrl.animator.CurrentClip.name.Equals(playingClip))
            {
                playingClip = SpriteLoader.animCtrl.animator.CurrentClip.name;
                Log($"{SpriteLoader.animCtrl.animator.CurrentClip.frames.Length}: {playingClip}");
            }
            if (Input.GetKeyDown(KeyCode.H)) {
                SpriteLoader.animCtrl.UpdateState(GlobalEnums.ActorStates.hard_landing);
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                SpriteLoader.LoadSprites();
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                //tk2dSpriteDefinition[] defs = HeroController.instance.gameObject.GetComponent<tk2dSprite>().Collection.spriteDefinitions;
                //for (int i = 0; i < defs.Length; ++i)
                //    Log($"{i}: {defs[i].name}");
                tk2dSpriteAnimationClip[] clips = SpriteLoader.animCtrl.animator.Library.clips;
                for (int i = 0; i < clips.Length; ++i)
                {
                    Log($"{clips[i].name}");
                }
            }
            else if (Input.GetKey(KeyCode.I))
            {
                PonySwitcher.nextPony();
            }
        }


        void ILocalSettings<LocalData>.OnLoadLocal(LocalData data) {
            localSaveData = data;
            Pony.currentPony = PonySwitcher.getPony(data.currentPony);
        }
        public LocalData OnSaveLocal() => localSaveData;

        public void OnLoadGlobal(GlobalData data) => globalSaveData = data;
        public GlobalData OnSaveGlobal() => globalSaveData;
    }
}

//string newDir = Path.Combine(DataDirectory, "My_mod_stuffs_yis");

//Texture2D t = Satchel.TextureUtils.duplicateTexture(
//    (Texture2D)HeroController.instance.GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture);
//File.WriteAllBytes(DataDirectory + "/test.png", t.EncodeToPNG());


/*
 public override List<(string, string)> GetPreloadNames()
        {
            if (GlobalSettings.Preloads)
            {
                return new List<(string, string)>
                {
                    ("Abyss_10", "higher_being/Dish Plat/Knight Dummy"),
                    ("Abyss_12", "Scream 2 Get/Cutscene Knight"),
                    ("Abyss_21", "Shiny Item DJ/Knight Cutscene"),
                    ("Crossroads_50", "Quirrel Lakeside/Sit Region/Knight Sit"),
                    ("Deepnest_East_12", "Hornet Blizzard Return Scene"),
                    ("Deepnest_Spider_Town", "RestBench Spider/Webbed Knight"),
                    ("Dream_Abyss", "End Cutscene/Dummy"),
                    ("GG_Door_5_Finale", "abyss_door_5_cutscene_sequence/main_chars"),
                    ("GG_Vengefly", "Boss Scene Controller/Dream Entry/Knight Dream Arrival"),
                    ("RestingGrounds_07", "Dream Moth/Knight Dummy"),
                    
                };   
            }
            
            return new List<(string, string)>();
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
*/


/*
Texture2D spriteSheet = Resources.Load<Texture2D>("yourSpriteSheet");
int frameWidth = 50, frameHeight = 50; // Replace with the desired width and height of each frame
int numFrames = 10; // Replace with the number of frames in your sprite sheet
Sprite[] sprites = new Sprite[numFrames];
for (int i = 0; i < numFrames; i++)
{
    int x = i * frameWidth;
    int y = 0;
    sprites[i] = Sprite.Create(spriteSheet, new Rect(x, y, frameWidth, frameHeight), new Vector2(0.5f, 0.5f));
}

This code creates an array of Sprite objects, where each Sprite corresponds to a single frame
in your sprite sheet. You can modify the values of frameWidth, frameHeight, and numFrames to
match the dimensions of your sprite sheet and the number of frames it contains. The Sprite.Create()
method takes as input the sprite sheet, the position and dimensions of the current frame, and the
pivot point of the sprite. The pivot point is specified as a Vector2 with values between 0 and 1,
where (0, 0) corresponds to the bottom-left corner of the sprite and (1, 1) corresponds to the
top-right corner.

Once you have created the array of Sprite objects, you can use them to set up your animations in Unity.
To do this, you can create an AnimationClip for each animation and add the corresponding SpriteRenderer
component to your game object. Then, you can assign the Sprite objects to the SpriteRenderer component
and set up the animation using the AnimationClip
 */