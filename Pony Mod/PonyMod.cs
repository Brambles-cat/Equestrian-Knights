using Modding;
using System.Collections.Generic;
using UnityEngine;
using Satchel;
using PonyMod.Ponies;
using System;
using System.Linq;
using System.IO;

namespace PonyMod
{
    public class PonyMod : Mod, ILocalSettings<LocalData>, IGlobalSettings<GlobalData>
    {
        public static string DataDirectory = AssemblyUtils.getCurrentDirectory();
        new public string GetName() => "Equestrian Knights";
        public override string GetVersion() => "v0.0.1";
        public static LocalData data { get; set; } = new LocalData();
        public static GlobalData globalSaveData { get; set; } = new GlobalData();


        public override void Initialize()
        {
            On.HeroController.Awake += SpriteAnimator.init;
            On.HeroController.Awake += InitHero;
            On.HeroController.Awake += InitHero;
            //On.HeroController.FixedUpdate += test;
            ModHooks.HeroUpdateHook += OnHeroUpdate;
            ModHooks.HeroUpdateHook += SpriteAnimator.AnimationUpdate;
            //On.HeroController.SceneInit += InitHero;
        }


        private void InitHero(On.HeroController.orig_Awake orig, HeroController self)
        {
            orig(self);
            //rb = HeroController.instance.GetComponent<Rigidbody2D>();
        }

        Vector3 offset = new Vector3(0, 0, 0);
        public void OnHeroUpdate()
        {

            //HeroController.instance.AffectedByGravity(false);
            if (Input.GetKeyDown(KeyCode.H))
            {
                PonySwitcher.previousPony();
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                PonySwitcher.nextPony();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                HeroController.instance.GetComponent<MeshRenderer>().enabled = !HeroController.instance.GetComponent<MeshRenderer>().enabled;
            }

            // offset
            else if (Input.GetKeyDown(KeyCode.J))
            {
                offset.x += -0.1f;
                SpriteAnimator.displayedPony.transform.position = HeroController.instance.transform.position + offset;
                Log(offset);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                offset.x += 0.1f;
                SpriteAnimator.displayedPony.transform.position = HeroController.instance.transform.position + offset;
                Log(offset);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                offset.y += 0.1f;
                SpriteAnimator.displayedPony.transform.position = HeroController.instance.transform.position + offset;
                Log(offset);
            }
            else if(Input.GetKeyDown(KeyCode.K))
            {
                offset.y += -0.1f;
                SpriteAnimator.displayedPony.transform.position = HeroController.instance.transform.position + offset;
                Log(offset);
            }
        }


        void ILocalSettings<LocalData>.OnLoadLocal(LocalData data) {
            PonyMod.data = data;
            Pony.currentPony = Pony.getFromStr(data.currentPony);
            Log($"current pony = {Pony.currentPony}");
        }
        public LocalData OnSaveLocal() => data;

        public void OnLoadGlobal(GlobalData data) => globalSaveData = data;
        public GlobalData OnSaveGlobal() => globalSaveData;
    }
}


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